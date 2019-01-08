/*
 * 这是一个基本饰品的例子
 */

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TemplateMod.Utils;
using System.Windows.Forms;

// 不说了
namespace TemplateMod.Items
{
	// 也不说了
	public class PurpleStone : ModItem
	{
		List<MyMatrix> transPos = new List<MyMatrix>();


		// 设置物品名字，描述的地方
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("紫气东来");
			Tooltip.SetDefault("整个人都都发紫了");
		}

		// 最最最重要的物品基本属性部分
		public override void SetDefaults()
		{
			// 跟以前没啥区别
			item.width = 22;
			item.height = 22;
			
			// 重点在这里，这个属性设为true才能带在身上
			item.accessory = true;

			// 物品的面板防御数值，装备了以后就会增加
			item.defense = 16;

			item.rare = 8;
			item.value = Item.sellPrice(0, 5, 0, 0);
			
			// 这个属性代表这是专家模式专有物品，稀有度颜色会是彩虹的！
			item.expert = true;
			//for(float f = 0.0f; f < 1.0f; f += 0.11f)
			//{
			//	transPos.Add(MyMatrix.FromVector24(Vector2.Lerp(new Vector2(0, -100f), new Vector2(85.5f, 50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector24(Vector2.Lerp(new Vector2(0, -100f), new Vector2(-85.5f, 50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector24(Vector2.Lerp(new Vector2(85.5f, 50.0f), new Vector2(-85.5f, 50.0f), f)));
			//}
			//for (float f = 0.0f; f < 1.0f; f += 0.11f)
			//{
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(50.0f, 50.0f, 50.0f), new Vector3(50.0f, -50.0f, 50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(50.0f, 50.0f, 50.0f), new Vector3(-50.0f, 50.0f, 50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(50.0f, 50.0f, 50.0f), new Vector3(50.0f, 50.0f, -50.0f), f)));

			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(-50.0f, -50.0f, 50.0f), new Vector3(-50.0f, -50.0f, -50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(-50.0f, -50.0f, 50.0f), new Vector3(50.0f, -50.0f, 50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(-50.0f, -50.0f, 50.0f), new Vector3(-50.0f, 50.0f, 50.0f), f)));

			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(-50.0f, 50.0f, -50.0f), new Vector3(50.0f, 50.0f, -50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(-50.0f, 50.0f, -50.0f), new Vector3(-50.0f, 50.0f, 50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(-50.0f, 50.0f, -50.0f), new Vector3(-50.0f, -50.0f, -50.0f), f)));

			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(50.0f, -50.0f, -50.0f), new Vector3(50.0f, 50.0f, -50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(50.0f, -50.0f, -50.0f), new Vector3(-50.0f, -50.0f, -50.0f), f)));
			//	transPos.Add(MyMatrix.FromVector34(
			//		Vector3.Lerp(new Vector3(50.0f, -50.0f, -50.0f), new Vector3(50.0f, -50.0f, 50.0f), f)));
			//}
			//transPos.Add(MyMatrix.FromVector34(new Vector3(50.0f, 50.0f, 50.0f)));
			//	transPos.Add(MyMatrix.FromVector34(new Vector3(-50.0f, -50.0f, 50.0f)));
			//	transPos.Add(MyMatrix.FromVector34(new Vector3(-50.0f, 50.0f, 50.0f)));
			//	transPos.Add(MyMatrix.FromVector34(new Vector3(50.0f, -50.0f, 50.0f)));
			//	transPos.Add(MyMatrix.FromVector34(new Vector3(50.0f, 50.0f, -50.0f)));
			//	transPos.Add(MyMatrix.FromVector34(new Vector3(-50.0f, -50.0f, -50.0f)));
			//	transPos.Add(MyMatrix.FromVector34(new Vector3(-50.0f, 50.0f, -50.0f)));
			//	transPos.Add(MyMatrix.FromVector34(new Vector3(50.0f, -50.0f, -50.0f)));
			for (float i = -16.28f; i < 16.28f; i += 0.2f)
			{
				transPos.Add(MyMatrix.FromVector34(new Vector3((float) (50.0f * Math.Cos(i)),
					(float) (50.0f * Math.Sin(i)), i / 0.05f)));
			}
		}


		// 物品合成表的设置部分
		public override void AddRecipes()
		{
			// 一定要写的
			ModRecipe recipe1 = new ModRecipe(mod);

			// 这里我设置了要用999个石头就能做
			recipe1.AddIngredient(ItemID.StoneBlock, 999);

			// 我设置了这把剑要在铁砧旁边合成
			recipe1.AddTile(TileID.Anvils);

			// 这两个函数确保合成表被加进游戏中了
			recipe1.SetResult(this);
			recipe1.AddRecipe();
		}

		// 这个重写函数十分重要，饰品穿在身上的时候会以每秒60次的速度执行这里的代码
		// 这也就是饰品效果放置的地方
		// 可以看到这个函数有两个参数，玩家和是否隐藏了视觉效果
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// 让玩家的生命值上限增加100
			player.statLifeMax2 += 100;

			// 让玩家魔法值上限增加100
			player.statManaMax2 += 100;

			// 玩家近战伤害增加15%，注意这里的值
			player.meleeDamage += 0.15f;

			// 玩家远程伤害增加15%
			player.rangedDamage += 0.15f;

			// 玩家魔法伤害增加15%
			player.magicDamage += 0.15f;

			// 玩家近战暴击率增加20%，注意这是个int
			player.meleeCrit += 20;

			// 玩家的近战攻速增加20%
			player.meleeSpeed += 0.2f;

			// 玩家的移动速度增加70%，最大能设到1，因为tr设置了上限
			player.moveSpeed += 0.7f;

			// 改玩家名字23333333
			player.name = "神一样的人";

			player.attackCD = 0;

			// 玩家魔力消耗减少99%
			player.manaCost -= 0.99f;

			// 增加玩家10召唤物上限
			player.maxMinions += 10;

			// 玩家的跳跃速度增加7.5倍
			player.jumpSpeedBoost += 7.5f;

			// 玩家不受掉落伤害
			player.noFallDmg = true;

			// 玩家不受击退
			player.noKnockback = true;

			// 允许玩家连跳
			player.jumpBoost = true;

			// 自动收税？？？
			player.CollectTaxes();

			// 本来还想写更多属性的，但是现在就写这么多吧，大家应该能看出规律了吧
			// 在写多电脑要炸了吧QAQ
			// 不如写写紫气好了
			player.GetModPlayer<TemplatePlayer>().stealth = 0.1f;
			Vector2 diff = Main.MouseScreen - new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f);
			for (int i = 0; i < transPos.Count; i++)
			{
				MyMatrix tmp = MyMatrix.RotationMatrixTY(diff.X * 0.02);
					/** MyMatrix.ScaleMatrixT(1.0, 1.0, 0.2 + Math.Abs(Math.Sin(Main.time * 0.02f)));*/ /** MyMatrix.RotationMatrixTZ(0.05);// * MyMatrix.TranslationMatrix(15.0, 15.0);*/
				tmp = tmp * transPos[i];
				//transPos[i] = tmp;
				Projectile.NewProjectile(player.Center + tmp.ExtractVector2(),
					Vector2.Zero, mod.ProjectileType("Transform"), 100, 100f, player.whoAmI);
			}

		}
	}
}