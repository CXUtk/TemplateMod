using Microsoft.Xna.Framework;
using TemplateMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod.Tiles
{
	public class TemplateOre : ModTile
	{
		public override void SetDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			// 是否能被探矿药剂识别
			Main.tileSpelunker[Type] = true;
			// 物块在金属探测器下的优先级，详情请见 https://terraria.gamepedia.com/Metal_Detector
			Main.tileValue[Type] = 410; 
			// 让矿物闪耀
			Main.tileShine2[Type] = true; 
			Main.tileShine[Type] = 975; 

			// 与泥土块融合
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("模板矿");
			AddMapEntry(new Color(77, 77, 77), name);

			dustType = MyDustId.BlackMaterial;
			drop = 1;
			soundType = 21;
			soundStyle = 1;
			//mineResist = 4f;
			//minPick = 200;
		}
	}
}