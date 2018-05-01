using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TemplateMod.Projectiles
{
	public class Generator : ModProjectile
	{
		Vector2 origin = new Vector2();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("弹幕发生器");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 120;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}
		public override void AI()
		{
			if(projectile.timeLeft % 20 == 1)
			{
				for (float i = 0.0f; i < MathHelper.TwoPi; i += MathHelper.Pi / 3.0f / (4 - projectile.timeLeft / 30))
				{
					Vector2 dir = i.ToRotationVector2();
					Projectile.NewProjectile(projectile.Center, dir * (6 + 3 * (4 - projectile.timeLeft / 30)),
						mod.ProjectileType("TestPro"), 100, 10, projectile.owner, 0, (4 - projectile.timeLeft / 30 + 1) * 0.5f);
				}
			}
		}
	}
}

