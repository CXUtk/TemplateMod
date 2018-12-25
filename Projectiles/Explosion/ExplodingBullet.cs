using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using System.Linq;

namespace TemplateMod.Projectiles.Explosion
{
	public class ExplodingBullet : ModProjectile
	{
		private float[] dx = { 1.5f, 1.5f, -1.5f, -1.5f };
		private float[] dy = { -1.5f, 1.5f, -1.5f, 1.5f };
		private int damage;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("爆炸子弹");
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.light = 0.5f;
			projectile.timeLeft = 400;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			// 高速飞行
			projectile.extraUpdates = 7;
			projectile.penetrate = -1;
			projectile.ranged = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if(projectile.alpha > 140)
			{
				return Color.Transparent;
			}
			return new Color(255, 255, 255, 100) * projectile.Opacity;
		}

		public override void AI()
		{
			// 注意这里保存了弹幕的伤害
			damage = projectile.damage;
			if (projectile.alpha > 0)
			{
				// 渐变出现
				projectile.alpha -= 10;
			}

			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;

			foreach (var npc in Main.npc)
			{
				if (npc.active && !npc.friendly && !npc.dontTakeDamage)
				{
					if (npc.Hitbox.Intersects(projectile.Hitbox))
					{
						// 将弹幕当前伤害设置为0并且立即杀死弹幕
						projectile.damage = 0;
						projectile.Kill();
						return;
					}
				}
			}
		}
		
		public override void Kill(int timeLeft)
		{
			// 注意是这里的伤害是之前保存的伤害
			Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, 
				mod.ProjectileType("Explode1"), damage, 10f, projectile.owner);
			projectile.damage = 0;

			// 原版爆炸粒子
			//for (int i = 0; i < 50; i++)
			//{
			//	var dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y),
			//		projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
			//	dust.velocity *= 1.4f;
			//}
			//for (int i = 0; i < 80; i++)
			//{
			//	var dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y),
			//		projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
			//	dust.noGravity = true;
			//	dust.velocity *= 5f;
			//	dust = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y),
			//		projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			//	dust.velocity *= 3f;
			//}
			//for (int i = 0; i < 4; i++)
			//{
			//	var gore = Gore.NewGoreDirect(new Vector2(projectile.position.X + projectile.width / 2 - 24f,
			//		projectile.position.Y + projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			//	gore.scale = 1.5f;
			//	gore.velocity.X = gore.velocity.X + dx[i];
			//	gore.velocity.Y = gore.velocity.Y + dy[i];
			//}
		}
	}
}

