using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;


namespace TemplateMod
{
	public class TemplateProjectile : GlobalProjectile
	{
		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			if (projectile.type == ProjectileID.RocketI)
			{
				Main.NewText("On Hit: " + projectile.penetrate);
				Main.NewText("W: " + projectile.width + " H: " + projectile.height);
			}
			base.OnHitNPC(projectile, target, damage, knockback, crit);
		}

		public override void AI(Projectile projectile)
		{
			if (projectile.type == ProjectileID.RocketI)
			{
				Main.NewText("Pen: " + projectile.penetrate);
				Main.NewText("W: " + projectile.width + " H: " + projectile.height);
			}
			base.AI(projectile);
		}

		public override void Kill(Projectile projectile, int timeLeft)
		{
			if (projectile.type == ProjectileID.RocketI)
			{
				Main.NewText("Kill: " + projectile.penetrate);
				Main.NewText("W: " + projectile.width + " H: " + projectile.height);
			}
		}
	}
}
