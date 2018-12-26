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
	public class TemplateNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		private int prevAIStyle = -1;
		private bool locked = false;

		public override void ResetEffects(NPC npc)
		{
			locked = false;
		}
		public void LockNPC()
		{
			locked = true;
		}

		public override void SetDefaults(NPC npc)
		{
			base.SetDefaults(npc);
			prevAIStyle = npc.aiStyle;
		}

		public override bool PreAI(NPC npc)
		{
			return !locked;
		}

		public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
		}
		public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
		{
			//Main.spriteBatch.End();
			//Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.Transform);
			////TemplateMod.MODEffectTable["Swirl"].Parameters["uIntensity"].SetValue((float)Math.Sin(Main.time * 0.03));
			//////TemplateMod.MODEffectTable["Comic2"].Parameters["uOpacity"].SetValue(0.3f);
			////TemplateMod.MODEffectTable["Swirl"].CurrentTechnique.Passes["Pass1"].Apply();
			//TemplateMod.MODEffectTable["Bloom"].CurrentTechnique.Passes["Pass1"].Apply();
			return true;
		}

		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
		{
			//spriteBatch.End();
			//spriteBatch.Begin();
		}
	}
}
