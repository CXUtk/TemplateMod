using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TemplateMod.Sky
{
	public class EffectManager2
	{
		private List<EffectEntity> _effectList;

		public bool CanDraw
		{
			get
			{
				return this._effectList.Any((ent) => !ent.IsDead);
			}
		}

		public int Length { get; }

		public EffectManager2(string name, int limit = 10)
		{
			_effectList = new List<EffectEntity>();
			for(int i = 0; i < limit; i++)
			{
				_effectList.Add(new EffectEntity(name, Vector2.Zero).Deactive());
			}
			Length = limit;
		}
		public void Update()
		{
			foreach(var ent in _effectList)
			{
				if(!ent.IsDead)
					ent.Update();
			}
		}
		
		public EffectEntity this[int i]
		{
			get
			{
				return _effectList[i];
			}
		}

		public void Insert(Vector2 pos)
		{
			bool conflict = false;
			foreach(var ent in _effectList)
			{
				if(Vector2.DistanceSquared(ent.Rect.Center.ToVector2(), pos) < 10000){
					ent.Reactive();
					conflict = true;
					break;
				}
			}
			if (!conflict)
			{
				int i = 0;
				for (; i < _effectList.Count; i++)
				{
					if (_effectList[i].IsDead)
					{
						break;
					}
				}
				if (i != _effectList.Count)
					_effectList[i].SetPos(pos);
			}
		}
	}
}
