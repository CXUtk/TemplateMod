
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
	public class BlasterCannon2 : ModItem
	{
		// 设置物品名字，描述的地方
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			// 这里可以写中文了ヾ(@^▽^@)ノ
			DisplayName.SetDefault("弹力追踪电磁炮");

			// 物品的描述，加入换行符 '\n' 可以多行显示哦
			Tooltip.SetDefault("弹跳+跟踪+穿透，三合一更高更快更强！");
		}

		// 最最最重要的物品基本属性部分
		public override void SetDefaults()
		{
			item.damage = 123;
			item.knockBack = 0.25f;
			item.rare = 7;
			item.useStyle = 5;
			item.ranged = true;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.width = 24;
			item.height = 24;
			item.scale = 0.85f;
			item.maxStack = 1;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("ChargingBlaster");
			item.shootSpeed = 6f;
			item.channel = true;
			item.autoReuse = false;
			item.mana = 50;
			item.useTime = 40;
			item.useAnimation = 40;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(ItemID.Wood, 10);
			recipe1.AddTile(TileID.Anvils);
			recipe1.SetResult(this);
			recipe1.AddRecipe();
		}
	}
}