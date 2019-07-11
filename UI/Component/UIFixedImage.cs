using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace TemplateMod.UI.Component
{
	public class UIFixedImage : UIElement
	{
		public Texture2D Texture
		{
			get;
			set;
		}
		public Rectangle? SourceRect
		{
			get;
			set;
		}

		public float TextureScale
		{
			get;
			set;
		}

		public bool UseBoxDraw
		{
			get;
			set;
		}

		public Vector2 CornerSize
		{
			get;
			set;
		}

		public UIFixedImage(Texture2D texture) : base()
		{
			Texture = texture;
			SourceRect = null;
			TextureScale = 1f;
		}
		protected override void DrawSelf(SpriteBatch sb)
		{
			if (Texture == null)
				return;
			if (UseBoxDraw)
			{
				Drawing.DrawAdvBox(sb, GetDimensions().ToRectangle(), Color.White, Texture, CornerSize);
			}
			else
			{
				sb.Draw(Texture, GetDimensions().ToRectangle(), SourceRect,
					Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
			}
			base.DrawSelf(sb);
		}
	}
}
