using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using System.Linq;

namespace TemplateMod.Projectiles.Special
{
	public class SpecialPro : ModProjectile
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
			projectile.light = 0.1f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 100000;
		}

		private List<Vector2> points = new List<Vector2>();
		private bool[] used = new bool[400];
		private int[] adj = new int[400];
		private float[] mcost = new float[400];
		private const int MAXN = 300;

		public override void AI()
		{
			if(projectile.ai[0] == 0)
			{
				for(int i = 0; i < MAXN; i++)
				{
					points.Add(projectile.Center + (Main.rand.NextFloatDirection() * 6.28f).ToRotationVector2() * Main.rand.Next(3, 180));
				}
			}
			projectile.ai[0] = 1;
			if (Main.time % 5 < 1)
			{
				for (int i = 0; i < MAXN; i++)
				{
					points[i] = points[i] + (Main.rand.NextFloatDirection() * 6.28f).ToRotationVector2() * Main.rand.Next(1, 5);
				}
			}
			for (int i = 0; i < MAXN; i++)
			{
				used[i] = false;
				mcost[i] = float.PositiveInfinity;
			}
			mcost[0] = 0;
			while (true)
			{
				int v = -1;
				for (int i = 0; i < MAXN; i++)
				{
					if (!used[i] && (v == -1 || mcost[i] < mcost[v])) v = i;
				}
				if (v == -1) break;
				used[v] = true;
				for (int i = 0; i < MAXN; i++)
				{
					if (used[i]) continue;
					float d = Vector2.DistanceSquared(points[v], points[i]);
					if (d < mcost[i])
					{
						mcost[i] = d;
						adj[i] = v;
					}
				}
			}

			if (Main.time % 2 < 1)
			{
				for(int j = 0; j < MAXN; j++)
				{
					Vector2 s = points[j];
					Vector2 t = points[adj[j]];
					float dis = (t - s).Length();
					Vector2 unit = Vector2.Normalize(t - s);
					for (int i = 0; i < dis; i += 6)
					{
						var d = Dust.NewDustDirect(s + unit * i, 0, 0, MyDustId.WhiteLingering, 0, 0, 100, Color.White, 0.85f);
						d.noGravity = true;
						d.position = s + unit * i;
						d.velocity *= 0;
					}
				}
			}

		}
	}
}

