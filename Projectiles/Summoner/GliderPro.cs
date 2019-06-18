using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TemplateMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod.Projectiles.Summoner
{
	public class GliderPro : ModProjectile
	{
		private float Timer
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}

		private float State
		{
			get { return projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}
		public Vector2 TargetLocation = new Vector2();

		private static float _nearPlayerSpeed = 0.1f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("僚机");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.aiStyle = -1;
			projectile.timeLeft = 3;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.scale = 1.1f;
			// 召唤物必备的属性
			Main.projPet[projectile.type] = true;
			projectile.netImportant = true;
			projectile.minionSlots = 1;
			projectile.minion = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		/// <summary>
		/// 没有接触伤害
		/// </summary>
		/// <returns></returns>
		public override bool MinionContactDamage()
		{
			return false;
		}
		/// <summary>
		/// 寻找最近的敌方单位
		/// </summary>
		/// <param name="position"></param>
		/// <param name="maxDistance"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static NPC FindCloestEnemy(Vector2 position, float maxDistance, Func<NPC, bool> predicate)
		{
			float maxDis = maxDistance;
			NPC res = null;
			foreach (var npc in Main.npc.Where(n => n.active && !n.friendly && predicate(n)))
			{
				float dis = Vector2.Distance(position, npc.Center);
				if (dis < maxDis)
				{
					maxDis = dis;
					res = npc;
				}
			}
			return res;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			var modPlayer = player.GetModPlayer<TemplatePlayer>();
			// 玩家死亡会让召唤物消失
			if (player.dead)
			{
				modPlayer.Gliders = false;
			}
			if (modPlayer.Gliders)
			{
				// 如果Gliders不为true那么召唤物弹幕只有两帧可活
				projectile.timeLeft = 2;
			}
			// 弹幕的姿态调整
			projectile.direction = (projectile.spriteDirection = -Math.Sign(projectile.velocity.X));
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			projectile.netUpdate = true;
			NPC npc = FindCloestEnemy(projectile.Center, 1200f, (n) =>
			{
				return n.CanBeChasedBy() &&
				!n.dontTakeDamage && Collision.CanHitLine(projectile.Center, 1, 1, n.Center, 1, 1);
			});
			if (TargetLocation == Vector2.Zero && npc != null && Vector2.Distance(projectile.Center, player.Center) < 700)
			{
				TargetLocation = npc.Center;
			}
			// 如果鼠标没有控制而且周围没有敌人
			if (npc == null && TargetLocation == Vector2.Zero)
			{
				State = 0;
				Timer = 0;
			}


			if (State == 0)
			{
				MoveAroundPlayer(player);
				if (npc != null || TargetLocation != Vector2.Zero) { State = 1; }
			}
			else if (State == 1)
			{
				Timer++;
				Vector2 diff = TargetLocation - projectile.Center;
				float distance = diff.Length();
				diff.Normalize();
				projectile.rotation = diff.ToRotation() + 1.57f;
				// 射击
				if (Timer % 30 < 1)
				{
					Projectile.NewProjectileDirect(projectile.Center + projectile.velocity + diff * 30, diff * 3f, mod.ProjectileType<BlazeBallSmall>(),
						projectile.damage + 5, projectile.knockBack, projectile.owner);
				}
				if (distance > 500)
				{
					projectile.velocity = (projectile.velocity * 20f + diff * 5) / 21f;
				}
				else
				{
					projectile.velocity *= 0.97f;
				}
				// 让召唤物不至于靠的太近
				if (distance > 200)
				{
					projectile.velocity = (projectile.velocity * 40f + diff * 5) / 41f;
				}
				else if (distance < 180)
				{
					projectile.velocity = (projectile.velocity * 20f + diff * -4) / 21f;
				}
				TargetLocation = Vector2.Zero;
			}


			// 召唤物弹幕的后续处理，轨迹，限制等
			if (projectile.velocity.Length() > 16)
			{
				projectile.velocity *= 0.98f;
			}
			if (Math.Abs(projectile.velocity.X) < 0.01f || Math.Abs(projectile.velocity.Y) < 0.01f)
			{
				projectile.velocity = Main.rand.NextVector2Circular(1, 1) * 2f;
			}

			if (projectile.velocity.Length() > 6)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
					MyDustId.OrangeFire, -projectile.velocity.X, -projectile.velocity.Y, 100, Color.Red, 1.0f);
				dust.noGravity = true;
				dust.position = projectile.Center - projectile.velocity;
			}

		}

		/// <summary>
		/// 让召唤物绕着玩家运动
		/// </summary>
		/// <param name="player"></param>
		private void MoveAroundPlayer(Player player)
		{
			Vector2 diff = projectile.Center - player.Center;
			diff.Normalize();
			//diff = diff.RotatedBy(MathHelper.PiOver2);
			projectile.velocity -= diff * 0.2f;

			if (projectile.Center.X < player.Center.X)
			{
				projectile.velocity.X += _nearPlayerSpeed;
			}
			if (projectile.Center.X > player.Center.X)
			{
				projectile.velocity.X -= _nearPlayerSpeed;
			}
			if (projectile.Center.Y < player.Center.Y)
			{
				projectile.velocity.Y += _nearPlayerSpeed;
			}
			if (projectile.Center.Y > player.Center.Y)
			{
				projectile.velocity.Y -= _nearPlayerSpeed;
			}

		}

	}
}
