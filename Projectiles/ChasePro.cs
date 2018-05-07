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
	public class ChasePro : ModProjectile
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
			projectile.friendly = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 400;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}
		public override void AI()
		{
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.OrangeShortFx, 0, 0, 100, Color.White, 2.5f);
			d.position = projectile.Center;
			d.velocity *= 0.0f;
			d.noGravity = true;
			if(projectile.timeLeft < 330)
			{
				projectile.velocity *= 0.95f;
			}

			if (projectile.timeLeft < 300)
			{
				NPC n = null;
				float distanceMax = 1000f;
				foreach (NPC npc in Main.npc)
				{
					if (npc.active && !npc.friendly && !npc.dontTakeDamage)
					{
						float currentDistance = Vector2.Distance(npc.Center, projectile.Center);
						if (currentDistance < distanceMax)
						{
							distanceMax = currentDistance;
							n = npc;
						}
					}
				}
				if (n != null)
				{
					Vector2 diff = (n.Center + n.velocity) - projectile.Center;
					float factor = 40f;
					factor = 5f + 35f * (diff.Length() / 1000f);

					projectile.velocity = (projectile.velocity * factor + Vector2.Normalize(diff) * 20f) / (factor + 1);
					if (diff.Length() < 50)
					{
						projectile.velocity = Vector2.Normalize(projectile.velocity) * 20f;
						projectile.velocity += Main.rand.NextVector2Circular(15, 15);
					}
				}
			}
		}
	}
}

