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
	public class DarkLancePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("诡异长矛");
		}
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.light = 0.3f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.extraUpdates = 4;
			projectile.ownerHitCheck = true;
		}


		// 老样子，弄个计时器
		public int Timer
		{
			get { return (int)projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}

		// 选择手动设定位置
		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void AI()
		{
			// 因为长矛贴图是斜着的所以我们只需要旋转45度（很重要
			projectile.spriteDirection = projectile.direction;

			//if (projectile.spriteDirection == -1)
			//{
			//	projectile.rotation -= 1.57f;
			//}

			// 要点1：长矛在玩家使用完物品后一定要消失
			var owner = Main.player[projectile.owner];
			if (Timer >= owner.itemAnimationMax * (projectile.MaxUpdates))
			{
				projectile.Kill();
				return;
			}
			Vector2 unit = Vector2.Normalize(projectile.velocity);
			projectile.velocity = unit;
			


			float factor = 0;
			if (Timer < owner.itemAnimationMax / 2 * (projectile.MaxUpdates))
			{
				var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.Fire, 0, 0, 100,
				Color.White, 2f);
				dust.position = projectile.Center;
				dust.velocity *= 0f;
				dust.noGravity = true;
				dust.fadeIn = 1f;
				factor = (Timer + 1) / (owner.itemAnimationMax / 2.0f * (projectile.MaxUpdates));
				projectile.velocity = unit.RotatedBy((float)Math.Sin(factor * 20f * projectile.direction) * 0.8f);
				projectile.Center = owner.RotatedRelativePoint(owner.MountedCenter, true) + unit * 20 + projectile.velocity * 50f * factor;
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
				projectile.velocity = unit;
			}
			// 要点2：长矛在一段时间后回退
			else
			{
				factor = (owner.itemAnimationMax * (projectile.MaxUpdates) - Timer) / (owner.itemAnimationMax * (projectile.MaxUpdates) - owner.itemAnimationMax / 2.0f * (projectile.MaxUpdates));
				projectile.Center = owner.RotatedRelativePoint(owner.MountedCenter, true) + unit * 20 + factor * projectile.velocity * 50f;
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
			}


			Timer++;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 unit = Vector2.Normalize(projectile.velocity);
			ProjectileExtras.DrawAroundOrigin(projectile.Center - unit * 30, projectile, Color.White, SpriteEffects.None);
			return false;
		}
	}
}

