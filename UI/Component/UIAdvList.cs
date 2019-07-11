using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TemplateMod.UI.Component
{
	// Token: 0x020003F4 RID: 1012
	public class UIAdvList : UIAdvElement
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x000192E1 File Offset: 0x000174E1
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		public float StartPadding { get; set; }

		// Token: 0x060023A6 RID: 9126 RVA: 0x00479FF0 File Offset: 0x004781F0
		public UIAdvList()
		{
			this._innerList.OverflowHidden = false;
			this._innerList.Width.Set(0f, 1f);
			this._innerList.Height.Set(0f, 1f);
			this.OverflowHidden = true;
			base.Append(this._innerList);
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000192EE File Offset: 0x000174EE
		public float GetTotalHeight()
		{
			return this._innerListHeight;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0047A078 File Offset: 0x00478278
		public void Goto(UIList.ElementSearchMethod searchMethod)
		{
			foreach (var t in this._items)
			{
				if (!searchMethod(t)) continue;
				this._scrollbar.ViewPosition = t.Top.Pixels;
				return;
			}
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000192F6 File Offset: 0x000174F6
		public virtual void Add(UIElement item)
		{
			this._items.Add(item);
			this._innerList.Append(item);
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0047A0D4 File Offset: 0x004782D4
		public virtual void AddRange(IEnumerable<UIElement> items)
		{
			this._items.AddRange(items);
			foreach (var element in items)
			{
				this._innerList.Append(element);
			}
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00019321 File Offset: 0x00017521
		public virtual bool Remove(UIElement item)
		{
			this._innerList.RemoveChild(item);
			this.UpdateOrder();
			return this._items.Remove(item);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00019341 File Offset: 0x00017541
		public virtual void Clear()
		{
			this._innerList.RemoveAllChildren();
			this._items.Clear();
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x00019359 File Offset: 0x00017559
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateScrollbar();
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00019367 File Offset: 0x00017567
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0047A140 File Offset: 0x00478340
		public override void RecalculateChildren()
		{
			base.RecalculateChildren();
			var num = StartPadding;
			foreach (var item in this._items)
			{
				item.Top.Set(num, 0f);
				item.Recalculate();
				num += item.GetOuterDimensions().Height + this.ListPadding;
			}
			this._innerListHeight = num;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00019391 File Offset: 0x00017591
		private void UpdateScrollbar()
		{
			if (this._scrollbar == null)
			{
				return;
			}
			this._scrollbar.SetView(base.GetInnerDimensions().Height, this._innerListHeight);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000193B8 File Offset: 0x000175B8
		public void SetScrollbar(UIAdvScrollBar scrollbar)
		{
			this._scrollbar = scrollbar;
			this.UpdateScrollbar();
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000193C7 File Offset: 0x000175C7
		public void UpdateOrder()
		{
			//this._items.Sort(new Comparison<UIElement>(this.SortMethod));
			this.UpdateScrollbar();
		}

		public void Sort()
		{
			this._items.Sort(new Comparison<UIElement>(this.SortMethod));
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00013CF5 File Offset: 0x00011EF5
		public int SortMethod(UIElement item1, UIElement item2)
		{
			return item1.CompareTo(item2);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0047A1C4 File Offset: 0x004783C4
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

		// Token: 0x060023B5 RID: 9141 RVA: 0x000193E6 File Offset: 0x000175E6
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._scrollbar != null)
			{
				this._innerList.Top.Set(-this._scrollbar.GetValue(), 0f);
			}
			this.Recalculate();
			
		}

		// Token: 0x04003FE7 RID: 16359
		public List<UIElement> _items = new List<UIElement>();

		// Token: 0x04003FE8 RID: 16360
		protected UIAdvScrollBar _scrollbar;

		// Token: 0x04003FE9 RID: 16361
		internal UIElement _innerList = new UIAdvList.UIInnerList();

		// Token: 0x04003FEA RID: 16362
		private float _innerListHeight;

		// Token: 0x04003FEB RID: 16363
		public float ListPadding = 5f;

		// Token: 0x020003F5 RID: 1013
		// (Invoke) Token: 0x060023B7 RID: 9143
		public delegate bool ElementSearchMethod(UIElement element);

		// Token: 0x020003F6 RID: 1014
		private class UIInnerList : UIAdvElement
		{
			// Token: 0x060023BA RID: 9146 RVA: 0x00008CF7 File Offset: 0x00006EF7
			public override bool ContainsPoint(Vector2 point)
			{
				return true;
			}
			//public override void Click(UIMouseEvent evt)
			//{
				
			//	base.Click(evt);
			//}
			// Token: 0x060023BB RID: 9147 RVA: 0x00431328 File Offset: 0x0042F528
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
