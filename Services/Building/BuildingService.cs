using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod.UI;
using TemplateMod.UI.Component;
using Terraria.UI;

namespace TemplateMod.Services.Building
{
	public class BuildingService : ITemplateToolBarService
	{
		public Texture2D Texture => TemplateMod.ModTexturesTable["Cog"];

		public string Tooltip => "地形选择器";

		public string Name => "模板UI: 地形选择器";

		public bool Enabled { get; set; }

		public UIDrawEventHandler DrawEvent => null;

		public BuildingService()
		{
			Enabled = true;
		}

		public void OnButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			TemplateMod.Instance.GUIManager.ToggleState(TemplateUIState.BuildingSelectWindow);
			if (TemplateMod.Instance.GUIManager.IsActive(TemplateUIState.BuildingSelectWindow))
			{
				BuildingUIState.Instance.RefreshFiles();
			}
		}
	}
}
