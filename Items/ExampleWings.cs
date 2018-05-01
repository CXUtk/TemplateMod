using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;
using Microsoft.Xna.Framework;

// 我仍然需要强调一下这个命名空间与存放位置的关系……
namespace TemplateMod.Items
{
	// 这里设置这个物品为翅膀类型
	[AutoloadEquip(EquipType.Wings)]
	public class ExampleWings : ModItem
	{
		// 设置物品名字和描述的地方
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("模板翅膀");
			Tooltip.SetDefault("这是一个被魔改了的翅膀");
		}

		// 不说了
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Lime;
			item.accessory = true;
		}

		// 熟悉的函数
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// 这里设置翅膀的加速时间，还是以60帧为一秒，这个翅膀的加速时间为3秒
			// 也就是说能持续按3秒空格，已经比一般翅膀要长了
			player.wingTimeMax = 180;

			player.wingTime = 10;
		}

		// 控制翅膀垂直速度以及运动行为的重写函数
		// 我写的值都是正常翅膀的值，请尽情魔改
		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			// 往下降的时候按空格瞬间上升的高度
			ascentWhenFalling = 0.85f;
			// 往上升的时候按空格瞬间上升的高度
			ascentWhenRising = 0.15f;
			// 最大能上升的加速度
			maxCanAscendMultiplier = 1f;
			// 最大上升速度
			maxAscentMultiplier = 3f;
			// 空格平均加速度
			constantAscend = 0.135f;

			// 知道什么叫火箭吗？
		}

		// 控制翅膀水平移动速度以及运动行为的重写函数
		// 我写的值都是正常翅膀的值，请尽情魔改
		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			// 水平速度上限
			speed = 9f;

			// 水平加速度值
			acceleration *= 2.5f;
		}

		public override bool WingUpdate(Player player, bool inUse)
		{
			if (inUse)
			{
				for (int i = 0; i < 2; i++)
				{
					Dust dust = Dust.NewDustDirect(player.Center, 10, 10,
						MyDustId.Fire, 0, 10f, 100, Color.White, Main.rand.NextFloat() + 1.0f);
					Dust dust2 = Dust.NewDustDirect(player.Center, 10, 10,
						MyDustId.Smoke, 0, 10f, 100, Color.White, Main.rand.NextFloat() + 1.0f);
					dust2.noGravity = true;
					dust.noGravity = true;
					dust.velocity.Y += 2f - player.velocity.X * 0.3f;
					dust.noLight = true;
				}
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}