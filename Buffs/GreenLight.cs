using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TemplateMod.Utils;

// 命名空间要跟文件夹所在路径匹配
namespace TemplateMod.Buffs
{
	// 同理类名与文件名匹配（说了好多次了）
	public class GreenLight : ModBuff
	{
		// 设置buff基本属性的地方，跟物品的SetDefault很像呢
		public override void SetDefaults()
		{
			// 设置buff名字和描述
			DisplayName.SetDefault("绿光");
			Description.SetDefault("爱是一道光。。。");

			// 因为buff严格意义上不是一个TR里面自定义的数据类型，所以没有像buff.XXXX这样的设置属性方式了
			// 我们需要用另外一种方式设置属性

			// 这个属性决定buff在游戏退出再进来后会不会仍然持续，true就是不会，false就是会
			Main.buffNoSave[Type] = true;

			// 用来判定这个buff算不算一个debuff，如果设置为true会得到TR里对于debuff的限制，比如无法取消
			Main.debuff[Type] = false;

			// 当然你也可以用这个属性让这个buff即使不是debuff也不能取消，设为false就是不能取消了
			this.canBeCleared = false;

			// 决定这个buff是不是照明宠物的buff，以后讲宠物和召唤物的时候会用到的，现在先设为false
			Main.lightPet[Type] = false;

			// 决定这个buff会不会显示持续时间，false就是会显示，true就是不会显示，一般宠物buff都不会显示
			Main.buffNoTimeDisplay[Type] = false;

			// 决定这个buff在专家模式会不会持续时间加长，false是不会，true是会
			this.longerExpertDebuff = false;

			// 如果这个属性为true，pvp的时候就可以给对手加上这个debuff/buff
			Main.pvpBuff[Type] = true;

			// 决定这个buff是不是一个装饰性宠物，用来判定的，比如消除buff的时候不会消除它
			Main.vanityPet[Type] = false;

			// 差不多就这么多，其实实际用途上不会用到这么多属性，比如debuff只用设置noSave和debuff就行了
			// 但是我都写下来了给你们作参考
			// 还有一些属性接下来会用到
		}

		// 这是一个重写函数，发生在buff存在于玩家身上的时候，第一个参数不介绍了，
		// 第二个参数是buff在玩家身上的位置，因为玩家很有可能同时存在很多buff，如何从中选中这个buff就是要靠这个值了
		public override void Update(Player player, ref int buffIndex)
		{
			player.stealth = 0.1f;
			// 冒绿光
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustDirect(player.position, player.width, 10,
					MyDustId.GreenFx, 0, -2f, 100, Color.White, 1.15f);
				dust.noGravity = true;
			}

			// 让玩家飘起来
			if(player.velocity.Y > -0.1f)
			{
				player.velocity.Y = -1f;
			}
			// buff消失前的一瞬间，绿光爆发233333
			// player.buffTime[buffIndex] 就是这个buff的剩余时间
			if (player.buffTime[buffIndex] < 1)
			{
				for (int i = 0; i < 100; i++)
				{
					Dust.NewDustDirect(player.position, player.width, 10,
						MyDustId.GreenFx, 0, -2f, 100, Color.White, 1.5f);
				}
			}
		}

		// 这是一个重写函数，发生在buff存在于NPC身上的时候，第一个参数是被施加buff的NPC，第二个同上
		public override void Update(NPC npc, ref int buffIndex)
		{
			// 自己写吧
			// 冒绿光
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustDirect(npc.position, npc.width, 10,
					MyDustId.GreenFx, 0, -2f, 100, Color.White, 1.15f);
				dust.noGravity = true;
			}
			if (npc.lifeRegen > 0) npc.lifeRegen = 0;
			npc.lifeRegen -= 120;
		}
	}
}
