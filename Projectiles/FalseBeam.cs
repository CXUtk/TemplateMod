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
    public class FalseBeam : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("假剑气");
		}
		public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
			projectile.scale = 1.5f;
            projectile.friendly = true;
			projectile.hostile = true;
			projectile.melee = true;
            projectile.timeLeft = 600;
			projectile.ignoreWater = true;
			//aiType = ProjectileID.TerraBeam;
			projectile.aiStyle = 27;
        }

		public override void AI()
		{

			Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height
				, MyDustId.Fire, 0f, 0f, 100, default(Color), 3f);
			dust.noGravity = true;
		}
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
                    MyDustId.PinkBrightBubble, 0, 0, 100, Color.White, 1.5f);
                d.noGravity = true;
                d.velocity *= 2;
            }
        }


	}
}
