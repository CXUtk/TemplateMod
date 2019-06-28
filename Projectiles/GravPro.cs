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
	public class GravPro : ModProjectile
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
		}


		public override void AI()
		{
			projectile.velocity.Y += 0.4f;
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.OrangeShortFx, 0, 0, 100, Color.White, 2f);
			d.position = projectile.Center;
			d.velocity *= 0.0f;
			d.noGravity = true;

		}
	}
}

