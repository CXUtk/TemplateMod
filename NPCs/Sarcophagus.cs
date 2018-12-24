using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod.NPCs
{
	public abstract class FSM_NPC : ModNPC
	{
		protected int State
		{
			get { return (int)npc.ai[0]; }
			set { npc.ai[0] = value; }
		}

		protected int Timer
		{
			get { return (int)npc.ai[1]; }
			set { npc.ai[1] = value; }
		}

		protected virtual void SwitchState(int state)
		{
			State = state;
		}

	}
}