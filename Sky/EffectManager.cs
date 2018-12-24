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
	public class EffectEntity
	{
		public Rectangle Rect
		{
			get;
			private set;
		}
		public int TimeLeft
		{
			get;
			private set;
		}

		public Effect Effect { get; }

		public EffectEntity(string effectName, Vector2 pos)
		{
			Effect = TemplateMod.MODEffectTable[effectName];
			Rect = new Rectangle((int)pos.X - 300, (int)pos.Y - 300, 600, 600);
			TimeLeft = 40;
		}
		public void Update()
		{
			if(TimeLeft >= 0)
				TimeLeft--;
		}

		public EffectEntity Deactive()
		{
			TimeLeft = 0;
			return this;
		}
		public EffectEntity Reactive()
		{
			TimeLeft = 40;
			return this;
		}

		public void SetPos(Vector2 pos)
		{
			Rect = new Rectangle((int)pos.X - 300, (int)pos.Y - 300, 600, 600);
		}

		public bool IsDead { get { return TimeLeft <= 0; } }

	}
	public class EffectManager
	{
		private List<EffectEntity> _redLightningEffectList;

		public bool CanDraw { get { return _redLightningEffectList.Count > 0; } }

		public EffectManager()
		{
			_redLightningEffectList = new List<EffectEntity>();
			for(int i = 0; i < 20; i++)
			{
				_redLightningEffectList.Add(new EffectEntity("Comic2", Vector2.Zero).Deactive());
			}
		}
		public void Update()
		{
			foreach(var ent in _redLightningEffectList)
			{
				if(!ent.IsDead)
					ent.Update();
			}
		}
		public void Draw(SpriteBatch sb)
		{
			var tex = TemplateMod.Instance.GetTexture("Images/Box");
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.Transform);
			foreach (var ent in _redLightningEffectList)
			{
				if (!ent.IsDead)
				{
					ent.Effect.Parameters["uIntensity"].SetValue(0.6f * (ent.TimeLeft / 40.0f));
					ent.Effect.Parameters["uColor"].SetValue(Color.Red.ToVector3());
					ent.Effect.Parameters["uLightPos"].SetValue(new Vector3(0.5f, 0.5f, 0.3f));
					ent.Effect.Parameters["uImageSize0"].SetValue(tex.Size());
					ent.Effect.CurrentTechnique.Passes["Pass1"].Apply();
					sb.Draw(tex, new Rectangle((int)(ent.Rect.X - Main.screenPosition.X),
						(int)(ent.Rect.Y - Main.screenPosition.Y), ent.Rect.Width, ent.Rect.Height), Color.White);
				}
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin();
		}

		public void Insert(Vector2 pos)
		{
			bool conflict = false;
			foreach(var ent in _redLightningEffectList)
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
				for (; i < _redLightningEffectList.Count; i++)
				{
					if (_redLightningEffectList[i].IsDead)
					{
						break;
					}
				}
				if (i != _redLightningEffectList.Count)
					_redLightningEffectList[i].SetPos(pos);
			}
		}
	}
}
