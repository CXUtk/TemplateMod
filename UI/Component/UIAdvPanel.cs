using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TemplateMod.UI.Component
{
	public delegate void UIDrawEventHandler(UIElement sender, SpriteBatch sb);
	public class UIAdvPanel : UIAdvElement
	{
		public event UIDrawEventHandler PostDraw;
		
		public Vector2 CornerSize
		{
			get;
			set;
		}
		public Texture2D MainTexture
		{
			get;
			set;
		}
		public Color Color
		{
			get;
			set;
		}


		public UIAdvPanel(Texture2D texture = null)
		{
			CornerSize = new Vector2(12, 12);
			MainTexture = texture;
			Color = new Color(63, 82, 151) * 0.7f;
			// base.SetPadding(CornerSize);
		}

		private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
		{
			if (MainTexture == null)
				return;
			var dimensions = GetDimensions();
			Drawing.DrawAdvBox(spriteBatch, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width,(int)dimensions.Height,
				Color, MainTexture, CornerSize);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (!Visible) return;
			this.DrawPanel(spriteBatch, MainTexture, Color);
			PostDraw?.Invoke(this, spriteBatch);
			base.DrawSelf(spriteBatch);
		}

	}
}
