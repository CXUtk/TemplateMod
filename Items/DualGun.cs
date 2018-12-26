
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
	public class DualGun : ModItem
	{
		// 设置物品名字，描述的地方
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			// 这里可以写中文了ヾ(@^▽^@)ノ
			DisplayName.SetDefault("双用途掌控者");

			// 物品的描述，加入换行符 '\n' 可以多行显示哦
			Tooltip.SetDefault("左键释放引力射线，右键引爆");
		}

		// 最最最重要的物品基本属性部分
		public override void SetDefaults()
		{
			item.damage = 123;
			item.knockBack = 0.25f;
			item.rare = 7;
			item.useStyle = 5;
			item.holdStyle = ItemHoldStyleID.HarpHoldingOut;
			item.autoReuse = true;
			item.ranged = true;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.UseSound = SoundID.Item36;
			item.width = 24;
			item.height = 24;
			item.scale = 0.85f;
			item.maxStack = 1;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("GravitationalBeam");
			item.shootSpeed = 1f;
			item.channel = true;
			item.autoReuse = false;
			item.useAmmo = AmmoID.None;
			item.useTime = 40;
			item.useAnimation = 40;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		//// 一定要用CanUseItem，保证在物品被使用之前修改属性
		//public override bool CanUseItem(Player player)
		//{
		//	if (player.altFunctionUse != 2)
		//	{
		//		item.shoot = mod.ProjectileType("GravitationalBeam");
		//		item.shootSpeed = 1f;
		//		item.channel = true;
		//		item.autoReuse = false;
		//		item.useAmmo = AmmoID.None;
		//		item.useTime = 40;
		//		item.useAnimation = 40;
		//	}
		//	else
		//	{
		//		item.shoot = mod.ProjectileType("ExplodingBullet");
		//		item.shootSpeed = 5f;
		//		item.useAmmo = AmmoID.Bullet;
		//		item.useTime = 4;
		//		item.useAnimation = 4;
		//		item.channel = false;
		//		item.autoReuse = true;
		//	}
		//	return true;
		//}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0.0f, 0.0f);
		}
		public override Vector2? HoldoutOrigin()
		{
			return base.HoldoutOrigin();
		}
		public override void HoldStyle(Player player)
		{
			if (Math.Abs(Main.time % 20 - 10.0f) < 0.01)
			{
				player.bodyFrame.Y = player.bodyFrame.Height * 2;
			}
			else if (Main.time % 20 < 1)
			{
				player.bodyFrame.Y = player.bodyFrame.Height * 4;
			}
		}
		public override bool HoldItemFrame(Player player)
		{
			if (Math.Abs(Main.time % 20 - 10.0f) < 0.01)
			{
				player.bodyFrame.Y = player.bodyFrame.Height * 2;
			}
			else if (Main.time % 20 < 1)
			{
				player.bodyFrame.Y = player.bodyFrame.Height * 4;
			}

			//player.bodyFrame.Y += player.bodyFrame.Height * 20;
			//if(player.bodyFrame.Y > player.bodyFrame.Height * 20)
			//{
			//	player.bodyFrame.Y = 0;
			//}
			return false;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.Next(10) < 4;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			Vector2 orig = new Vector2(speedX, speedY);
			// 随机角度
			float rotaion = orig.ToRotation() + Main.rand.NextFloatDirection() * MathHelper.Pi * 0.1f;
			player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
			player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
				rotaion.ToRotationVector2().X * player.direction);
			Projectile.NewProjectile(position, rotaion.ToRotationVector2() * 10f, type, 100, 10, player.whoAmI);
			return false;
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