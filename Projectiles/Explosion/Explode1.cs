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
	public class Explode1 : ModProjectile
	{
		public int Counter
		{
			get
			{
				return (int)projectile.ai[0];
			}
			set
			{
				projectile.ai[0] = value;
			}
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("爆炸");
		}

		public override void SetDefaults()
		{
			projectile.width = 98;
			projectile.height = 98;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 7;
		}

		public override void AI()
		{
			if (Counter >= 6 * Main.projFrames[projectile.type])
			{
				projectile.Kill();
				return;
			}
			if (++projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.hide = true;
				}
			}
			Lighting.AddLight(projectile.Center, 0.9f, 0.8f, 0.6f);
			if (projectile.timeLeft > 58)
			{
				Main.PlaySound(SoundID.Item14, projectile.position);
				projectile.Damage();
			}
			else
			{
				projectile.damage = 0;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			ProjectileExtras.DrawAroundOrigin(projectile.whoAmI, Color.White);
			return false;
		}
	}
}
