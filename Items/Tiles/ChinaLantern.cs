using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod.Items.Tiles
{
    public class ChinaLantern : ModItem
    {
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("灯笼");
		}
		public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 99;
            item.consumable = true;
            item.createTile = mod.TileType("Lantern");
            item.width = 30;
            item.height = 30;
        }
        public override void AddRecipes()
        {
            var recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.Wood, 5);
            recipe1.AddIngredient(ItemID.Torch, 2);
            recipe1.SetResult(this);
            recipe1.AddTile(TileID.WorkBenches);
            recipe1.AddRecipe();
        }
    }
}
