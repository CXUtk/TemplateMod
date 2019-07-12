using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TemplateMod.UI.Component;
using TemplateMod.VecMap;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TemplateMod
{
	public class TemplateWorld : ModWorld
	{
		public override void PostUpdate()
		{
			//TemplateMod._effectManager.Update();
			//TemplateMod._twistEffectManager.Update();
			base.PostUpdate();
		}

		private bool valid(int x, int y)
		{
			return x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY;
		}

		private void PlaceCross(int x, int y)
		{
			int[] dx = { 1, -1, 0, 0 };
			int[] dy = { 0, 0, 1, -1 };
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (!valid(x + dx[i] * j, y + dy[i] * j)) continue;
					Main.tile[x + dx[i] * j, y + dy[i] * j].type = (ushort)mod.TileType("TemplateOre");
					// 注意，这里active(true)会让物块凭空生成
					Main.tile[x + dx[i] * j, y + dy[i] * j].active(true);
				}
			}
		}
		private void Swap<T>(ref T a, ref T b)
		{
			T c = a;
			a = b;
			b = c;
		}

		private void TileLine(Microsoft.Xna.Framework.Point start, Microsoft.Xna.Framework.Point end, int type)
		{
			int x1 = start.X, y1 = start.Y;
			int x2 = end.X, y2 = end.Y;

			bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
			if (steep)
			{
				// 反转坐标轴
				Swap<int>(ref x1, ref y1);
				Swap<int>(ref x2, ref y2);
			}
			if (x1 > x2)
			{
				Swap<int>(ref x1, ref x2);
				Swap<int>(ref y1, ref y2);
			}
			int dx = x2 - x1;
			int dy = Math.Abs(y2 - y1);
			int error = 0;
			int ystep = (y1 < y2) ? 1 : -1;
			int y = y1;
			for (int x = x1; x <= x2; ++x)
			{
				if (steep)
				{
					if (valid(y, x))
					{
						Main.tile[y, x].type = (ushort)type;
						Main.tile[y, x].active();
					}
				}
				else
				{
					if (valid(x, y))
					{
						Main.tile[x, y].type = (ushort)type;
						Main.tile[x, y].active();
					}
				}
				if (2 * (error + dy) < dx)
					error += dy;
				else
				{
					y += ystep;
					error = error + dy - dx;
				}
			}
		}

		private void SetTile(int x, int y, int type)
		{
			if (valid(x, y))
			{
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x, y].active();
			}
		}

		private void PlaceStar(int x, int y)
		{
			//Microsoft.Xna.Framework.Point start = new Microsoft.Xna.Framework.Point(x, y);
			//var list = LoadVec.GetVecMap(x % 2==0 ? "裙":"小");
			//foreach (var p in list)
			//{
			//	SetTile((int)Math.Round(x + p.X * 16), (int)Math.Round(y + p.Y * 16), mod.TileType("TemplateOre"));
			//}
			////for(int r = 0; r < 8; r++)
			////{
			////	rad = r * MathHelper.PiOver4;
			////	Vector2 endvec = start.ToVector2() + r.ToRotationVector2() * 10;
			////	TileLine(start, new Point((int)endvec.X, (int)endvec.Y), mod.TileType("TemplateOre"));
			////}
		}

		private void GenNormalOres(GenerationProgress progress)
		{
			progress.Message = "生成普通矿物中……";
			// 生成数量是地图总物块数量的0.006%
			int size = (int)(Main.maxTilesX * Main.maxTilesY * 9E-04);
			for (int k = 0; k < size; k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY); // WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.

				// 如果在地狱层以上
				if (y < WorldGen.rockLayer)
				{
					// 原版自带矿物生成函数strength代表的是这个生成区域的最大大小，steps代表这个生成区域连接的紧密程度
					WorldGen.TileRunner(x, y, Main.rand.Next(2, 5), Main.rand.Next(2, 5), mod.TileType("TemplateOre"), false, 0f, 0f, false, true);
				}
				else
				{

						PlaceCross(x, y);

				}
				// Alternately, we could check the tile already present in the coordinate we are interested. Wrapping WorldGen.TileRunner in the following condition would make the ore only generate in Snow.
				// Tile tile = Framing.GetTileSafely(x, y);
				// if (tile.active() && tile.type == TileID.SnowBlock)
				// {
				// 	WorldGen.TileRunner(.....);
				// }
				progress.Value = k / (float)size;
			}
		}

		private void ReplaceToIceWorld(GenerationProgress progress)
		{
			progress.Message = "正在将泥土块替换为雪块……";
			// 暴力替换过程（太暴力了(*/ω＼*)
			for (int i = 50; i < Main.maxTilesX - 50; i++)
			{
				for (int j = 0; j < Main.maxTilesY - 250; j++)
				{
					Tile tile = Framing.GetTileSafely(i, j);
					if (tile.type == TileID.Dirt || tile.type == TileID.Grass)
					{
						tile.type = TileID.Gold;
						tile.active();
					}
					if (tile.type == TileID.Stone || tile.type == TileID.ClayBlock)
					{
						tile.type = TileID.Silver;
						tile.active();
					}
				}
				progress.Value = i / (float)Main.maxTilesX;
			}
		}

		public override void PostDrawTiles()
		{
		
			var spriteBatch = Main.spriteBatch;
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
			if (TemplateMod.SelectMode)
			{
				spriteBatch.Draw(Main.magicPixel, TemplateMod.SelectUpperLeft.ToVector2() * 16 - Main.screenPosition, new Rectangle(0, 0, 16, 16), Color.Purple * 0.7f);
				spriteBatch.Draw(Main.magicPixel, TemplateMod.SelectLowerRight.ToVector2() * 16 - Main.screenPosition, new Rectangle(0, 0, 16, 16), Color.Red * 0.7f);

				Rectangle targetRect = new Rectangle((int)((int)TemplateMod.SelectUpperLeft.X * 16 - Main.screenPosition.X), (int)((int)TemplateMod.SelectUpperLeft.Y * 16 - Main.screenPosition.Y),
					(int)(TemplateMod.SelectLowerRight.X - TemplateMod.SelectUpperLeft.X + 1) * 16, (int)(TemplateMod.SelectLowerRight.Y - TemplateMod.SelectUpperLeft.Y + 1) * 16);
				Drawing.DrawAdvBox(spriteBatch, targetRect, Color.Yellow * 0.5f, TemplateMod.ModTexturesTable["Box"], new Vector2(8, 8));
			}
			if (TemplateMod.BuildMode)
			{

			}
			spriteBatch.End();
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int genoreLayer = tasks.FindIndex((pass) => pass.Name.Equals("Shinies"));
			if (genoreLayer != -1)
			{
				tasks.Insert(genoreLayer + 1, new PassLegacy("Template:GenNormalOre", GenNormalOres));
				//tasks.Add(new PassLegacy("Template:IceWorld", ReplaceToIceWorld));
				//tasks.Insert(genoreLayer + 1, new PassLegacy("Template:GenNormalOre", GenNormalOres));
			}
		}
	}
}

