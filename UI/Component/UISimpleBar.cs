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
    public class UISimpleBar : UIElement
    {
		public Texture2D BarTexture { get; set; }
		public Texture2D FillerTexture { get; set; }
		public Color FillerColor { get; set; }
		public Vector2 CornerSize { get; set; }

		public float Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				if (_value > 1) _value = 1;
				else if (_value < 0) _value = 0;
			}
		}

		private float _value;

		public UISimpleBar()
        {
			FillerTexture = TemplateMod.ModTexturesTable["Box"];
			_value = 0;
			FillerColor = Color.White;
			CornerSize = new Vector2(8, 8);
		}

		protected override void DrawSelf(SpriteBatch sb)
		{
			base.DrawSelf(sb);
			var dimension = GetDimensions();
			Drawing.DrawAdvBox(sb, dimension.ToRectangle(), Color.Black, FillerTexture, CornerSize);
			Drawing.DrawAdvBox(sb, 
				new Rectangle((int)dimension.X, (int)dimension.Y, (int)(dimension.Width * Value), (int)dimension.Height), FillerColor, FillerTexture, CornerSize);
			if (BarTexture != null)
				Drawing.DrawAdvBox(sb, dimension.ToRectangle(), Color.White, BarTexture, CornerSize);

		}
    }
}
