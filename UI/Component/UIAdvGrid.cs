using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TemplateMod.UI.Component
{

	// Token: 0x020002BB RID: 699
	public class UIAdvGrid : UIAdvElement
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00431028 File Offset: 0x0042F228
		public UIAdvGrid()
		{
			this._innerList.OverflowHidden = false;
			this._innerList.Width.Set(0f, 1f);
			this._innerList.Height.Set(0f, 1f);
			this.OverflowHidden = true;
			base.Append(this._innerList);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00013BFD File Offset: 0x00011DFD
		public float GetTotalHeight()
		{
			return this._innerListHeight;
		}

		//// Token: 0x06001B7C RID: 7036 RVA: 0x004310B0 File Offset: 0x0042F2B0
		//public void Goto(UIGrid.ElementSearchMethod searchMethod, bool center = false)
		//{
		//	for (int i = 0; i < this._items.Count; i++)
		//	{
		//		if (searchMethod(this._items[i]))
		//		{
		//			this._scrollbar.ViewPosition = this._items[i].Top.Pixels;
		//			if (center)
		//			{
		//				this._scrollbar.ViewPosition = this._items[i].Top.Pixels - base.GetInnerDimensions().Height / 2f + this._items[i].GetOuterDimensions().Height / 2f;
		//			}
		//			return;
		//		}
		//	}
		//}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00013C05 File Offset: 0x00011E05
		public virtual void Add(UIElement item)
		{
			this._items.Add(item);
			this._innerList.Append(item);
			// this.UpdateOrder();
			this._innerList.Recalculate();
		}

		public List<UIElement> GetList()
		{
			return this._items;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00431164 File Offset: 0x0042F364
		public virtual void AddRange(IEnumerable<UIElement> items)
		{
			this._items.AddRange(items);
			foreach (var element in items)
			{
				this._innerList.Append(element);
			}
			// this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00013C30 File Offset: 0x00011E30
		public virtual bool Remove(UIElement item)
		{
			this._innerList.RemoveChild(item);
			// this.UpdateOrder();
			return this._items.Remove(item);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00013C50 File Offset: 0x00011E50
		public virtual void Clear()
		{
			this._innerList.RemoveAllChildren();
			this._items.Clear();
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00013C68 File Offset: 0x00011E68
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateScrollbar();
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00013C76 File Offset: 0x00011E76
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
			}
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x004311D0 File Offset: 0x0042F3D0
		public override void RecalculateChildren()
		{
			var width = base.GetInnerDimensions().Width;
			base.RecalculateChildren();
			var num = 0f;
			var num2 = 0f;
			var num3 = 0f;
			foreach (var uielement in this._items)
			{
				var outerDimensions = uielement.GetOuterDimensions();
				if (num2 + outerDimensions.Width > width && num2 > 0f)
				{
					num += num3 + this.ListPadding;
					num2 = 0f;
					num3 = 0f;
				}
				num3 = Math.Max(num3, outerDimensions.Height);
				uielement.Left.Set(num2, 0f);
				num2 += outerDimensions.Width + this.ListPadding;
				uielement.Top.Set(num, 0f);
				uielement.Recalculate();
			}
			this._innerListHeight = num + num3;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00013CA0 File Offset: 0x00011EA0
		private void UpdateScrollbar()
		{
			_scrollbar?.SetView(base.GetInnerDimensions().Height, this._innerListHeight);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00013CC7 File Offset: 0x00011EC7
		public void SetScrollbar(UIAdvScrollBar scrollbar)
		{
			this._scrollbar = scrollbar;
			this.UpdateScrollbar();
		}

		//// Token: 0x06001B86 RID: 7046 RVA: 0x00013CD6 File Offset: 0x00011ED6
		//public void UpdateOrder()
		//{
		//	this._items.Sort(new Comparison<UIElement>(this.SortMethod));
		//	this.UpdateScrollbar();
		//}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00013CF5 File Offset: 0x00011EF5
		public int SortMethod(UIElement item1, UIElement item2)
		{
			return item1.CompareTo(item2);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x004312B8 File Offset: 0x0042F4B8
		public override List<SnapPoint> GetSnapPoints()
		{
			var list = new List<SnapPoint>();
			SnapPoint item;
			if (base.GetSnapPoint(out item))
			{
				list.Add(item);
			}
			foreach (var uielement in this._items)
			{
				list.AddRange(uielement.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00013CFE File Offset: 0x00011EFE
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._scrollbar != null)
			{
				this._innerList.Top.Set(-this._scrollbar.GetValue(), 0f);
			}
			this.Recalculate();
		}

		// Token: 0x0400188D RID: 6285
		public List<UIElement> _items = new List<UIElement>();

		// Token: 0x0400188E RID: 6286
		protected UIAdvScrollBar _scrollbar;

		// Token: 0x0400188F RID: 6287
		internal UIElement _innerList = new UIInnerList();

		// Token: 0x04001890 RID: 6288
		private float _innerListHeight;

		// Token: 0x04001891 RID: 6289
		public float ListPadding = 5f;

		// Token: 0x020002BC RID: 700
		// (Invoke) Token: 0x06001B8B RID: 7051
		public delegate bool ElementSearchMethod(UIElement element);

		// Token: 0x020002BD RID: 701
		private class UIInnerList : UIElement
		{
			// Token: 0x06001B8E RID: 7054 RVA: 0x00008CF7 File Offset: 0x00006EF7
			public override bool ContainsPoint(Vector2 point)
			{
				return true;
			}

			// Token: 0x06001B8F RID: 7055 RVA: 0x00431328 File Offset: 0x0042F528
			protected override void DrawChildren(SpriteBatch spriteBatch)
			{
				var position = this.Parent.GetDimensions().Position();
				var dimensions = new Vector2(this.Parent.GetDimensions().Width, this.Parent.GetDimensions().Height);
				foreach (var uielement in this.Elements)
				{
					var position2 = uielement.GetDimensions().Position();
					var dimensions2 = new Vector2(uielement.GetDimensions().Width, uielement.GetDimensions().Height);
					if (Collision.CheckAABBvAABBCollision(position, dimensions, position2, dimensions2))
					{
						uielement.Draw(spriteBatch);
					}
				}
			}

		}
	}

}
