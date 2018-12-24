using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using TemplateMod.Utils;
using Terraria.DataStructures;
using Terraria.Enums;

namespace TemplateMod.Tiles
{
	public class Lantern : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			// 物块含有边缘高亮线
			TileID.Sets.HasOutlines[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };

			// 物块只能固定在天花板上
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile,
				2, 0);
			// 禁止物块被放在地上
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();

			// 作为NPC入住条件之一的光源
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			name.SetDefault("灯笼");
			AddMapEntry(new Color(200, 20, 20), name);
			dustType = MyDustId.Red;

			// 不允许被自动挖掘挖掉
			disableSmartCursor = true;
		}

		public override bool HasSmartInteract()
		{
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("ChinaLantern");
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			// 如果灯笼开着就发光
			Tile tile = Main.tile[i, j];
			if (tile.frameX < 36)
			{
				r = 0.9f;
				g = 0.1f;
				b = 0.1f;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void RightClick(int i, int j)
		{
			int frameX = Main.tile[i, j].frameX;
			int frameY = Main.tile[i, j].frameY;

			// 调整到基准位置
			if (frameX >= 36)
			{
				frameX -= 36;
			}

			// 判断起始位置
			Point16 baseP = new Point16(i, j);
			if (frameX > 0 && frameY > 0)
				baseP = new Point16(i - 1, j - 1);
			else if (frameX > 0 && frameY == 0)
				baseP = new Point16(i - 1, j);
			else if (frameX == 0 && frameY > 0)
				baseP = new Point16(i, j - 1);

			
			for (int k = 0; k < 2; k++)
			{
				Main.tile[baseP.X + k, baseP.Y].frameX = (short)((Main.tile[baseP.X + k, baseP.Y].frameX + 36) % 72);
				Main.tile[baseP.X + k, baseP.Y + 1].frameX = (short)((Main.tile[baseP.X + k, baseP.Y + 1].frameX + 36) % 72);
			}

		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("ChinaLantern"));
		}

	}
}

