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

namespace TemplateMod.UI.Component.Special
{
	public class UITileFileItem : UIAdvPanel
	{

		private readonly UIText fileNameText;
		private UIAdvPanel tilePanel;
		private TileFile file;

		public UITileFileItem(TileFile tiles)
		{
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


		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			//Vector2 upperleft2 = GetDimensions().ToRectangle().TopLeft();
			//spriteBatch.Draw(Main.magicPixel, new Rectangle((int)(upperleft2.X ), (int)(upperleft2.Y), 16, 16),
			//			null, Color.White);
			Vector2 upperleft = tilePanel.GetDimensions().ToRectangle().TopLeft();
			Main.NewText(file.Width);
			for (int i = 0; i < Math.Min(file.Width, 7); i++)
			{
				for (int j = 0; j < Math.Min(file.Height, 7); j++)
				{
					//if(file.TileBlocks[i, j].wall != 0)
					//{
					//	var tex = Main.tileTexture[file.TileBlocks[i, j].type];
					//	spriteBatch.Draw(tex, new Rectangle((int)(upperleft.X + i * 16), (int)(upperleft.Y + j * 16), 16, 16),
					//		new Rectangle(file.TileBlocks[i, j].frameX, file.TileBlocks[i, j].frameY, 16, 16), Color.White);
					//}
					if (file.TileBlocks[i, j].type == 0) continue;
					var tex = Main.tileTexture[file.TileBlocks[i, j].type];
					spriteBatch.Draw(tex, new Rectangle((int)(upperleft.X + i * 16), (int)(upperleft.Y + j * 16), 16, 16),
						new Rectangle(file.TileBlocks[i, j].frameX, file.TileBlocks[i, j].frameY, 16, 16), Color.White);
				}
			}
		}



	}
}
