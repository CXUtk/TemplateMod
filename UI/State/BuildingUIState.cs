﻿using System;
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
using Terraria.GameContent.UI.States;
using TemplateMod.Utils;
using System.Security.Cryptography;
using TemplateMod.Files;
using System.Threading;
using TemplateMod.UI.Component.Special;
//using TemplateMod.UI.Component.Special;

namespace TemplateMod.UI
{
	public class BuildingUIState : AdvWindowUIState
	{
		public static BuildingUIState Instance;
		private int _relaxTimer;
		private float _rotation;
		private UIAdvGrid _tilefilesList;
		private UIText Label;
		private UICDButton selectModeButton;
		private UICDButton buildModeButton;
		private UICDButton saveButton;
		private UICDButton refreshButton;
		private const float WINDOW_WIDTH = 640;
		private const float WINDOW_HEIGHT = 420;
		private const float FILE_LIST_WIDTH = 420;
		private const float FILE_LIST_HEIGHT = 320;
		private const float FILE_LIST_OFFSET_LEFT = -90;
		private const float FILELIST_OFFSET_TOP = 25;
		public int selectedItem = -1;

		public BuildingUIState()
		{
			Instance = this;
		}


		public void DrawSelected()
		{
			var selected = _tilefilesList._items.First((u) => ((UITileFileItem)u).Index == selectedItem);
			if (selected == null) return;
			var thisitem = (UITileFileItem)selected;
			var drawPos =  new Vector2((int)(Main.MouseScreen.X / 16) * 16, (int)(Main.MouseScreen.Y / 16) * 16) - new Vector2(thisitem.file.Width * 8, thisitem.file.Height * 8);
			UITileFileItem.DrawPreview(Main.spriteBatch, thisitem.file.TileBlocks, drawPos, 1f);
		}

		public void PlaceSelected()
		{
			var selected = _tilefilesList._items.First((u) => ((UITileFileItem)u).Index == selectedItem);
			if (selected == null) return;
			var thisitem = (UITileFileItem)selected;
			var drawPos = new Point((int)((Main.MouseWorld.X - thisitem.file.Width * 8) / 16) , (int)((Main.MouseWorld.Y - thisitem.file.Height * 8) / 16) );
			int w = thisitem.file.Width;
			int h = thisitem.file.Height;
			for(int i = 0; i < w; i++)
			{
				for(int j = 0; j < h; j++)
				{
					Tile t = TileFile.TileBlock.ToTile(thisitem.file.TileBlocks[i, j]);
					Main.tile[drawPos.X + i, drawPos.Y + j] = t;
				}
			}
		}

		protected override void Initialize(UIAdvPanel WindowPanel)
		{
			WindowPanel.MainTexture = TemplateMod.ModTexturesTable["AdvInvBack1"];
			WindowPanel.Left.Set(Main.screenWidth / 2 - WINDOW_WIDTH / 2, 0f);
			WindowPanel.Top.Set(Main.screenHeight / 2 - WINDOW_HEIGHT / 2, 0f);
			WindowPanel.Width.Set(WINDOW_WIDTH, 0f);
			WindowPanel.Height.Set(WINDOW_HEIGHT, 0f);
			WindowPanel.Color = Color.White * 0.8f;

			var fileListPanel = new UIAdvPanel(TemplateMod.ModTexturesTable["Box"]);
			fileListPanel.Top.Set(-FILE_LIST_HEIGHT / 2 + FILELIST_OFFSET_TOP, 0.5f);
			fileListPanel.Left.Set(-FILE_LIST_WIDTH / 2 + FILE_LIST_OFFSET_LEFT, 0.5f);
			fileListPanel.Width.Set(FILE_LIST_WIDTH, 0f);
			fileListPanel.Height.Set(FILE_LIST_HEIGHT, 0f);
			fileListPanel.SetPadding(10f);

			_tilefilesList = new UIAdvGrid();
			_tilefilesList.Width.Set(-25f, 1f);
			_tilefilesList.Height.Set(0f, 1f);
			_tilefilesList.ListPadding = 5f;
			fileListPanel.Append(_tilefilesList);

			// ScrollBar设定
			var uiscrollbar = new UIAdvScrollBar();
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(0f, 1f);
			uiscrollbar.HAlign = 1f;
			fileListPanel.Append(uiscrollbar);
			_tilefilesList.SetScrollbar(uiscrollbar);
			WindowPanel.Append(fileListPanel);

			//refreshButton = new UIButton(TemplateMod.ModTexturesTable["Refresh"], false);
			//refreshButton.Top.Set(55, 0f);
			//refreshButton.Left.Set(-35 / 2 - 65, 1f);
			//refreshButton.Width.Set(35, 0f);
			//refreshButton.Height.Set(35, 0f);
			//refreshButton.OnClick += RefreshButton_OnClick;
			//refreshButton.ButtonDefaultColor = new Color(200, 200, 200);
			//refreshButton.ButtonChangeColor = Color.White;
			//refreshButton.UseRotation = true;
			//refreshButton.TextureScale = 0.2f;
			//refreshButton.Tooltip = "刷新";
			//WindowPanel.Append(refreshButton);

			var announcement = new UIMessageBox("打开选择模式以后，左键可以选择左上角的点，右键选择右下角的点，然后点击保存即可。");
			announcement.Top.Set(-FILE_LIST_HEIGHT / 2 + FILELIST_OFFSET_TOP - 45, 0.5f);
			announcement.Left.Set(-200, 1f);
			announcement.Width.Set(200, 0f);
			announcement.Height.Set(165, 0f);
			announcement.BackgroundColor = Color.Transparent;
			announcement.BorderColor = Color.Transparent;
			WindowPanel.Append(announcement);


			Label = new UIText("地形选择器", 0.6f, true);
			Label.Top.Set(-FILE_LIST_HEIGHT / 2 + FILELIST_OFFSET_TOP - 35f, 0.5f);
			var texSize = Main.fontMouseText.MeasureString(Label.Text);
			Label.Left.Set(-FILE_LIST_WIDTH / 2 + FILE_LIST_OFFSET_LEFT, 0.5f);
			WindowPanel.Append(Label);

			selectModeButton = new UICDButton(null, true);
			selectModeButton.Top.Set(350, 0f);
			selectModeButton.Left.Set(-175, 1f);
			selectModeButton.Width.Set(150, 0f);
			selectModeButton.Height.Set(40, 0f);
			selectModeButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBack2"];
			selectModeButton.ButtonDefaultColor = new Color(200, 200, 200);
			selectModeButton.ButtonChangeColor = Color.White;
			selectModeButton.CornerSize = new Vector2(12, 12);
			selectModeButton.ButtonText = "开启选择模式";
			selectModeButton.OnClick += SelectButtonClick;
			WindowPanel.Append(selectModeButton);


			buildModeButton = new UICDButton(null, true);
			buildModeButton.Top.Set(305, 0f);
			buildModeButton.Left.Set(-175, 1f);
			buildModeButton.Width.Set(150, 0f);
			buildModeButton.Height.Set(40, 0f);
			buildModeButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBack2"];
			buildModeButton.ButtonDefaultColor = new Color(200, 200, 200);
			buildModeButton.ButtonChangeColor = Color.White;
			buildModeButton.CornerSize = new Vector2(12, 12);
			buildModeButton.ButtonText = "开启建筑模式";
			buildModeButton.OnClick += BuildModeButton_OnClick;
			WindowPanel.Append(buildModeButton);

			saveButton = new UICDButton(null, true);
			saveButton.Top.Set(260, 0f);
			saveButton.Left.Set(-175, 1f);
			saveButton.Width.Set(150, 0f);
			saveButton.Height.Set(40, 0f);
			saveButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBack2"];
			saveButton.ButtonDefaultColor = new Color(200, 200, 200);
			saveButton.ButtonChangeColor = Color.White;
			saveButton.CornerSize = new Vector2(12, 12);
			saveButton.ButtonText = "保存已选择";
			saveButton.OnClick += SaveButton_OnClick; 
			WindowPanel.Append(saveButton);

			refreshButton = new UICDButton(null, true);
			refreshButton.Top.Set(215, 0f);
			refreshButton.Left.Set(-175, 1f);
			refreshButton.Width.Set(150, 0f);
			refreshButton.Height.Set(40, 0f);
			refreshButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBack2"];
			refreshButton.ButtonDefaultColor = new Color(200, 200, 200);
			refreshButton.ButtonChangeColor = Color.White;
			refreshButton.CornerSize = new Vector2(12, 12);
			refreshButton.ButtonText = "刷新";
			refreshButton.OnClick += RefreshButton_OnClick1;
			WindowPanel.Append(refreshButton);
		}

		private void RefreshButton_OnClick1(UIMouseEvent evt, UIElement listeningElement)
		{
			RefreshFiles();
		}

		private void SaveButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			int w = TemplateMod.SelectLowerRight.X - TemplateMod.SelectUpperLeft.X + 1;
			int h = TemplateMod.SelectLowerRight.Y - TemplateMod.SelectUpperLeft.Y + 1;
			if (!TemplateMod.SelectMode || w <= 0 || h <= 0)
			{
				Main.NewText("错误：你没有选择任何有效的区域");
				return;
			}
			// 设置区域上限为百万面积
			if(w > 2000 || h > 2000 || w * h > 10e6)
			{
				Main.NewText("错误：选择的区域过大");
				return;
			}
			TileFile file = new TileFile(w, h);
			for (int i = TemplateMod.SelectUpperLeft.X; i <= TemplateMod.SelectLowerRight.X; i++)
			{
				for (int j = TemplateMod.SelectUpperLeft.Y; j <= TemplateMod.SelectLowerRight.Y; j++)
				{
					file.SetTile(i - TemplateMod.SelectUpperLeft.X, j - TemplateMod.SelectUpperLeft.Y, Main.tile[i, j]);
				}
			}
			TemplateMod.Instance.TileFileManager.AddAndSave(file);
			RefreshFiles();
			
		}

		private void BuildModeButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			TemplateMod.BuildMode ^= true;
			TemplateMod.SelectMode = false;
			UpdateBuildButton();
			UpdateSelectButton();
			
		}

		private void UpdateBuildButton()
		{
			buildModeButton.ButtonText = (TemplateMod.SelectMode ? "关闭" : "开启") + "建筑模式";
			if (TemplateMod.BuildMode)
			{
				buildModeButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBackRej"];
			}
			else
			{
				buildModeButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBack2"];
			}
		}

		private void UpdateSelectButton()
		{
			selectModeButton.ButtonText = (TemplateMod.SelectMode ? "关闭" : "开启") + "选择模式";
			if (TemplateMod.SelectMode)
			{
				selectModeButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBackRej"];
			}
			else
			{
				selectModeButton.BoxTexture = TemplateMod.ModTexturesTable["AdvInvBack2"];
			}
		}

		private void SelectButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			TemplateMod.SelectMode ^= true;
			TemplateMod.BuildMode = false;
			UpdateSelectButton();
			UpdateBuildButton();
		}

		public void RefreshFiles()
		{
			//var thread = new Thread(() =>
			//{
			//	// 锁住这个对象防止刷新频率过快导致错位
			//	lock (this)
			//	{
			selectedItem = -1;
			_tilefilesList.Clear();
			int i = 0;
			foreach (var file in TemplateMod.Instance.TileFileManager.GetTileFiles())
			{
				var f = new UITileFileItem(file, i);
				f.OnClick += F_OnClick;
				_tilefilesList.Add(f);
				i++;
			}
			//	}
			//});
			//thread.Start();
		}

		private void F_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			foreach(var item in _tilefilesList._items)
			{
				var fileitem = (UITileFileItem)item;
				fileitem.MainTexture = TemplateMod.ModTexturesTable["AdvInvBack1"];
			}
			var thisitem = (UITileFileItem)listeningElement;
			thisitem.MainTexture = TemplateMod.ModTexturesTable["AdvInvBack3"];
			selectedItem = thisitem.Index;
		}

		public bool MouseInside
		{
			get
			{
				return TemplateMod.Instance.GUIManager.IsActive(TemplateUIState.BuildingSelectWindow)
					&& WindowPanel.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint());
			}
		}



		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}


		public void ClearMatches()
		{
			_tilefilesList.Clear();
		}

		protected override void OnClose(UIMouseEvent evt, UIElement listeningElement)
		{
			TemplateMod.Instance.GUIManager.SetState(TemplateUIState.BuildingSelectWindow, false);
		}
	}
}
