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
	public class UICDButton : UIButton
	{
		private int coolDown;
		public int CoolDownCount
		{
			get; set;
		}
		public UICDButton(Texture2D texture, bool withBox = true) : base(texture, withBox)
		{
			CoolDownCount = 30;
			coolDown = 0;
		}

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
			coolDown = CoolDownCount;
			this.Enabled = false;
		}


		public override void Update(GameTime gameTime)
		{
			if(coolDown > 0)
			{
				coolDown--;
			}
			else
			{
				this.Enabled = true;
			}
			base.Update(gameTime);
		}

	}
}
