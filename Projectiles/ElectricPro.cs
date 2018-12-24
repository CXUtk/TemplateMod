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
	public class ElectricPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("猩红电流");
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.timeLeft = 200;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 100;
		}
		public override void AI()
		{
			// 发出红光
			Lighting.AddLight(projectile.position, 0.5f, 0.0f, 0.0f);

			// 线性粒子效果
			for (int i = 0; i < 3; i++)
			{
				Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.RedTransBubble, 0, 0, 100, Color.Red, 1f);
				d.position = projectile.Center - projectile.velocity * i / 3f;
				d.velocity *= 0.2f;
				d.noGravity = true;
				d.scale = (float)Main.rand.Next(90, 110) * 0.014f;
			}

			// 获取目标NPC
			NPC target = Main.npc[(int)projectile.ai[0]];
			// 如果敌对npc是活着的
			if (target.active)
			{
				// 计算朝向目标的向量
				Vector2 targetVec = target.Center - projectile.Center;
				float dis = targetVec.Length() / 300f;
				float factor = MathHelper.Lerp(0f, 40f, (float)Math.Sqrt(dis));
				targetVec.Normalize();
				// 目标向量是朝向目标的大小为20的向量
				targetVec *= 6f;
				// 朝向npc的单位向量*20 + 3.33%偏移量
				projectile.velocity = (projectile.velocity * factor + targetVec) / (factor + 1f);
				if (projectile.velocity.Length() < 16)
					projectile.velocity *= 1.02f;
			}

		}
	}
}

