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

// ������б�ܿ�ͷ�ľ��Ӷ���ע��QAQ���Գ�������û���κ�Ӱ�죬���Ҿ����ˣ�����ɾ

// ���϶�����Ҫʹ�õĳ��򼯣���Ҫ�Ҷ�����Ȼ����������

// ���������ռ䣬�������MOD������TemplateMod
namespace TemplateMod
{
	// MOD���������֣���Ҫ���ļ�����MOD����ȫһ�£����Ҽ̳�Mod��
	public class TemplateMod : Mod
	{
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
	}

}

