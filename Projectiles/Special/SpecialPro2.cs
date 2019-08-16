using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using System.Linq;

namespace TemplateMod.Projectiles.Special
{
	public class SpecialPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("神秘弹幕");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = -1;
			projectile.light = 0.1f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.extraUpdates = 4;
			projectile.timeLeft = 120;
		}
		public float Factor
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}
		public float TFactor
		{
			get { return projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}
		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public Vector2 getFront(float fac)
		{
			Vector2 res = new Vector2();
			res.X = projectile.Center.X + projectile.velocity.X * 30 * fac + projectile.velocity.X * (float)Math.Cos(fac * 6) * 15 * fac+ projectile.velocity.Y * (float)Math.Sin(fac * fac) * 15;
			res.Y = projectile.Center.Y + projectile.velocity.Y * 30 * fac + projectile.velocity.Y * (float)Math.Cos(fac * 6) * 15 *fac - projectile.velocity.X * (float)Math.Sin(fac * fac) * 15;
 			return res;
		}
		Vector2 front;
		public override void AI()
		{
			if(projectile.timeLeft > 60)
			{
				Factor = (120 - projectile.timeLeft) / 60f;
			}
			else
			{
				Factor = (projectile.timeLeft) / 60f;
			}
			TFactor = projectile.timeLeft / 120f;
			front = getFront(Factor);
			////var dust = Dust.NewDustDirect(front, 0, 0, MyDustId.Fire, 0, 0, 100);
			////dust.noGravity = true;
			////dust.velocity *= 0;
		}


		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture = Main.magicPixel;
			float tsc = (float)Math.Sqrt(TFactor);
			for (float fac = 0f; fac < Factor; fac += 1 / 120f)
			{
				float sc = 1.0f - fac / Factor;
				Vector2 origin = getFront(fac);
				spriteBatch.Draw(texture, origin - Main.screenPosition,
						new Rectangle(0, 0, 10, 10), Color.White, projectile.velocity.ToRotation(),
						new Vector2(5, 5), sc * tsc, 0, 0);
			}
		}
	}
}

