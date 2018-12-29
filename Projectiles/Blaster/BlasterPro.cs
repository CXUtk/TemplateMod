using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using System.Linq;

namespace TemplateMod.Projectiles.Blaster
{
	public class BlasterPro : ModProjectile
	{
		private bool[] visited;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("弹跳能量球");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ChargedBlasterOrb);
			projectile.aiStyle = -1;
			projectile.timeLeft = 420;
			Main.projFrames[projectile.type] = 3;
			projectile.penetrate = -1;
			visited = new bool[Main.npc.Length];
			aiType = ProjectileID.ChargedBlasterCannon;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X * 0.9f;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * 0.9f;
			}
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			visited[target.whoAmI] = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			ProjectileExtras.DrawAroundOrigin(projectile.whoAmI, Color.White);
			return false;
		}

		public override void AI()
		{

			// 粒子效果 
			for (int i = 0; i < 1; i++)
			{
				Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 226, projectile.velocity.X, 0f, 0, default(Color), 1f);
				d.position -= Vector2.One * 3f;
				d.scale = 0.5f;
				d.noGravity = true;
				d.velocity = projectile.velocity / 3f;
				d.alpha = 255 - (int)(255f * projectile.scale);
			}
			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.timeLeft < 400)
			{
				NPC target = null;
				// 最大寻敌距离
				float distanceMax = 500f;
				foreach (NPC npc in Main.npc)
				{
					// 如果npc活着且敌对
					if (npc.active && !npc.friendly && npc.whoAmI >= 0 && !visited[npc.whoAmI]
						&& Collision.CanHitLine(projectile.Center, 1, 1, npc.Center, 1, 1))
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

				//// 如果能被30整除，也就是每0.5秒
				//if(projectile.timeLeft % 30 == 0)
				//{
				//	// 向反方向发射一模一样的弹幕
				//	Projectile.NewProjectile(projectile.position, -projectile.velocity, projectile.type,
				//		projectile.damage, projectile.knockBack, projectile.owner, 1);
				//}
			}
		}
	}
}

