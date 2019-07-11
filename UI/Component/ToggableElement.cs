using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TemplateMod.UI.Component
{
	/// <summary>
	/// 可以切换Enable以及Draw
	/// </summary>
	public abstract class ToggableElement : UIElement
	{
		public bool Enabled
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}

		public int Index
		{
			get;
			set;
		}

		protected ToggableElement() : base()
		{
			Enabled = true;
			Visible = true;
			Index = 0;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (!Visible) return;
			base.DrawSelf(spriteBatch);
		}

	}
}
