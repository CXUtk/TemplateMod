﻿/*
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
	public class TemplateGun : ModItem
	{

		const double RAD = 1.0;

		static MyMatrix rotation = new MyMatrix(new double[3, 3]
		{
			{Math.Cos(RAD), -Math.Sin(RAD), 0.0},
			{Math.Sin(RAD), Math.Cos(RAD),  0.0},
			{0.0,           0.0,            1.0}
		});
		private string[] _strList = new string[]
		{
			//"D", "X", "T", "s", "T"
			"小","裙","子"
		};

		private int counter = 0;
		// 设置物品名字，描述的地方
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			// 这里可以写中文了ヾ(@^▽^@)ノ
			DisplayName.SetDefault("非洲手枪");

			// 物品的描述，加入换行符 '\n' 可以多行显示哦
			Tooltip.SetDefault("可以射出子弹，然后呢？");

			foreach(var mod in ModLoader.Mods) // => 之前是ModLoader.LoadedMods
			{

			}
		}

		// 最最最重要的物品基本属性部分
		public override void SetDefaults()
		{
			// 伤害！知道该做什么了吧，后面这个值随便改吧，但是不要超过2147483647
			// 不然…… 你试试就知道了
			item.damage = 123;

			// 击退，你懂的，但是这个击退有个上限就是20.0f，超过20击退效果跟20没什么区别
			// 后面的 'f' 表示这是个小数，0.25
			item.knockBack = 0.25f;

			// 物品的基础暴击几率，比正常物品少了 4% 呢
			item.crit = -4;

			// 物品的稀有度，由-1到13越来越高，具体参考维基百科或者我的博客
			item.rare = 2;

			// 攻击速度和攻击动画持续时间！
			// 这个数值越低越快，因为TR游戏速度每秒是60帧，这里的30就是
			// 10.0 / 60.0 = 0.1666... 秒挥动一次！也就是一秒6次
			// 一般来说我们要把这两个值设成一样，但也有例外的时候，我们以后会讲
			item.useTime = 4;
			item.useAnimation = 4;

			// 使用方式，这个值决定了武器使用时到底是按什么样的动画播放
			// 1 代表挥动，也就是剑类武器！
			// 2 代表像药水一样喝下去，emmmm这个放在剑上会不会很奇怪（吞
			// 3 代表像同志短剑一样刺x 出去
			// 4 唔，这个一般不是用在武器上的，想象一下生命水晶使用的时候的动作
			// 5 手持，枪、弓、法杖类武器的动作，用途最广，这里就用它
			item.useStyle = 5;
			item.holdStyle = ItemHoldStyleID.HarpHoldingOut;

			// 决定了这个武器鼠标按住不放能不能一直攻击， true代表可以
			// （我就是要按废你的鼠标！
			item.autoReuse = true;

			// 决定了这个武器的伤害属性，
			// melee 代表近战
			// ranged 代表远程
			// magic 代表膜法，不，魔法
			// summon 代表召唤
			// thrown 代表投掷
			item.ranged = true;

			// 物品的价格，这里用sellPrice，也就是卖出物品的价格作为基准
			// 这件物品卖出时会获得 0白金 1金 60银 0铜 这么多的钱 （就这？
			item.value = Item.sellPrice(0, 1, 0, 0);

			// 设置这个物品使用时发出的声音，以后会讲到怎么调出其他声音
			// 在这里我用的是开枪的声音
			item.UseSound = SoundID.Item36;

			// 物品的碰撞体积大小，可以与贴图无关，但是建议设为跟贴图一样的大小
			// 不然鬼知道会不会发生奇怪的事情（无所谓的）
			item.width = 24;
			item.height = 24;

			// 让它变小一点
			item.scale = 0.85f;

			// 最大堆叠数量，唔，对于一般武器来说，即使你堆了99个，使用的时候还是只有一个的效果
			item.maxStack = 1;

			//-------------------------------------------------------------------------
			// 接下来就是枪械武器独特的属性

			// noMelee代表这个武器使用的时候贴图会不会造成伤害
			// 如果你希望开枪的时候你的手枪还能敲在敌人头上就把它设为false
			// 反正我不希望：（，就当枪本身没有伤害吧
			item.noMelee = true;

			// 决定枪射出点什么和射出的速度的量
			// 这里我让枪射出子弹，并且以 （7像素 / 帧） 的速度射出去 
			item.shoot = mod.ProjectileType("SpecialPro2");
			item.shootSpeed = 3f;

			// 选择这个枪射出（的时候消耗什么作为弹药，这里选择子弹
			// 你也可以删（或者注释）掉这一句，这样枪就什么都不消耗了
			//【重要】如果设置了消耗什么弹药，那么之前shoot设置的值就会被弹药物品的属性所覆盖
			// 也就是说，你到底射出的是什么就由弹药决定了！
			// item.useAmmo = AmmoID.Bullet;

			// 好了，到这里差不多就是一个普通的枪需要填写的属性了
			// 至于更高级的枪怎么制作，嘿嘿，往后看吧。

		}

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
			player.statManaMax = player.statManaMax2 = 500;
			player.statMana = 500;
			//List<Vector2> list = LoadVec.GetVecMap(_strList[(counter++) % _strList.Length]);
			//Vector2 orig = new Vector2(speedX, speedY);
			//float normal = orig.ToRotation();
			//foreach (var vec in list)
			//{
			//	Vector2 vel = vec * 5;
			//	MyMatrix tmp = rotation * MyMatrix.FromVector2(vel);
			//	vel = tmp.ExtractVector2();
			//	Projectile.NewProjectile(position, Vector2.Normalize(orig * 5) + vel, type, 100, 10, player.whoAmI);
			//}
			//Main.mouseX += Main.rand.Next(-5, 5);
			//Main.mouseY += Main.rand.Next(-5, 5);
			//type = ProjectileID.Bullet;
			Vector2 orig = new Vector2(speedX, speedY);
			// 随机角度
			float rotaion = orig.ToRotation() + Main.rand.NextFloatDirection() * MathHelper.Pi * 0.1f;
			player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
			player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
				rotaion.ToRotationVector2().X * player.direction);
			Projectile.NewProjectile(position, rotaion.ToRotationVector2() * 10f, type, 100, 10, player.whoAmI);
			//NPC target = null;
			//// 最大寻敌距离
			//float distanceMax = 1000f;
			//foreach (NPC npc in Main.npc)
			//{
			//	// 如果npc活着且敌对
			//	if (npc.active && !npc.friendly)
			//	{
			//		// 计算距离
			//		float currentDistance = Vector2.Distance(npc.Center, player.Center);
			//		// 如果npc距离比当前最大距离小
			//		if (currentDistance < distanceMax)
			//		{
			//			// 就把最大距离设置为npc和玩家的距离
			//			// 并且暂时选取这个npc为距离最近npc
			//			distanceMax = currentDistance;
			//			target = npc;
			//		}
			//	}
			//}
			//// 如果找到符合条件的npc
			//if (target != null)
			//{
			//	Vector2 vec = target.Center - player.Center;
			//	vec.Normalize();
			//	Projectile.NewProjectile(position, vec * 10, type, 100, 10, player.whoAmI);
			//}
			return false;
		}


		// 物品合成表的设置部分
		// 因为我懒，所以设置的跟那把剑一样，emmm
		public override void AddRecipes()
		{
			// 一定要写的
			ModRecipe recipe1 = new ModRecipe(mod);

			// 这里我设置了这把剑要1个木头就能制作
			recipe1.AddIngredient(ItemID.MoltenFury, 10);

			// 我设置了这把剑要在铁砧旁边合成
			recipe1.AddTile(TileID.Anvils);

			// 这两个函数确保合成表被加进游戏中了
			recipe1.SetResult(this);
			recipe1.AddRecipe();
		}
	}
}