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
		public int NPCID { get; }
		public NPCCmp(int who)
		{
			NPCID = who;
		}

		public int CompareTo(NPCCmp other)
		{
			return 1;
		}
	}
	public class TestPro : ModProjectile
	{
		Vector2 origin = new Vector2();
		Vector2 originVel = new Vector2();
		Vector2 prevNode = Vector2.Zero;
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
			projectile.timeLeft = 360;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 100;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.8f, 0.0f, 0.0f);
			if (projectile.ai[1] != 0 && !Main.npc[(int)projectile.ai[1] - 1].active) projectile.Kill();
			if (projectile.ai[0] == 0)
			{
				origin = projectile.Center;
				originVel = Vector2.Normalize(projectile.velocity);
				seed = Main.rand.NextFloat();
			} 
			projectile.ai[0] += 0.32f;
			if(prevNode == Vector2.Zero)
			{
				prevNode = projectile.Center;
			}
			if (projectile.timeLeft % 5 < 1)
			{
				float distance = Vector2.Distance(prevNode, projectile.Center);
				for (float i = 0; i <= distance; i += 2.0f)
				{
					Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.RedTrans, 0, 0, 100, Color.White, 1.4f);
					d.position = Vector2.Lerp(prevNode, projectile.Center, i / distance);
					d.velocity *= 0.0f;
					d.noGravity = true;
				}
				prevNode = projectile.Center;
			}
			projectile.direction = projectile.position.X < origin.X ? -1 : 1;


			if (projectile.timeLeft < 356)
			{
				projectile.velocity.X = (float)Math.Sin(projectile.ai[0] * 0.88f + projectile.velocity.Y * projectile.velocity.X) * 10 + originVel.X;
				projectile.velocity.Y = (float)Math.Cos(projectile.ai[0] * 0.88f + projectile.velocity.X) * 10 + originVel.Y + seed;

				NPC n = null;
				if (projectile.ai[1] != 0)
					n = Main.npc[(int)projectile.ai[1] - 1];
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

		}

		public override bool CanDamage()
		{
			return projectile.timeLeft < 355;
		}


		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.localNPCImmunity[target.whoAmI] = -1;
			target.immune[projectile.owner] = 5;
			TemplateMod._effectManager.Insert(target.Center);
			if (projectile.ai[1] == 0)
			{
				TemplateMod.TwistedStrength = 3.14f;
				TemplateMod._twistEffectManager.Insert(target.Center);
				HashSet<NPC> npcSet = new HashSet<NPC>();
				NPC closest = null;
				float maxDist = 1000f;
				foreach (var npc in Main.npc)
				{
					if (npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage && npc.whoAmI != target.whoAmI)
					{
						npcSet.Add(npc);
						float dis = Vector2.Distance(npc.Center, projectile.Center);
						if(dis < maxDist)
						{
							maxDist = dis;
							closest = npc;
						}
					}
				}
				if (closest != null)
				{
					Vector2 unit = Vector2.Normalize(closest.Center - target.Center);
					Projectile.NewProjectile(target.Center, unit * 5, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0, closest.whoAmI + 1);
					npcSet.Remove(closest);
					NPC cloest2 = null;
					NPC prev = closest;
					do
					{
						cloest2 = null;
						float maxDist2 = 300f;
						foreach (var npc in npcSet)
						{
							float dis = Vector2.Distance(npc.Center, prev.Center);
							if (dis < maxDist2)
							{
								maxDist2 = dis;
								cloest2 = npc;
							}
						}
						if(cloest2 != null)
						{
							Vector2 dir = Vector2.Normalize(cloest2.Center - prev.Center);
							Projectile.NewProjectile(prev.Center, dir * 5, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0, cloest2.whoAmI + 1);
							prev = cloest2;
							npcSet.Remove(cloest2);

						}
					} while (cloest2 != null && npcSet.Count != 0);
				}
			}
			projectile.Kill();

		}
	}
}

