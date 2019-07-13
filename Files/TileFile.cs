using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace TemplateMod.Files
{
	public class TileFile
	{
		public struct TileBlock
		{
			public bool isMod;
			public ushort type;
			public string name;
			public string modname;
			public ushort wall;
			public byte liquid;
			public ushort sTileHeader;
			public byte bTileHeader;
			public byte bTileHeader2;
			public byte bTileHeader3;
			public short frameX;
			public short frameY;

			public static Tile ToTile(TileBlock block)
			{
				Tile t = new Tile();
				t.type = block.type;
				t.wall = block.wall;
				t.liquid = block.liquid;
				t.sTileHeader = block.sTileHeader;
				t.bTileHeader = block.bTileHeader;
				t.bTileHeader2 = block.bTileHeader2;
				t.bTileHeader3 = block.bTileHeader3;
				t.frameX = block.frameX;
				t.frameY = block.frameY;
				return t;
			}
		}
		public string FileName { get; set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public DateTime LastModifiedTime { get; private set; }
		public DateTime CreatedTime { get; private set; }
		public TileBlock[,] TileBlocks { get; private set; }
		private const string AUTH = "TILE_FILE_TEMPLATE";

		public void SetTile(int x, int y, Tile tile)
		{
			// 注意，这里的物块名字只能是这个mod（TemplateMod）拥有的
			var block = new TileBlock();
			block.isMod = (TileLoader.GetTile(tile.type) != null);
			if(block.isMod)
			{
				block.name = TileLoader.GetTile(tile.type).Name;
				block.modname = TileLoader.GetTile(tile.type).mod.Name;
			}
			block.type = tile.type;
			block.wall = tile.wall;
			block.liquid = tile.liquid;
			block.sTileHeader = tile.sTileHeader;
			block.bTileHeader = tile.bTileHeader;
			block.bTileHeader2 = tile.bTileHeader2;
			block.bTileHeader3 = tile.bTileHeader3;
			block.frameX = tile.frameX;
			block.frameY = tile.frameY;
			TileBlocks[x, y] = block;
		}

		public TileFile()
		{

		}

		public TileFile(int w, int h)
		{
			Width = w;
			Height = h;
			TileBlocks = new TileBlock[w, h];
			CreatedTime = DateTime.Now;
		}

		public void ReadFile(string path)
		{
			FileStream fileStream = File.OpenRead(path);
			Read(fileStream);
		}

		public void Read(Stream stream)
		{
			using (DeflateStream ds = new DeflateStream(stream, CompressionMode.Decompress))
			using (BinaryReader br = new BinaryReader(ds))
			{
				// 验证文件合法性，读取所有物块数据
				var auth = br.ReadString();
				if(auth != AUTH)
				{
					throw new FileLoadException("文件格式不合法");
				}
				int w = br.ReadUInt16(), h = br.ReadUInt16();
				Width = w;
				Height = h;
				TileBlocks = new TileBlock[w, h];
				CreatedTime = new DateTime(br.ReadInt64());
				LastModifiedTime = new DateTime(br.ReadInt64());
				for(int i = 0; i < w; i++)
				{
					for(int j = 0; j < h; j++)
					{
						var tile = new TileBlock();
						tile.isMod = br.ReadBoolean();
						if (tile.isMod)
						{
							tile.name = br.ReadString();
							tile.modname = br.ReadString();
							var reqmod = ModLoader.GetMod(tile.modname);
							if (reqmod != null)
							{
								tile.type = (ushort)reqmod.TileType(tile.name);
							}
							else
							{
								tile.type = (ushort)ModLoader.GetMod("ModLoader").TileType<MysteryTile>();
							}
						}
						else
						{
							tile.type = br.ReadUInt16();
						}
						tile.wall = br.ReadUInt16();
						tile.liquid = br.ReadByte();
						tile.sTileHeader = br.ReadUInt16();
						tile.bTileHeader = br.ReadByte();
						tile.bTileHeader2 = br.ReadByte();
						tile.bTileHeader3= br.ReadByte(); 
						tile.frameX = br.ReadInt16();
						tile.frameY = br.ReadInt16();
						TileBlocks[i, j] = tile;
					}
				}
			}
		}

		public void Write(string path)
		{
			LastModifiedTime = CreatedTime;
			using (FileStream fs = File.OpenWrite(path))
			using (DeflateStream ds = new DeflateStream(fs, CompressionMode.Compress))
			using (BinaryWriter bw = new BinaryWriter(ds))
			{
				// 文件格式：识别文本+宽+高+物块信息……
				bw.Write(AUTH);
				bw.Write((ushort)Width);
				bw.Write((ushort)Height);
				bw.Write(CreatedTime.Ticks);
				bw.Write(LastModifiedTime.Ticks);

				// 把物块信息全部写进文件
				for (int i = 0; i < Width; i++)
				{
					for (int j = 0; j < Height; j++)
					{
						var tile = TileBlocks[i, j];
						bw.Write(tile.isMod);
						if (tile.isMod)
						{
							bw.Write(tile.name);
							bw.Write(tile.modname);
						}
						else
							bw.Write(tile.type);
						bw.Write(tile.wall);
						bw.Write(tile.liquid);
						bw.Write(tile.sTileHeader);
						bw.Write(tile.bTileHeader);
						bw.Write(tile.bTileHeader2);
						bw.Write(tile.bTileHeader3);
						bw.Write(tile.frameX);
						bw.Write(tile.frameY);
					}
				}
			}
		}
	}
}
