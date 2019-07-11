using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TemplateMod.UI.Component;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.UI;

namespace TemplateMod.Services
{
	public interface ITemplateToolBarService
	{
		/// <summary>
		/// 35x35的工具栏图标
		/// </summary>
		Texture2D Texture { get; }

		/// <summary>
		/// 要对按钮进行的额外绘制操作
		/// </summary>
		UIDrawEventHandler DrawEvent { get; }

		/// <summary>
		/// 35x35的工具栏图标
		/// </summary>
		string Tooltip { get; }

		/// <summary>
		/// 这个工具的名字，方便索引用的，尽量起一些具有区分性的名字，避免冲突
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 该服务是否可用，比如有些服务是只有管理员才能用的，一开始不能设为true
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// UI的开/关切换，也就是点击图标时发生的事情
		/// </summary>
		void OnButtonClicked(UIMouseEvent evt, UIElement listeningElement);
	}
}
