using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod.Projectiles.Summoner
{
	public class BlazeBallSmall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("火球");
		}
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.timeLeft = 180;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.extraUpdates = 5;
			projectile.scale = 0.5f;
			Main.projFrames[projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void AI()
		{
			for (int i = 1; i <= 3; i++)
			{
				Dust d = Dust.NewDustDirect(projectile.position - new Vector2(4, 4), projectile.width + 8, projectile.height + 8, DustID.Fire, 0,
					0, 100, Color.White, 1.2f);
				d.position = projectile.Center - projectile.velocity * i / 3f;
				d.velocity *= 0;
				d.noGravity = true;
			}

			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
			++projectile.frameCounter;
			if (projectile.frameCounter > 6)
			{
				projectile.frame = (projectile.frame + 1) % Main.projFrames[projectile.type];
				projectile.frameCounter = 0;
			}

			if (projectile.timeLeft < 176)
			{
				NPC n = null;
				float maxDis = 1250f;
				foreach (var npc in Main.npc)
				{
					if (npc.active && !npc.friendly && !npc.dontTakeDamage)
					{
						if (npc.Hitbox.Intersects(projectile.Hitbox))
						{
							projectile.Kill();
							return;
						}
						float dis = npc.Distance(projectile.Center);
						if (maxDis > dis)
						{
							maxDis = dis;
							n = npc;
						}
					}
				}
				if (n != null)
				{
					Vector2 unit = Vector2.Normalize(n.Center - projectile.Center);
					float factor = 40f;
					projectile.velocity = (projectile.velocity * factor + unit * 5f) / (factor + 1f);
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 vector = new Vector2(texture2D.Width * 0.5f, texture2D.Height / Main.projFrames[projectile.type] * 0.5f);
			SpriteEffects spriteEffects = (projectile.direction == -1) ? SpriteEffects.FlipHorizontally : 0;
			var frame = texture2D.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);

			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Color color = Color.White * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length) * 0.6f;
				Main.spriteBatch.Draw(texture2D, projectile.oldPos[k] + projectile.Size * 0.5f - Main.screenPosition, frame,
					color, projectile.rotation, vector, projectile.scale, spriteEffects, 0f);
			}
			Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition, frame, Color.White, projectile.rotation, vector, projectile.scale, spriteEffects, 0f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, mod.ProjectileType<Explode1Immune>(), projectile.damage, 3f, projectile.owner, 0, 1.0f + Main.rand.NextFloat() * 0.6f);
		}

	}
}
