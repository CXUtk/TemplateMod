using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace TemplateMod.UI
{
	public class CDInterfaceManager
	{
		private readonly List<ConditionalInterface> _conditionalInterfaces;

		public CDInterfaceManager()
		{
			_conditionalInterfaces = new List<ConditionalInterface>();
		}

		public void Add(ConditionalInterface _interface)
		{
			_conditionalInterfaces.Add(_interface);
		}

		public void Update(GameTime gameTime)
		{
			foreach (var ui in _conditionalInterfaces)
			{
				if (ui.CanShow())
				{
					ui.Update(gameTime);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var ui in _conditionalInterfaces)
			{
				if (ui.CanShow())
				{
					ui.Draw(spriteBatch, Main._drawInterfaceGameTime);
				}
			}
		}
	}
}
