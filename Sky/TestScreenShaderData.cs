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

namespace TemplateMod
{
	public class TestScreenShaderData : ScreenShaderData
	{
		public TestScreenShaderData(string passName) : base(passName)
		{
		}

		public TestScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		public override void Apply()
		{
			try
			{
				if(TemplateMod.TwistedStrength > 0)
				{
					TemplateMod.TwistedStrength -= 0.15f;
					UseIntensity(TemplateMod.TwistedStrength);
				}
				this.Shader.Parameters["uLengthSq"].SetValue(0.03f);
				this.Shader.Parameters["uEffectPos"].SetValue(TemplateMod.TwistedPos - Main.screenPosition);
				base.Apply();
			}
			catch(Exception ex)
			{
				WorldGen.SaveAndQuit();
			}
		}

	}
}
