using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using TemplateMod.UI.Component;

namespace TemplateMod.UI
{
	public class ToolBarState : UIState
	{
		public int ButtonsCount { get { return _toolButtons.Count; } }

		private UIAdvPanel windowPanel;
		private bool _collapseOn;
		private UIButton _openButton;
		private UIButton _toggleDestructButton;
		private UIButton _toggleScrollButton;

		private List<UIButton> _toolButtons = new List<UIButton>();

		private const float TOOLBAR_INIT_WIDTH = 200f;
		private const float TOOLBAR_INIT_HEIGHT = 74f;
		private const float TOOLBAR_ICON_PADDING_LEFT = 30f;
		private const float TOOLBAR_ICON_MARGIN_LEFT = 50f;

		public void ShowButtons()
		{
			windowPanel.RemoveAllChildren();
			for(var i = 0; i < _toolButtons.Count; i++)
			{
				var but = _toolButtons[i];
				but.Top.Set(-but.Height.Pixels / 2, 0.5f);
				but.Left.Set(TOOLBAR_ICON_PADDING_LEFT + i * TOOLBAR_ICON_MARGIN_LEFT, 0f);
				windowPanel.Append(but);
			}
			var estimatedWidth = TOOLBAR_ICON_PADDING_LEFT * 2 + _toolButtons.Count * TOOLBAR_ICON_MARGIN_LEFT;
			if(estimatedWidth < TOOLBAR_INIT_WIDTH)
			{
				estimatedWidth = TOOLBAR_INIT_WIDTH;
			}
			windowPanel.Left.Set(Main.screenWidth / 2 - estimatedWidth / 2, 0f);
			windowPanel.Width.Set(estimatedWidth, 0f);
		}


		internal void SetUpButtons()
		{
			TemplateMod.ToolBarServiceManager.GetButtons(_toolButtons);
		}

		public override void OnInitialize()
		{
			_collapseOn = false;
			windowPanel = new UIAdvPanel(TemplateMod.ModTexturesTable["AdvInvBack1"]);
			windowPanel.Left.Set(Main.screenWidth / 2 - TOOLBAR_INIT_WIDTH / 2, 0f);
			windowPanel.Top.Set(Main.screenHeight - 12f, 0f);
			windowPanel.Width.Set(TOOLBAR_INIT_WIDTH, 0f);
			windowPanel.Height.Set(TOOLBAR_INIT_HEIGHT, 0f);
			windowPanel.Color = Color.Transparent;
			windowPanel.CornerSize = new Vector2(12, 12);

			_openButton = new UIButton(TemplateMod.ModTexturesTable["CollapseButtonUp"], false);
			_openButton.Left.Set(Main.screenWidth / 2 - 122f, 0f);
			_openButton.Top.Set(windowPanel.GetDimensions().Position().Y - 12f, 0f);
			_openButton.Width.Set(48f, 0f);
			_openButton.Height.Set(14f, 0f);
			_openButton.ButtonDefaultColor = Color.White;
			_openButton.ButtonChangeColor = new Color(0.8f, 0.8f, 0.8f, 1f);
			_openButton.Tooltip = "打开底栏";
			_openButton.OnClick += OpenPanel_OnClick;

			SetUpButtons();
			ShowButtons();
			base.Append(_openButton);
			base.Append(windowPanel);
			

		}

		private void SwapButtonTexture()
		{
			if (_collapseOn)
			{
				_openButton.Texture = TemplateMod.ModTexturesTable["CollapseButtonDown"];
				_openButton.Tooltip = "收回底栏";
			}
			else
			{
				_openButton.Texture = TemplateMod.ModTexturesTable["CollapseButtonUp"];
				_openButton.Tooltip = "打开底栏";
			}
		}

		internal void OpenPanel_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			_collapseOn ^= true;
			SwapButtonTexture();
			windowPanel.Color = Color.White;
		}

		public override void Update(GameTime gameTime)
		{
			var maxH = Main.screenHeight - TOOLBAR_INIT_HEIGHT - 12f;
			if (_collapseOn && windowPanel.Top.Pixels > maxH)
			{
				windowPanel.Top.Set(windowPanel.Top.Pixels - 6f, 0f);
				if (windowPanel.Top.Pixels < maxH)
				{
					windowPanel.Top.Pixels = maxH;
				}
				windowPanel.Color = Color.White;
			}
			else if (!_collapseOn && windowPanel.Top.Pixels < Main.screenHeight)
			{
				windowPanel.Top.Set(windowPanel.Top.Pixels + 6f, 0f);
				if (windowPanel.Top.Pixels >= Main.screenHeight)
				{
					windowPanel.Color = Color.Transparent;
					windowPanel.Top.Pixels = Main.screenHeight;
				}
			}
			if (windowPanel.Top.Pixels > Main.screenHeight)
			{
				windowPanel.Top.Set(Main.screenHeight, 0f);
			}
			_openButton.Left.Set(windowPanel.GetDimensions().Center().X - 24f, 0f);
			_openButton.Top.Set(windowPanel.Top.Pixels - 12f, 0f);
			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			if (windowPanel.ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
				Main.LocalPlayer.showItemIcon = false;
			}
			base.Draw(spriteBatch);
		}
	}
}
