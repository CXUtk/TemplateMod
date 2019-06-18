using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod.Projectiles.Summoner
{
	public class Explode1Immune : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("爆炸");
		}
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			Main.projFrames[projectile.type] = 7;
		}
		public override void Kill(int timeLeft)
		{


		}

		public override void AI()
		{
			projectile.ai[1] += 0.01f;
			projectile.scale = projectile.ai[1] * 0.8f;
			projectile.ai[0] += 1f;
			if (projectile.ai[0] >= 6 * Main.projFrames[projectile.type])
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
			projectile.alpha -= 50;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			Lighting.AddLight(projectile.Center, 0.9f, 0.8f, 0.6f);
			if (projectile.ai[0] == 1f)
			{
				Main.PlaySound(SoundID.Item14, projectile.position);
				projectile.position = projectile.Center;
				projectile.width = (projectile.height = (int)(52f * projectile.scale));
				projectile.Center = projectile.position;
				projectile.Damage();
			}
			else
			{
				projectile.damage = 0;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.localNPCImmunity[target.whoAmI] = -1;
			target.immune[projectile.owner] = 1;
		}


		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			ProjectileExtras.DrawAroundOrigin(projectile.whoAmI, Color.White);
			return false;
		}
		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300, false);
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300);
		}
	}
}
