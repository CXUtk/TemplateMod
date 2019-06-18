using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TemplateMod.Utils;

namespace TemplateMod.Items
{
	public class ExampleLaserWeapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("射出毁灭一切的射线");
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.noMelee = true;
			item.magic = true;
			item.channel = true; // 蓄力武器
			item.mana = 5;
			item.rare = 5;
			item.width = 28;
			item.height = 30;
			item.useTime = 20;
			item.UseSound = SoundID.Item13;
			item.useStyle = 5;
			item.shootSpeed = 14f;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("ExampleLaser");
			item.value = Item.sellPrice(0, 0, 3);
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
