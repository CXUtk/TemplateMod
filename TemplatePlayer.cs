﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TemplateMod.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace TemplateMod
{
	public class TemplatePlayer : ModPlayer
	{
		public float stealth;

		/// <summary>
		/// 不死之身
		/// </summary>
		public bool Undead
		{
			get;
			set;
		}

		public bool Gliders
		{
			get;
			set;
		}

		// CD不需要每帧重置，所以不用ResetEffects
		private int UndeadCD = 0;



		public override void ResetEffects()
		{
			stealth = 1.0f;
			Undead = false;
			Gliders = false;
		}

		public override void PostUpdate()
		{
			if(Undead && UndeadCD > 0)
			{
				UndeadCD--;
			}
			if (TemplateMod.SelectMode && !BuildingUIState.Instance.MouseInside)
			{
				if(Main.mouseLeft && Main.mouseLeftRelease)
				{
					TemplateMod.SelectUpperLeft = new Point((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
				}
				if (Main.mouseRight && Main.mouseRightRelease)
				{
					TemplateMod.SelectLowerRight = new Point((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
				}
			}
			if (TemplateMod.BuildMode && !BuildingUIState.Instance.MouseInside)
			{
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					if (BuildingUIState.Instance.selectedItem != -1)
					{
						BuildingUIState.Instance.PlaceSelected();
					}
				}
				
			}
		}

		public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
		{
			if (stealth < 1.0f)
			{
				float scale = stealth;
				scale = MathHelper.Clamp(stealth, 0.0f, 1.0f);
				drawInfo.eyeWhiteColor = Color.Multiply(drawInfo.eyeWhiteColor, scale);
				drawInfo.eyeColor = Color.Multiply(drawInfo.eyeColor, scale);
				drawInfo.hairColor = Color.Multiply(drawInfo.hairColor, scale);
				drawInfo.faceColor = Color.Multiply(drawInfo.faceColor, scale);
				drawInfo.bodyColor = Color.Multiply(drawInfo.bodyColor, scale);
				drawInfo.legColor = Color.Multiply(drawInfo.legColor, scale);
				drawInfo.shirtColor = Color.Multiply(drawInfo.shirtColor, scale);
				drawInfo.underShirtColor = Color.Multiply(drawInfo.underShirtColor, scale);
				drawInfo.pantsColor = Color.Multiply(drawInfo.pantsColor, scale);
				drawInfo.shoeColor = Color.Multiply(drawInfo.shoeColor, scale);
				drawInfo.headGlowMaskColor = Color.Multiply(drawInfo.headGlowMaskColor, scale);
				drawInfo.bodyGlowMaskColor = Color.Multiply(drawInfo.bodyGlowMaskColor, scale);
				drawInfo.legGlowMaskColor = Color.Multiply(drawInfo.legGlowMaskColor, scale);
				drawInfo.armGlowMaskColor = Color.Multiply(drawInfo.armGlowMaskColor, scale);
				drawInfo.upperArmorColor = Color.Multiply(drawInfo.upperArmorColor, scale);
				drawInfo.lowerArmorColor = Color.Multiply(drawInfo.lowerArmorColor, scale);
				drawInfo.middleArmorColor = Color.Multiply(drawInfo.middleArmorColor, scale);
				// 减少仇恨
				player.aggro = -1000;
			}
		}

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			base.ProcessTriggers(triggersSet);
		}



		public override void UpdateBiomeVisuals()
		{
			//player.ManageSpecialBiomeVisuals("Template:UltraLight", TemplateMod.TwistedStrength > 0f);
			//player.ManageSpecialBiomeVisuals("Template:Disort", true);
		}
		//public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		//{
		//	if (Main.rand.Next(5) == 0)
		//	{
		//		Projectile.NewProjectileDirect(target.Center + new Vector2(0, -500), new Vector2(0, 5),
		//			mod.ProjectileType("TestPro"), 155, 7f, player.whoAmI);
		//	}
		//}

		//public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		//{
		//	if (Main.rand.Next(4) == 0 && proj.type != mod.ProjectileType("TestPro"))
		//	{
		//		Projectile.NewProjectileDirect(target.Center + new Vector2(0, -500), new Vector2(0, 5),
		//		mod.ProjectileType("TestPro"), 155, 7f, player.whoAmI);
		//	}
		//}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if(Undead && UndeadCD == 0)
			{
				// 冷却五分钟
				UndeadCD = 18000;
				return false;
			}
			return true;
		}

	}
}
