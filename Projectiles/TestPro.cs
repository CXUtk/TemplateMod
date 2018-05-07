using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;

namespace TemplateMod.Projectiles
{
	class NPCCmp : IComparable<NPCCmp>
	{
		public int Get { get; }
		private float _distance;
		public int Next { get; private set; }
		public NPCCmp(int who, float distance)
		{
			Get = who;
			Next = -1;
			_distance = distance;
		}

		public int CompareTo(NPCCmp other)
		{
			return _distance.CompareTo(other._distance);
		}
	}
	public class TestPro : ModProjectile
	{
		Vector2 origin = new Vector2();
		Vector2 originVel = new Vector2();
		float seed = 0;
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
			projectile.extraUpdates = 100;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
			if (!Main.npc[(int)projectile.ai[1]].active) projectile.Kill();
			if (projectile.ai[0] == 0)
			{
				origin = projectile.Center;
				originVel = Vector2.Normalize(projectile.velocity);
				seed = Main.rand.NextFloat();
			} 
			projectile.ai[0] += 0.32f;
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.RedTrans, 0, 0, 100, Color.White, 2.0f);
			d.position = projectile.Center;
			d.velocity *= 0.2f;
			d.noGravity = true;
			projectile.direction = projectile.position.X < origin.X ? -1 : 1;

			if (projectile.timeLeft < 356)
			{
				projectile.velocity.X = (float)Math.Sin(projectile.ai[0] * 0.88f + projectile.velocity.Y * projectile.velocity.X) * 10 + originVel.X;
				projectile.velocity.Y = (float)Math.Cos(projectile.ai[0] * 0.88f + projectile.velocity.X) * 10 + originVel.Y + seed;

				NPC n = null;
				if (projectile.ai[1] != 0) n = Main.npc[(int)projectile.ai[1]];
				else
				{
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
				}

				if (n != null)
				{
					Vector2 diff = (n.Center + n.velocity) - projectile.Center;
					float factor = 40f;
					factor = 5f + 35f * (diff.Length() / 1000f);

					projectile.velocity = (projectile.velocity * factor + Vector2.Normalize(diff) * 20f) / (factor + 1);
				}
			}

			//Vector2 diff = Vector2.Normalize(projectile.Center - origin);
			//float dis = (projectile.Center - origin).Length();
			//if(projectile.ai[0] < 30)
			//{
			//	projectile.velocity *= 0.95f;
			//}
			//else if (projectile.ai[0] == 30)
			//{
			//	projectile.velocity = diff.RotatedBy(1.57) * 5f * projectile.ai[1];
			//}
			//else if(projectile.ai[0] > 30 && projectile.ai[0] < 180)
			//{
			//	projectile.velocity -= (projectile.velocity.LengthSquared() / dis) * diff * 1.04f;
			//}


		}

		public override bool CanDamage()
		{
			return projectile.timeLeft < 320;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (projectile.ai[1] == 0)
			{
				SortedSet<NPCCmp> NPCPQ = new SortedSet<NPCCmp>();
				foreach (var npc in Main.npc)
				{
					if (npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage) {
						float dis = Vector2.Distance(npc.Center, projectile.Center);
						if (dis < 1000f)
							NPCPQ.Add(new NPCCmp(npc.whoAmI, dis));
					}
				}
				NPC prev = null;
				foreach (var npc in NPCPQ)
				{
					Vector2 diff = Main.npc[npc.Get].Center - (prev == null ? projectile.Center : prev.Center);
					diff.Normalize();
					Projectile.NewProjectile((prev == null ? projectile.Center : prev.Center), diff * 20, projectile.type, projectile.damage, 9f, projectile.owner, 0, npc.Get);
					prev = Main.npc[npc.Get];
				}
			}
			projectile.Kill();

		}
	}
}

