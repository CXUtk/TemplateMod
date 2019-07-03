
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TemplateMod.VecMap;
using TemplateMod.Utils;

namespace TemplateMod.Items
{
	// 保证类名跟文件名一致，这样也方便查找
	public class DarkLance : ModItem
	{
		// 设置物品名字，描述的地方
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("诡异长矛");
			Tooltip.SetDefault("用诡异手法铸炼而成（");
		}

		// 最最最重要的物品基本属性部分
		public override void SetDefaults()
		{
			// 懒得改属性，直接复制
			item.CloneDefaults(ItemID.Spear);
			item.knockBack = 10f;
			item.damage = 100;
			item.shoot = mod.ProjectileType("DarkLancePro");
		}


		public override void AddRecipes()
		{
			ModRecipe recipe1 = new ModRecipe(mod);
			// 铁锭或者铅锭100个
			recipe1.AddRecipeGroup("IronBar", 100);
			recipe1.AddTile(TileID.Anvils);
			recipe1.SetResult(this);
			recipe1.AddRecipe();
		}
	}
}