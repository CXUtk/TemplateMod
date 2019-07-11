using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TemplateMod.UI.Component
{
	// Token: 0x02000124 RID: 292
	public class UIMessageBox : UIPanel
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x00401514 File Offset: 0x003FF714
		public UIMessageBox(string text)
		{
			this.text = text;
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition = 0f;
				this.heightNeedsRecalculating = true;
			}
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0040154D File Offset: 0x003FF74D
		public override void OnActivate()
		{
			base.OnActivate();
			this.heightNeedsRecalculating = true;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0040155C File Offset: 0x003FF75C
		internal void SetText(string text)
		{
			this.text = text;
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition = 0f;
				this.heightNeedsRecalculating = true;
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00401584 File Offset: 0x003FF784
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			DynamicSpriteFont fontMouseText = Main.fontMouseText;
			float num = 0f;
			if (this._scrollbar != null)
			{
				num = -this._scrollbar.GetValue();
			}
			foreach (Tuple<string, float> tuple in this.drawtexts)
			{
				if (num + tuple.Item2 > innerDimensions.Height)
				{
					break;
				}
				if (num >= 0f)
				{
					Terraria.Utils.DrawBorderString(spriteBatch, tuple.Item1, new Vector2(innerDimensions.X, innerDimensions.Y + num), Color.White, 1f, 0f, 0f, -1);
				}
				num += tuple.Item2;
			}
			this.Recalculate();
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00401664 File Offset: 0x003FF864
		public override void RecalculateChildren()
		{
			base.RecalculateChildren();
			if (!this.heightNeedsRecalculating)
			{
				return;
			}
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			if (innerDimensions.Width <= 0f || innerDimensions.Height <= 0f)
			{
				return;
			}
			DynamicSpriteFont fontMouseText = Main.fontMouseText;
			this.drawtexts.Clear();
			float num = 0f;
			float y = fontMouseText.MeasureString("A").Y;
			string[] array = this.text.Split(new char[]
			{
				'\n'
			});
			foreach (string text in array)
			{
				string text2 = text;
				if (text2.Length == 0)
				{
					num += y;
				}
				while (text2.Length > 0)
				{
					string text3 = "";
					while (fontMouseText.MeasureString(text2).X > innerDimensions.Width)
					{
						text3 = text2[text2.Length - 1].ToString() + text3;
						text2 = text2.Substring(0, text2.Length - 1);
					}
					if (text3.Length > 0)
					{
						int num2 = text2.LastIndexOf(' ');
						if (num2 >= 0)
						{
							text3 = text2.Substring(num2 + 1) + text3;
							text2 = text2.Substring(0, num2);
						}
					}
					this.drawtexts.Add(new Tuple<string, float>(text2, y));
					num += y;
					text2 = text3;
				}
			}
			this.height = num;
			this.heightNeedsRecalculating = false;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x004017E2 File Offset: 0x003FF9E2
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateScrollbar();
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x004017F0 File Offset: 0x003FF9F0
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0040181A File Offset: 0x003FFA1A
		public void SetScrollbar(UIAdvScrollBar scrollbar)
		{
			this._scrollbar = scrollbar;
			this.UpdateScrollbar();
			this.heightNeedsRecalculating = true;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00401830 File Offset: 0x003FFA30
		private void UpdateScrollbar()
		{
			_scrollbar?.SetView(base.GetInnerDimensions().Height, this.height);
		}

		// Token: 0x04001471 RID: 5233
		private string text;

		// Token: 0x04001472 RID: 5234
		protected UIAdvScrollBar _scrollbar;

		// Token: 0x04001473 RID: 5235
		private float height;

		// Token: 0x04001474 RID: 5236
		private bool heightNeedsRecalculating;

		// Token: 0x04001475 RID: 5237
		private readonly List<Tuple<string, float>> drawtexts = new List<Tuple<string, float>>();
	}
}
