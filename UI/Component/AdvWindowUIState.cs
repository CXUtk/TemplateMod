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

namespace TemplateMod.UI.Component
{
	public class AdvWindowUIState : UIState
	{
		protected UIAdvPanel WindowPanel;
		private UIButton close;
		private Vector2 _offset = new Vector2();
		private bool _dragging = false;

		protected sealed override void DrawSelf(SpriteBatch spriteBatch)
		{
			var MousePosition = Main.MouseScreen;
			if (WindowPanel.ContainsPoint(MousePosition))
			{
				Main.LocalPlayer.mouseInterface = true;
				Main.LocalPlayer.showItemIcon = false;
			}
			base.DrawSelf(spriteBatch);
			OnDraw(spriteBatch);

		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (_dragging)
			{
				WindowPanel.Left.Set(Main.MouseScreen.X - _offset.X, 0f);
				WindowPanel.Top.Set(Main.MouseScreen.Y - _offset.Y, 0f);
				Recalculate();
			}
		}

		public sealed override void OnInitialize()
		{
			WindowPanel = new UIAdvPanel();
			WindowPanel.OnMouseDown += new MouseEvent(DragStart);
			WindowPanel.OnMouseOver += new MouseEvent(Dragging);
			WindowPanel.OnMouseUp += new MouseEvent(DragEnd);
			Initialize(WindowPanel);
			var closeTex = TemplateMod.ModTexturesTable["CloseButton"];
			close = new UIButton(closeTex, false);
			close.Left.Set(-30f, 1f);
			close.Top.Set(10f, 0f);
			close.Width.Set(20f, 0f);
			close.Height.Set(20f, 0f);
			close.ButtonDefaultColor = Color.White;
			close.ButtonChangeColor = Color.Red;
			close.OnClick += Close_OnClick;
			WindowPanel.Append(close);
			this.Append(WindowPanel);
		}

		private void Dragging(UIMouseEvent evt, UIElement listeningElement)
		{
			if (_dragging)
			{
				var end = evt.MousePosition;
				WindowPanel.Left.Set(end.X - _offset.X, 0f);
				WindowPanel.Top.Set(end.Y - _offset.Y, 0f);
			}
		}

		protected virtual void Initialize(UIAdvPanel WindowPanel)
		{

		}

		protected virtual void OnClose(UIMouseEvent evt, UIElement listeningElement)
		{

		}

		protected virtual void OnDraw(SpriteBatch sb)
		{

		}


		private void Close_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			OnClose(evt, listeningElement);
		}
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			_offset = new Vector2(evt.MousePosition.X - WindowPanel.Left.Pixels, evt.MousePosition.Y - WindowPanel.Top.Pixels);
			_dragging = true;
		}
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			var end = evt.MousePosition;
			_dragging = false;
			WindowPanel.Left.Set(end.X - _offset.X, 0f);
			WindowPanel.Top.Set(end.Y - _offset.Y, 0f);
			Recalculate();
		}
	}
}
