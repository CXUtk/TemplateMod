﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TemplateMod.Items
{
	public class ThunderPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("雷霆镐子");
			Tooltip.SetDefault("？？？");
		}

		// 自己写的合法性检测函数
		private bool valid(int x, int y)
		{
			return x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY;
		}

		public override void UseStyle(Player player)
		{
			if (valid(Player.tileTargetX, Player.tileTargetY))
			{
				var tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
				tile.type = TileID.Dirt;
				tile.active(true);
				WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
			}
		}

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 25;
			item.useTime = 3;
			item.knockBack = 5f;
			item.width = 20;
			item.height = 12;
			item.damage = 10;
			item.pick = 1000;
			item.axe = 14;
			item.hammer = 90;
			item.UseSound = SoundID.Item1;
			item.rare = 4;
			item.value = 54000;
			item.melee = true;
			item.scale = 1.15f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

