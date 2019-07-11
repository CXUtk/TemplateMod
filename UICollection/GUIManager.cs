using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TemplateMod.UI;
using TemplateMod.UI.Component;
using Microsoft.Xna.Framework;

namespace TemplateMod
{
	public enum TemplateUIState
	{
		BuildingSelectWindow,
	}
	public class GUIManager
	{
		private readonly TemplateMod _mod;

		private BuildingUIState buildingUIState;

		private readonly UserInterface _toolBarInterface;
		private readonly CDInterfaceManager _cdInterface;
		private readonly ToolBarState _toolBarState;


		private readonly Dictionary<TemplateUIState, bool> _canShowUITable = new Dictionary<TemplateUIState, bool>();
	

		public GUIManager(TemplateMod mod)
		{
			_mod = mod;

			_toolBarInterface = new UserInterface();
			_toolBarState = new ToolBarState();
			_toolBarInterface.SetState(_toolBarState);

			foreach (var type in typeof(TemplateUIState).GetEnumValues())
			{
				_canShowUITable.Add((TemplateUIState)type, false);
			}

			_cdInterface = new CDInterfaceManager();
			SetWindows();
		}

		internal void SetWindows()
		{
			buildingUIState = new BuildingUIState();
			AddState(buildingUIState, TemplateUIState.BuildingSelectWindow);
		}


		private void AddState(UIState state, TemplateUIState uistate)
		{
			var profileinterface = new ConditionalInterface(() => _canShowUITable[uistate]);
			profileinterface.SetState(state);
			_cdInterface.Add(profileinterface);
		}


		public void UpdateUI(GameTime gameTime) 
		{
			try
			{
				_cdInterface.Update(gameTime);
				_toolBarInterface.Update(gameTime);
			}
			catch (Exception ex)
			{
				Main.NewText(ex);
			}
		}
		public void RunUI()
		{
			try
			{
				_cdInterface.Draw(Main.spriteBatch);
				_toolBarInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
			}
			catch(Exception ex)
			{
				Main.NewText(ex);
			}
		}

		/// <summary>
		/// 切换UI开关状态
		/// </summary>
		/// <param name="state"></param>
		internal void ToggleState(TemplateUIState state)
		{
			if (!_canShowUITable.ContainsKey(state)) throw new ArgumentException("不存在此UI状态");
			_canShowUITable[state] ^= true;
		}



		/// <summary>
		/// 设置UI开关状态
		/// </summary>
		/// <param name="state"></param>
		internal void SetState(TemplateUIState state, bool value)
		{
			if (!_canShowUITable.ContainsKey(state)) throw new ArgumentException("不存在此UI状态");
			_canShowUITable[state] = value;
		}

		/// <summary>
		/// 判断UI是否在开启状态
		/// </summary>
		/// <param name="state"></param>
		internal bool IsActive(TemplateUIState state)
		{
			if (!_canShowUITable.ContainsKey(state)) throw new ArgumentException("不存在此UI状态");
			return _canShowUITable[state];
		}

	}
}
