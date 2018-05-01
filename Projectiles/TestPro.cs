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
	public class TestPro : ModProjectile
	{
		Vector2 origin = new Vector2();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("神秘弹幕");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 360;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}
		public override void AI()
		{
			if(projectile.ai[0] == 0)
			{
				origin = projectile.Center;
			}
			projectile.ai[0]++;
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.RedTrans, 0, 0, 100, Color.White, 1.0f);
			d.position = projectile.Center;
			d.velocity *= 0.2f;
			d.noGravity = true;

			Vector2 diff = Vector2.Normalize(projectile.Center - origin);
			float dis = (projectile.Center - origin).Length();
			if(projectile.ai[0] < 30)
			{
				projectile.velocity *= 0.95f;
			}
			else if (projectile.ai[0] == 30)
			{
				projectile.velocity = diff.RotatedBy(1.57) * 5f * projectile.ai[1];
			}
			else if(projectile.ai[0] > 30 && projectile.ai[0] < 180)
			{
				projectile.velocity -= (projectile.velocity.LengthSquared() / dis) * diff * 1.04f;
			}


		}
	}
}

