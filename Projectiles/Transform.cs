using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;

namespace TemplateMod.Projectiles
{
	public class Transform : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("变换器");
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.timeLeft = 2;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.scale = 0.5f;
		}

		public override void AI()
		{
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width,
				projectile.height, MyDustId.OrangeFire2, 0, 0, 100, Color.White, 0.5f);
			d.noGravity = true;
			d.position = projectile.Center;
			d.velocity *= 0;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

	}
}
