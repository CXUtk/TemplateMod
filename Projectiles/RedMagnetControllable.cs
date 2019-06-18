using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using System.Linq;

namespace TemplateMod.Projectiles
{
	public class RedMagnetControllable : ModProjectile
	{
		float baseRot = 0.0f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("可控猩红磁暴球");
		}
		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 44;
			projectile.aiStyle = -1;
			projectile.timeLeft = 600;
			projectile.friendly = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 5;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
		}

		// 接近实体物块的时候逐渐消失
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.tileCollide = false;
			projectile.velocity = oldVelocity;
			if (projectile.timeLeft > 30)
				projectile.timeLeft = 30;
			return false;
		}

		public override void AI()
		{
			if (projectile.timeLeft < 30)
				projectile.alpha += 10;
			// 核心代码部分

			// 如果玩家仍然在控制弹幕
			if (Main.player[projectile.owner].channel)
			{
				// 获取弹幕持有者
				Player player = Main.player[projectile.owner];

				// 从玩家到达鼠标位置的单位向量
				Vector2 unit = Vector2.Normalize(Main.MouseWorld - player.Center);
				// 随机角度
				float rotaion = unit.ToRotation();
				// 调整玩家转向以及手持物品的转动方向
				player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
				player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
					rotaion.ToRotationVector2().X * player.direction);
				player.itemTime = 2;
				player.itemAnimation = 2;
				// 从弹幕到达鼠标位置的单位向量
				Vector2 unit2 = Vector2.Normalize(Main.MouseWorld - projectile.Center);
				// 让弹幕缓慢朝鼠标方向移动
				if (Vector2.Distance(projectile.Center, Main.MouseWorld) < 5)
				{
					projectile.velocity *= 0;
					projectile.Center = Main.MouseWorld;
				}
				else
				{
					projectile.velocity = unit2 * 5;
				}
			}
			else
			{
				// 如果玩家放弃吟唱就慢慢消失
				if (projectile.timeLeft > 30)
					projectile.timeLeft = 30;
				// 返回函数这样就不会执行下面的攻击代码
				return;
			}

			// 累加帧计时器
			projectile.frameCounter++;
			// 当计时器经过了7帧
			if (projectile.frameCounter % 7 == 0)
			{
				// 重置计时器
				projectile.frameCounter = 0;
				// 选择下一帧动画
				// 让弹幕的帧与等于与5进行模运算，也就是除以5的余数
				projectile.frame++;
				projectile.frame %= 5;
			}

			NPC target = null;
			// 最大寻敌距离
			float distanceMax = 400f;
			foreach (NPC npc in Main.npc)
			{
				// 如果npc活着且敌对
				if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy
					&& Collision.CanHit(projectile.Center, 1, 1, npc.position, npc.width, npc.height))
				{
					// 计算距离
					float currentDistance = Vector2.Distance(npc.Center, projectile.Center);
					// 如果npc距离比当前最大距离小
					if (currentDistance < distanceMax)
					{
						// 就把最大距离设置为npc和玩家的距离
						// 并且暂时选取这个npc为距离最近npc
						distanceMax = currentDistance;
						target = npc;
					}
				}
			}
			baseRot += 0.15f;
			// 如果找到符合条件的npc， 并且符合开火间隔（一秒6次）
			if (target != null && projectile.timeLeft % 10 < 1)
			{
				Vector2 toTarget = target.Center - projectile.Center;
				toTarget.Normalize();
				toTarget *= 6f;

				toTarget = toTarget.RotatedBy(baseRot);
				for (int i = 0; i < 3; i++)
				{
					toTarget = toTarget.RotatedBy(MathHelper.Pi / 1.5f);
					// 我调整了一下发射位置，这样射线看起来更像从磁球中间射出来的
					Projectile.NewProjectile(projectile.Center + projectile.velocity * 4f,
								toTarget, mod.ProjectileType("ElectricPro"), 100, 5f, projectile.owner, target.whoAmI);
				}
			}

		}
	}
}

