using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace TemplateMod
{
	public class TemplateWorld : ModWorld
	{
		public override void PostUpdate()
		{
			//TemplateMod._effectManager.Update();
			//TemplateMod._twistEffectManager.Update();
			base.PostUpdate();
		}
	}
}
