/*
 * ģ��MOD 
 * TemplateMod.cs
 * ���ߣ�DXTsT
 * 
 * ����༭�޸�
 */

using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TemplateMod.UICollection;
using TemplateMod.UI;
using TemplateMod.VecMap;

// ������б�ܿ�ͷ�ľ��Ӷ���ע��QAQ���Գ�������û���κ�Ӱ�죬���Ҿ����ˣ�����ɾ

// ���϶�����Ҫʹ�õĳ��򼯣���Ҫ�Ҷ�����Ȼ����������

// ���������ռ䣬�������MOD������TemplateMod
namespace TemplateMod
{
	// MOD���������֣���Ҫ���ļ�����MOD����ȫһ�£����Ҽ̳�Mod��
	public class TemplateMod : Mod
	{
		public static bool ShowDustTestUI = false;

		public static TemplateMod Instance;

		public static ModHotKey DustTestKey;

		private static List<ConditionalInterface> _userInterfaces;



		public TemplateMod()
		{
			// MOD�ĳ�ʼ����������������һЩ����
			// ע�⣬���Load() ������һ��
			Properties = new ModProperties()
			{
				// �Զ�������ͼʲô��
				Autoload = true,

				// �Զ�����Ѫ����ͼ
				AutoloadGores = true,

				// �Զ����������ļ�
				AutoloadSounds = true,

				// �Զ����ر���ͼƬ
				AutoloadBackgrounds = true
			};

			// ������Щ�˽�һ�¾����ˣ�ÿ��mod��Ҫ�����
		}

		public override void Load()
		{
			Instance = this;
			DustTestKey = RegisterHotKey("����Dust���Խ���", "K");
			_userInterfaces = new List<ConditionalInterface>();
			AddUI(new DustTestUI());
			LoadVec.LoadVectors();
		}

		private static void AddUI(UIState state)
		{
			ConditionalInterface tmpUI = new ConditionalInterface(() => ShowDustTestUI);
			tmpUI.SetState(state);
			_userInterfaces.Add(tmpUI);
		}

		public override void PostDrawInterface(SpriteBatch spriteBatch)
		{
			foreach (var ui in _userInterfaces)
			{
				if (ui.CanShow())
				{
					ui.Draw(spriteBatch, Main._drawInterfaceGameTime);
				}
			}
		}


		public override void UpdateUI(GameTime gameTime)
		{
			foreach (var ui in _userInterfaces)
			{
				if (ui.CanShow())
				{
					ui.Update(gameTime);
				}
			}
		}

	}

}
