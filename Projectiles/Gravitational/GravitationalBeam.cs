using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using System.Linq;

namespace TemplateMod.Projectiles.Gravitational
{
	public class GravitationalBeam : ModProjectile
	{
		private float[] dx = { 1.5f, 1.5f, -1.5f, -1.5f };
		private float[] dy = { -1.5f, 1.5f, -1.5f, 1.5f };
		private int damage;
		private Vector2[] posArray = new Vector2[300];

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("引力波");
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.penetrate = -1;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			bool onEnemy = false;
			NPC target1 = null;
			if (player.channel)
			{
				Vector2 unit = Vector2.Normalize(Main.MouseWorld - player.Center);
				// 随机角度
				float rotaion = unit.ToRotation();
				// 调整玩家转向以及手持物品的转动方向
				player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
				player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
					rotaion.ToRotationVector2().X * player.direction);
				player.itemTime = 2;
				player.itemAnimation = 2;
				projectile.damage = 0;
				projectile.timeLeft = 2;
				projectile.Center = player.Center;
				projectile.velocity = Vector2.Normalize(Main.MouseWorld - projectile.Center);
				Vector2 current = projectile.Center;
				posArray[0] = projectile.Center;
				Vector2 unitDir = projectile.velocity;
				for (int i = 1; i < posArray.Length; i++)
				{
					NPC target = null;
					float distanceMax = 500f;
					foreach (NPC npc in Main.npc)
					{
						if (npc.active && !npc.friendly)
						{
							float currentDistance = Vector2.Distance(npc.Center, current);
							if (currentDistance < distanceMax)
							{
								distanceMax = currentDistance;
								target = npc;
							}
						}
					}
					if (target != null)
					{
						Vector2 targetVec = target.Center - current;
						float dis = targetVec.Length() / 500f;
						float factor = MathHelper.Lerp(0f, 40f, (float)Math.Sqrt(dis));
						targetVec.Normalize();
						targetVec *= 3f;
						unitDir = (unitDir * factor + targetVec) / (factor + 1f);
						if (Vector2.Distance(current, target.Center) < 5)
						{
							onEnemy = true;
							target1 = target;
						}
					}
					current += unitDir;
					posArray[i] = current;

				}
			}
			else
			{
				//projectile.Kill();
			}
			if (onEnemy)
			{
				target1.GetGlobalNPC<TemplateNPC>().LockNPC();
				target1.velocity = Vector2.Normalize(Main.MouseWorld - target1.Center) * 11f;
				if (Vector2.Distance(target1.Center, Main.MouseWorld) < 11)
				{
					target1.velocity *= 0;
					target1.Center = Main.MouseWorld;
				}
				return;
			}
			if (player.altFunctionUse == 2)
			{
				for (int i = 1; i < posArray.Length; i += 10)
				{
					Projectile.NewProjectileDirect(posArray[i], Vector2.Zero, mod.ProjectileType<Explosion.Explode1>(), 1000, 10f, projectile.owner);
				}
				projectile.Kill();
				player.reuseDelay = 40;
			}
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			for(int i = 0; i < posArray.Length; i++)
			{
				spriteBatch.Draw(Main.magicPixel, posArray[i] - Main.screenPosition, 
					new Rectangle(0, 0, 1, 1), Color.Orange * 0.85f, 0f, Vector2.One * 0.5f, 2f, SpriteEffects.None, 0f);
			}
			return false;
		}

		//public override void Kill(int timeLeft)
		//{
		//	// 注意是这里的伤害是之前保存的伤害
		//	Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, 
		//		mod.ProjectileType("Explode1"), damage, 10f, projectile.owner);
		//	projectile.damage = 0;

		//	// 原版爆炸粒子
		//	//for (int i = 0; i < 50; i++)
		//	//{
		//	//	var dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y),
		//	//		projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
		//	//	dust.velocity *= 1.4f;
		//	//}
		//	//for (int i = 0; i < 80; i++)
		//	//{
		//	//	var dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y),
		//	//		projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
		//	//	dust.noGravity = true;
		//	//	dust.velocity *= 5f;
		//	//	dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y),
		//	//		projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
		//	//	dust.velocity *= 3f;
		//	//}
		//	//for (int i = 0; i < 4; i++)
		//	//{
		//	//	var gore = Gore.NewGoreDirect(new Vector2(projectile.position.X + projectile.width / 2 - 24f,
		//	//		projectile.position.Y + projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
		//	//	gore.scale = 1.5f;
		//	//	gore.velocity.X = gore.velocity.X + dx[i];
		//	//	gore.velocity.Y = gore.velocity.Y + dy[i];
		//	//}
		//}
	}
}

