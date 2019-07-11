using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod.UI.Component;
using Terraria;
using Terraria.UI;

namespace TemplateMod.UI
{
	public class UILayer : GameInterfaceLayer
	{
		private readonly GUIManager _UImanager;

		public UILayer(GUIManager manager) : this("TemplateMod: UI", InterfaceScaleType.UI)
		{
			_UImanager = manager;
		}

		private UILayer(string name, InterfaceScaleType scaleType) : base(name, scaleType)
		{

		}

		protected override bool DrawSelf()
		{
			_UImanager.RunUI();
			if (TemplateMod.ShowTooltip == null) return true;
			if (TemplateMod.ShowTooltip != "")
			{
				var size = Main.fontMouseText.MeasureString(TemplateMod.ShowTooltip);
				var drawPos = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(25f, 25f);
				if (drawPos.Y > Main.screenHeight - 30f)
					drawPos.Y = Main.screenHeight - 30f;
				if (drawPos.X > Main.screenWidth - size.X)
					drawPos.X = Main.screenWidth - size.X - 30.0f;
				Drawing.DrawBox(Main.spriteBatch, new Rectangle((int)drawPos.X - 5, (int)drawPos.Y - 10, (int)size.X + 10, (int)size.Y + 10),
					Color.White * 0.75f, 0.8f, TemplateMod.ModTexturesTable["Box2"]);
				Drawing.DrawColorCodedString(Main.spriteBatch, Main.fontMouseText, TemplateMod.ShowTooltip, drawPos);
			}
			TemplateMod.ShowTooltip = "";
			return true;
		}
	}
}
