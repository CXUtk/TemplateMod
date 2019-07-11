using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.UI;

namespace TemplateMod.UI.Component
{
	public class UISimpleList : UIAdvElement
	{
		private readonly List<UIElement> _contents;

		public float ListPadding { get; set; }

		public UISimpleList()
		{
			_contents = new List<UIElement>();
			ListPadding = 5f;
		}
		
		public override void RecalculateChildren()
		{
			base.RecalculateChildren();
			var num = 0f;
			foreach (var element in this._contents)
			{
				element.Top.Set(num, 0f);
				element.Recalculate();
				num += element.GetOuterDimensions().Height + ListPadding;
			}
			Height.Set(num, 0f);
		}

		public void Clear()
		{
			_contents.Clear();
			RemoveAllChildren();
		}

		public void Add(UIElement element)
		{
			_contents.Add(element);
			Append(element);
		}

		public void Remove(UIElement element)
		{
			RemoveChild(element);
			_contents.Remove(element);
		}


	}
}
