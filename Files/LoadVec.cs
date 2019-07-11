using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace TemplateMod.VecMap
{

	public static class LoadVec
	{
		private static Dictionary<string, VectorMap> _vecMapHash;

		public static void LoadVectors()
		{
			_vecMapHash = new Dictionary<string, VectorMap>();
			LoadSingle("D");
			LoadSingle("X");
			LoadSingle("s");
			LoadSingle("T");
			LoadSingle("小");
			LoadSingle("裙");
			LoadSingle("子");
		}

		private static void LoadSingle(string name)
		{
			byte[] buffer = TemplateMod.Instance.GetFileBytes(string.Format("VecMap/{0}.vec", name));
			VectorMap map = new VectorMap(buffer);
			_vecMapHash.Add(name, map);
		}

		public static List<Vector2> GetVecMap(string name)
		{
			if (_vecMapHash.ContainsKey(name))
			{
				return _vecMapHash[name].Get();
			}
			else
			{
				throw new ArgumentException("未找到名为" + name + "的向量表");
			}
		}
	}
}
