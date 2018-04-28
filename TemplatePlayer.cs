using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace TemplateMod
{
	public class TemplatePlayer : ModPlayer
	{
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (TemplateMod.DustTestKey.JustPressed)
			{
				TemplateMod.ShowDustTestUI = !TemplateMod.ShowDustTestUI;
			}
		}

	}
}
