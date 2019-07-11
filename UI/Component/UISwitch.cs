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
	public delegate void SwitchEvent(UIElement element, bool state);
	public class UISwitch : UIAdvElement
	{
		public bool Value { get; set; }

		private readonly UIPanel scrollBlock;
		public event SwitchEvent OnSwitch;
		private Color bgColor;

		public UISwitch()
		{
			scrollBlock = new UIPanel();
			scrollBlock.BackgroundColor = Color.White;
			scrollBlock.Width.Set(0, 0.5f);
			scrollBlock.Height.Set(0, 1f);
			Append(scrollBlock);

			bgColor = new Color(255, 200, 200);
		}

		public override void Click(UIMouseEvent evt)
		{
			if (Enabled)
				Switch();
		}

		public void Switch()
		{
			Value ^= true;
			if (Value)
			{
				bgColor = new Color(200, 255, 200);
			}
			else
			{
				bgColor = new Color(255, 200, 200);
			}
			Recalculate();
			OnSwitch?.Invoke(this, Value);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (Value && scrollBlock.Left.Pixels < this.Width.Pixels / 2)
			{
				scrollBlock.Left.Set(scrollBlock.Left.Pixels + 10, 0f);
				if (scrollBlock.Left.Pixels > this.Width.Pixels / 2)
				{
					scrollBlock.Left.Pixels = this.Width.Pixels / 2;
				}
			}
			else if (!Value && scrollBlock.Left.Pixels > 0)
			{
				scrollBlock.Left.Set(scrollBlock.Left.Pixels - 10, 0f);
				if (scrollBlock.Left.Pixels < 0)
				{
					scrollBlock.Left.Pixels = 0;
				}
			}
		}

		protected override void DrawSelf(SpriteBatch sb)
		{
			var dimension = GetDimensions();
			Drawing.DrawAdvBox(sb, dimension.ToRectangle(), bgColor, Drawing.Box1, new Vector2(8, 8));
			base.DrawSelf(sb);
		}
	}
}
