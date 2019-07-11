using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using TemplateMod.Services;
using TemplateMod.UI.Component;
using TemplateMod.Services.Building;

namespace TemplateMod.UI
{
	public class ToolBarServiceManager
	{
		private List<ITemplateToolBarService> _services = new List<ITemplateToolBarService>();

		public int ServicesCount
		{
			get { return _services.Count; }
		}

		public void Add(ITemplateToolBarService service)
		{
			_services.Add(service);
		}

		public void Disable(string name)
		{
			foreach (var service in _services)
			{
				if(service.Name == name)
				{
					service.Enabled = false;
				}
			}

			throw new ArgumentException("没找到名字为: " + name + " 的服务");
		}

		public void Enable(string name)
		{
			foreach (var service in _services)
			{
				if (service.Name == name)
				{
					service.Enabled = true;
				}
			}

			throw new ArgumentException("没找到名字为: " + name + " 的服务");
		}

		public void Remove(string name)
		{
			_services.RemoveAll((s) => s.Name == name);
		}

		internal void GetButtons(List<UIButton> list)
		{
			list.Clear();
			foreach (var service in _services)
			{
				if (!service.Enabled) continue;
				var boxTex = TemplateMod.ModTexturesTable["Box"];
				var button = new UIButton(service.Texture, false);
				button.OnClick += service.OnButtonClicked;
				button.Width.Set(35, 0f);
				button.Height.Set(35, 0f);
				button.ButtonDefaultColor = new Color(200, 200, 200);
				button.ButtonChangeColor = Color.White;
				button.Tooltip = service.Tooltip;
				if (service.DrawEvent != null)
				{
					button.PostDraw += service.DrawEvent;
				}
				list.Add(button);
			}
		}

		public ToolBarServiceManager()
		{
			SetUpDefault();
		}

		internal void SetUpDefault()
		{
			if (Main.netMode == 0)
			{
				Add(new BuildingService());
			}
		}
	}
}
