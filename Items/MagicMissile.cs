/*
 * 这是一个基本枪械类武器的例子
 */

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TemplateMod.VecMap;
using TemplateMod.Utils;

// 注意这里命名空间变了，多了个.Items
// 因为这个文件在Items文件夹，而读取图片的时候是根据命名空间读取的，如果写错了可能图片就读不到了
// 跟那把剑个一样，后面我就不说了
namespace TemplateMod.Items
{
	// 保证类名跟文件名一致，这样也方便查找
	public class MagicMissile : ModItem
	{

		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			DisplayName.SetDefault("猩红磁球飞弹");

			Tooltip.SetDefault("除了掌控飞弹以外还能掌控什么呢？");
		}

		// 最最最重要的物品基本属性部分
		public override void SetDefaults()
		{
			item.damage = 100;
			item.knockBack = 0.25f;
			item.rare = 8;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 5;
			item.magic = true;
			item.mana = 10;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.UseSound = SoundID.Item9;
			item.width = 24;
			item.height = 24;
			item.maxStack = 1;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("RedMagnetControllable");
			Item.staff[item.type] = true;
			item.shootSpeed = 4f;
			// 新属性item.channel，表示这个物品是否是吟唱施法
			item.channel = true;
			// 如果channel是true，则autoReuse一定要是false
			item.autoReuse = true;

			// 物品方面代码没有太大的变化
		}

		public override void AddRecipes()
		{
			// 一定要写的
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(ItemID.Wood, 1);
			recipe1.AddTile(TileID.Anvils);
			recipe1.SetResult(this);
			recipe1.AddRecipe();
		}
	}
}