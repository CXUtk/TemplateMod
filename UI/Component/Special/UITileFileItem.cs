using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ReLogic.Graphics;
using Terraria.GameInput;
using ReLogic.OS;
using Microsoft.Xna.Framework.Input;
using Terraria.UI.Chat;
using System;
using System.Collections.Generic;
using Terraria.Graphics;
using TemplateMod.Files;
using Terraria.GameContent.Liquid;
using TemplateMod.Utils;

namespace TemplateMod.UI.Component.Special
{
	public class UITileFileItem : UIAdvPanel
	{

		private readonly UIText fileNameText;
		private UIAdvPanel tilePanel;
		public TileFile file;
		private Texture2D preview;
		private bool loaded = false;
		private bool selected = false;
		private Texture2D preview2;

		public UITileFileItem(TileFile tiles, int idx)
		{
			Index = idx;
			file = tiles;
			
			this.Width.Set(120f, 0f);
			this.Height.Set(150f, 0f);
			this.CornerSize = new Vector2(8, 8);
			base.MainTexture = TemplateMod.ModTexturesTable["AdvInvBack1"];
			this.Color = Color.Cyan * 0.8f;
			base.SetPadding(6f);
			this.OverflowHidden = true;

			fileNameText = new UIText(tiles.FileName);
			fileNameText.VAlign = 1f;
			fileNameText.HAlign = 0.5f;
			fileNameText.MarginBottom = 2f;
			Append(fileNameText);

			tilePanel = new UIAdvPanel(TemplateMod.ModTexturesTable["AdvInvBack1"]);
			tilePanel.Width.Set(115f, 0f);
			tilePanel.Height.Set(115f, 0f);
			tilePanel.HAlign = 0.5f;
			tilePanel.VAlign = 0f;
			Append(tilePanel);
		}


		private static bool canDrawColorWall(Tile tile)
		{
			return tile != null && tile.wallColor() > 0 && Main.wallAltTextureDrawn[(int)tile.wall, (int)tile.wallColor()] && Main.wallAltTextureInit[(int)tile.wall, (int)tile.wallColor()];
		}


		public override void Update(GameTime gameTime)
		{
			if (!loaded)
			{
				preview = MakeThumbnail(file);
				loaded = true;
			}
			base.Update(gameTime);
		}


		public static void DrawPreview(SpriteBatch sb, TileFile.TileBlock[,] BrushTiles, Vector2 position, float scale = 1f)
		{
			Color color = Color.White;
			color.A = 160;
			int width = BrushTiles.GetLength(0);
			int height = BrushTiles.GetLength(1);
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Tile tile = TileFile.TileBlock.ToTile(BrushTiles[x, y]);
					bool flag = tile.wall > 0;
					if (flag)
					{
						Main.instance.LoadWall((int)tile.wall);
						bool flag2 = canDrawColorWall(tile) && (int)tile.type < Main.wallAltTexture.GetLength(0) && Main.wallAltTexture[(int)tile.type, (int)tile.wallColor()] != null;
						Texture2D textureWall;
						if (flag2)
						{
							textureWall = Main.wallAltTexture[(int)tile.type, (int)tile.wallColor()];
						}
						else
						{
							textureWall = Main.wallTexture[(int)tile.wall];
						}
						int wallFrame = (int)(Main.wallFrame[(int)tile.wall] * 180);
						Rectangle value = new Rectangle(tile.wallFrameX(), tile.wallFrameY() + wallFrame, 32, 32);
						Vector2 pos = position + new Vector2((float)(x * 16 - 8), (float)(y * 16 - 8));
						sb.Draw(textureWall, pos * scale, new Rectangle?(value), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
					}
					bool flag3 = tile.liquid > 14;
					if (flag3)
					{
						bool flag4 = tile.honey();
						Texture2D textureWater;
						if (flag4)
						{
							textureWater = LiquidRenderer.Instance._liquidTextures[11].Offset(16, 48, 16, 16);
						}
						else
						{
							bool flag5 = tile.lava();
							if (flag5)
							{
								textureWater = LiquidRenderer.Instance._liquidTextures[1].Offset(16, 48, 16, 16);
							}
							else
							{
								textureWater = LiquidRenderer.Instance._liquidTextures[0].Offset(16, 48, 16, 16);
							}
						}
						int waterSize = (int)((tile.liquid + 1) / 16);
						Vector2 pos2 = position + new Vector2((float)(x * 16), (float)(y * 16 + (16 - waterSize)));
						sb.Draw(textureWater, pos2 * scale, new Rectangle?(new Rectangle(0, 16 - waterSize, 16, waterSize)), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
					}
					bool flag6 = tile.active();
					if (flag6)
					{
						Main.instance.LoadTiles((int)tile.type);
						Texture2D texture = Main.tileTexture[(int)tile.type];
						Rectangle? value2 = new Rectangle?(new Rectangle((int)tile.frameX, (int)tile.frameY, 16, 16));
						Vector2 pos3 = position + new Vector2((float)(x * 16), (float)(y * 16));
						sb.Draw(texture, pos3 * scale, value2, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
					}
				}
			}
		}

		internal Texture2D MakeThumbnail(TileFile tileInfo)
		{
			int desiredWidth = 100;
			int desiredHeight = 100;
			int actualWidth = tileInfo.Width * 16;
			int actualHeight = tileInfo.Height * 16;
			float scale = 1f;
			Vector2 offset = default(Vector2);
			if (actualWidth > desiredWidth || actualHeight > desiredHeight)
			{
				if (actualHeight > actualWidth)
				{
					scale = (float)desiredWidth / (float)actualHeight;
					offset.X = ((float)desiredWidth - (float)actualWidth * scale) / 2f;
				}
				else
				{
					scale = (float)desiredWidth / (float)actualWidth;
					offset.Y = ((float)desiredHeight - (float)actualHeight * scale) / 2f;
				}
			}
			offset /= scale;
			RenderTarget2D renderTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, desiredWidth, desiredHeight);
			Main.instance.GraphicsDevice.SetRenderTarget(renderTarget);
			Main.instance.GraphicsDevice.Clear(Color.Transparent);
			Main.spriteBatch.Begin();
			DrawPreview(Main.spriteBatch, tileInfo.TileBlocks, offset, scale);
			Main.spriteBatch.End();
			Main.instance.GraphicsDevice.SetRenderTarget(null);
			Texture2D mergedTexture = new Texture2D(Main.instance.GraphicsDevice, desiredWidth, desiredHeight);
			Color[] content = new Color[desiredWidth * desiredHeight];
			renderTarget.GetData<Color>(content);
			mergedTexture.SetData<Color>(content);
			return mergedTexture;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			//Vector2 upperleft2 = GetDimensions().ToRectangle().TopLeft();
			//spriteBatch.Draw(Main.magicPixel, new Rectangle((int)(upperleft2.X ), (int)(upperleft2.Y), 16, 16),
			//			null, Color.White);
			Vector2 upperleft = tilePanel.GetDimensions().ToRectangle().TopLeft();
			if(preview != null)
			spriteBatch.Draw(preview, tilePanel.GetDimensions().ToRectangle(), Color.White);
		}



	}
}
