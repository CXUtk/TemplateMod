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
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			base.OnHitNPC(target, damage, knockback, crit);
		}
		public override void AI()
		{
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.OrangeShortFx, 0, 0, 100, Color.White, 2.5f);
			d.position = projectile.Center;
			d.velocity *= 0.0f;
			d.noGravity = true;

			if (projectile.timeLeft < 400)
			{
				NPC target = null;
				// 最大寻敌距离
				float distanceMax = 300f;
				foreach (NPC npc in Main.npc)
				{
					// 如果npc活着且敌对
					if (npc.active && !npc.friendly)
					{
						// 计算距离
						float currentDistance = Vector2.Distance(npc.Center, projectile.Center);
						// 如果npc距离比当前最大距离小
						if (currentDistance < distanceMax)
						{
							// 就把最大距离设置为npc和玩家的距离
							// 并且暂时选取这个npc为距离最近npc
							distanceMax = currentDistance;
							target = npc;
						}
					}
				}
				// 如果找到符合条件的npc
				if (target != null)
				{
					// 计算朝向目标的向量
					Vector2 targetVec = target.Center  - projectile.Center;
					float dis = targetVec.Length() / 300f;
					float factor = MathHelper.Lerp(0f, 40f, (float)Math.Sqrt(dis));
					targetVec.Normalize();
					// 目标向量是朝向目标的大小为20的向量
					targetVec *= 10f;
					// 朝向npc的单位向量*20 + 3.33%偏移量
					projectile.velocity = (projectile.velocity * factor + targetVec) / (factor + 1f);
				}
				//float factor = 40f;
				//factor = 5f + 35f * (diff.Length() / 1000f);
				//if (diff.Length() < 50)
				//{
				//	projectile.velocity = Vector2.Normalize(projectile.velocity) * 20f;
				//	projectile.velocity += Main.rand.NextVector2Circular(15, 15);
				//}

				// 如果能被30整除，也就是每0.5秒
				if(projectile.timeLeft % 30 == 0)
				{
					// 向反方向发射一模一样的弹幕
					Projectile.NewProjectile(projectile.position, -projectile.velocity, projectile.type,
						projectile.damage, projectile.knockBack, projectile.owner, 1);
				}
			}
		}
	}
}

