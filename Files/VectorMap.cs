using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TemplateMod.VecMap
{
	public class VectorMap
	{
		private List<Vector2> _vectors;
		public VectorMap(byte[] bytes)
		{
			_vectors = new List<Vector2>();
			using (MemoryStream mems = new MemoryStream(bytes))
			{
				using (BinaryReader br = new BinaryReader(mems))
				{
					int count = br.ReadInt32();
					for (int i = 0; i < count; i++)
					{
						float x = br.ReadSingle();
						float y = br.ReadSingle();
						Vector2 vector = new Vector2(x, y);
						_vectors.Add(vector);
					}
				}
			}
		}

		public List<Vector2> Get()
		{
			return _vectors;
		}
	}
}
