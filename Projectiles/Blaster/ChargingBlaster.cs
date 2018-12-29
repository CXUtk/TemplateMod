using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using System.Linq;

namespace TemplateMod.Projectiles.Blaster
{
	public class ChargingBlaster : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("蓄力电磁炮");
		}

		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 44;
			projectile.aiStyle = -1;
			projectile.friendly = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.alpha = 255;
			projectile.penetrate = -1;
		}
		public override bool ShouldUpdatePosition()
		{
			return false;
		}
		public override void AI()
		{
			// 核心代码部分
			if(projectile.ai[0] < 200)
				projectile.ai[0]++;
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
				projectile.timeLeft = 2;

				// 模仿原版的粒子效果
				Vector2 unit2 = Vector2.Normalize(Main.MouseWorld - projectile.Center);
				projectile.velocity = unit2;
				projectile.Center = player.Center + unit * 30f;
				int factor = 0;
				if (projectile.ai[0] >= 60f)
				{
					factor++;
				}
				if (projectile.ai[0] >= 180f)
				{
					factor++;
				}
				Vector2 rotVel = (Vector2.UnitX * 18f).RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2)); ;
				Vector2 pos = projectile.Center + rotVel;
				for (int k = 0; k < factor + 1; k++)
				{
					int type = 226;
					float size = 0.4f;
					if (k % 2 == 1)
					{
						type = 226;
						size = 0.65f;
					}
					Vector2 spawnPos = pos + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - (float)(factor * 2));
					Dust d = Dust.NewDustDirect(spawnPos - Vector2.One * 8f, 16, 16, type, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
					d.velocity = Vector2.Normalize(pos - spawnPos) * 1.5f * (10f - (float)factor * 2f) / 10f;
					d.noGravity = true;
					d.scale = size;
					d.customData = player;
				}
			}
			else
			{
				// 如果玩家放弃吟唱就发射弹幕
				Projectile.NewProjectile(projectile.Center, projectile.velocity * 7f, mod.ProjectileType<BlasterPro>(), (int)(projectile.ai[0] / 200f * 200), 4f, projectile.owner);
				return;
			}

		}
	}
}

