using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TemplateMod.UI.Component
{
	public class UIAdvElement : ToggableElement
	{

		public UIAdvElement() : base()
		{

		}
		public static Rectangle GetRectIntersections(Rectangle r1, Rectangle r2)
		{
			var xmin = Math.Max(r1.X, r2.X);
			var xmax1 = r1.X + r1.Width;
			var xmax2 = r2.X + r2.Width;
			var xmax = Math.Min(xmax1, xmax2);
			if (xmax > xmin)
			{
				var ymin = Math.Max(r1.Y, r2.Y);
				var ymax1 = r1.Y + r1.Height;
				var ymax2 = r2.Y + r2.Height;
				var ymax = Math.Min(ymax1, ymax2);
				if (ymax > ymin)
				{
					var outrect = new Rectangle
					{
						X = xmin,
						Y = ymin,
						Width = xmax - xmin,
						Height = ymax - ymin
					};
					return outrect;
				}
			}
			return new Rectangle();
		}



		public override void Draw(SpriteBatch spriteBatch)
		{
			// 傻逼原版程序员不好好写剪裁效果，连矩形相交都不判
			var overflowHidden = this.OverflowHidden;
			var useImmediateMode = this._useImmediateMode;
			var rasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
			var scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
			var anisotropicClamp = SamplerState.AnisotropicClamp;

			var mystate = (RasterizerState)typeof(UIElement)
				.GetField("_overflowHiddenRasterizerState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
				.GetValue(null);
			if (useImmediateMode)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, mystate, null, Main.UIScaleMatrix);
				this.DrawSelf(spriteBatch);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, mystate, null, Main.UIScaleMatrix);
			}
			else
			{
				this.DrawSelf(spriteBatch);
			}
			if (overflowHidden)
			{
				spriteBatch.End();
				var clippingRectangle = this.GetClippingRectangle(spriteBatch);
				spriteBatch.GraphicsDevice.ScissorRectangle = GetRectIntersections(scissorRectangle, clippingRectangle);
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, mystate, null, Main.UIScaleMatrix);
			}
			this.DrawChildren(spriteBatch);
			if (overflowHidden)
			{
				spriteBatch.End();
				spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle;
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, rasterizerState, null, Main.UIScaleMatrix);
			}
		}
	}
}
