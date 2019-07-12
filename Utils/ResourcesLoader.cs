using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Terraria;
using Terraria.ModLoader;

namespace TemplateMod
{
	public static class ResourcesLoader
	{
		public static int OldGlowMasksLength;

		public static void LoadAllTextures()
		{
			try
			{
				TemplateMod.ModTexturesTable = new Dictionary<string, Texture2D>();
				TemplateMod.ModTexturesTable.Clear();
				LoadTextures();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		public static void Unload()
		{
			foreach(var t in TemplateMod.ModTexturesTable)
			{
				t.Value.Dispose();
			}
			TemplateMod.ModTexturesTable.Clear();
			TemplateMod.ModTexturesTable = null;
		}
		private static void LoadTexture(string name)
		{
			TemplateMod.ModTexturesTable.Add(name.Substring(7), TemplateMod.Instance.GetTexture(name));
		}
		private static void LoadTextures()
		{
			// 这里用了反射获取贴图内容以及名字
			IDictionary<string, Texture2D> textures = (IDictionary<string, Texture2D>)(typeof(Mod).GetField("textures",
				System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(TemplateMod.Instance));

			var names = textures.Keys.Where((name) =>
			{
				return name.StartsWith("Images/");
			});
			foreach (var name in names)
				LoadTexture(name);
		}
		public static Vector2 Integerize(this Vector2 v)
		{
			return new Vector2((int)v.X, (int)v.Y);
		}
	}
}
