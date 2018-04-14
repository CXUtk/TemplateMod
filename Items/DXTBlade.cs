/*
 * 这是一个基本剑型武器的例子
 */
 
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

// 注意这里命名空间变了，多了个.Items
// 因为这个文件在Items文件夹，而读取图片的时候是根据命名空间读取的，如果写错了可能图片就读不到了
namespace TemplateMod.Items
{
	// 保证类名跟文件名一致，这样也方便查找
    public class DXTBlade : ModItem
    {
		// 设置物品名字，描述的地方
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			// 这里可以写中文了ヾ(@^▽^@)ノ
			DisplayName.SetDefault("神秘之剑");

			// 物品的描述，加入换行符 '\n' 可以多行显示哦
			Tooltip.SetDefault("这把剑看起来很有用\n" +
				"虽然我并不这么觉得");
		}

		// 最最最重要的物品基本属性部分
		public override void SetDefaults()
		{
			// 伤害！想都不要想，后面这个值随便改吧，但是不要超过2147483647
			// 不然…… 你试试就知道了
			item.damage = 50;

			// 击退，你懂的，但是这个击退有个上限就是20，超过20击退效果跟20没什么区别
			// 后面的 'f' 表示这是个小数，8.25
			item.knockBack = 8.25f;

			// 物品的基础暴击几率，比正常物品多了 10% 呢
			item.crit = 10;

			// 物品的稀有度，由-1到13越来越高，具体参考维基百科或者我的博客
			item.rare = 7;

			// 攻击速度和攻击动画持续时间！
			// 这个数值越低越快，因为TR游戏速度每秒是60帧，这里的30就是
			// 30.0 / 60.0 = 0.5 秒挥动一次！也就是一秒两次
			// 一般来说我们要把这两个值设成一样，但也有例外的时候，我们以后会讲
			item.useTime = 5;
			item.useAnimation = 5;

			// 使用方式，这个值决定了武器使用时到底是按什么样的动画播放
			// 1 代表挥动，也就是剑类武器！
			// 2 代表像药水一样喝下去，emmmm这个放在剑上会不会很奇怪（吞
			// 3 代表像同志短剑一样刺x 出去
			// 4 唔，这个一般不是用在武器上的，想象一下生命水晶使用的时候的动作
			// 5 手持，枪、弓、法杖类武器的动作，用途最广
			item.useStyle = 3;

			// 决定了这个武器鼠标按住不放能不能一直攻击， true代表可以, false代表不行
			// （鼠标别按废了
			item.autoReuse = true;

			// 决定了这个武器的伤害属性，
			// melee 代表近战
			// ranged 代表远程
			// magic 代表膜法，不，魔法
			// summon 代表召唤
			// thrown 代表投掷
			item.melee = true;

			// 物品的价格，这里用sellPrice，也就是卖出物品的价格作为基准
			// 这件物品卖出时会获得 0白金 1金 60银 0铜 这么多的钱 （就这？
			item.value = Item.sellPrice(0, 1, 60, 0);

			// 设置这个物品使用时发出的声音，以后会讲到怎么调出其他声音
			// 在这里我用的是普通的挥剑声音
			item.UseSound = SoundID.Item1;

			// 物品使用的时候变大的倍数，这里是1.2倍，也就是比贴图大1.2倍（emm
			item.scale = 1.2f;

			// 物品的碰撞体积大小，可以与贴图无关，但是建议设为跟贴图一样的大小
			// 不然鬼知道会不会发生奇怪的事情
			item.width = 44;
			item.height = 44;

			// 最大堆叠数量，唔，对于武器来说，即使你堆了99个，使用的时候还是只有一个的效果
            item.maxStack = 1;


			// 这就是一个基本的剑类MOD武器所需要的属性，后面有别的武器的时候还会有更多属性，不要着急
			// 可以尝试改一改这些数据，在游戏中看看效果
        }


		// 物品合成表的设置部分
        public override void AddRecipes()
        {
			// 一定要写的
            ModRecipe recipe1 = new ModRecipe(mod);

			// 这里我设置了这把剑要1个木块就能制作
			recipe1.AddIngredient(ItemID.Wood, 1);

			// 我设置了这把剑要在工作台旁边合成
			recipe1.AddTile(TileID.WorkBenches);

			// 这两个函数确保合成表被加进游戏中了
			recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
}
