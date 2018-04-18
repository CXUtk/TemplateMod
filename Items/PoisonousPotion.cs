using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

// �����ռ���Items�ļ�����
namespace TemplateMod.Items
{
	// ���ķ��룬�ж���ҩˮ��ͬ�����������ļ�����ͬ
	public class PoisonousPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// ��Ʒ����
			DisplayName.SetDefault("�ж�ҩˮ");

			// ��Ʒ����
			Tooltip.SetDefault("��Ҫ����ʲôЧ�������˾�֪����");
		}

		// ��������Ҫ����Ʒ�������Բ���
		public override void SetDefaults()
		{
			// ͬ����������ͼ��С
			item.width = 14;
			item.height = 24;

			// ͬ�������ú�ҩˮ�Ķ�������ʱ��
			item.useAnimation = 17;
			item.useTime = 17;

			// ��Ʒ�Ķѵ�����
			item.maxStack = 30;

			// ��Ʒ��ʹ�÷�ʽ�����ǵ�2��ʲô��
			item.useStyle = 2;

			// ��ҩˮ������
			item.UseSound = SoundID.Item3;

			// ϡ�ж�
			item.rare = 5;

			// ��ֵ
			item.value = Item.sellPrice(0, 0, 50, 0);

			// *����-���������Ʒʹ���Ժ�᲻����٣�true����ʹ�ú���Ʒ����һ����Ĭ��Ϊfalse
			item.consumable = true;

			// *����-����ʹ�ö������ֺ����ת��᲻��Ӱ�춯���ķ���true���ǻᣬĬ��Ϊfalse
			item.useTurn = true;

			// *����-����TR�ڲ�ϵͳ�������Ʒ��һ������ҩˮ��Ʒ������TRϵͳ������Ŀ�ģ�����һ����ҩˮ����Ĭ��Ϊfalse
			// item.potion = false;

			// *����-���ҩˮ�ܸ���ҼӶ���Ѫ����potionһ��ʹ�ú���ҩ�ͻ��п�ҩ��debuff
			// item.healLife = 50;

			// *����-��buff�ķ���1��������Ʒ��buffTypeΪbuff��ID
			// �������������Ż�debuff��2333
			item.buffType = BuffID.OnFire;

			// *����-��������Ʒ��������ʾbuff����ʱ��
			item.buffTime = 60000;
		}

		// ����Ʒ�Ӻϳɱ�
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			// ԭ�ϣ�һƿˮ
			recipe.AddIngredient(ItemID.BottledWater, 1);

			// ��������̨��Ҳ������ҩˮ�Ĺ���̨���ϳ�
			recipe.AddTile(TileID.AlchemyTable);

			// �㶮��
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		/* ��������Ҫ�Ĳ��֣�����������������ʹ�������Ʒ��ʱ�򣬳���SetDefault����ļ�����������
		 * �Զ���ҩˮ�����Ժ��Ч������Ҫ������������������
		 * ����������Զ�Player����ң������Խ����޸� */
		public override bool UseItem(Player player)
		{
			// ����Ҽ�buff�ĵڶ�����ʽ���Ƽ���
			// ����Ҽ����ж�buff������ 60000 / 60 = 1000��
			// ��һ����buff��ID���ڶ��������ʱ��
			player.AddBuff(BuffID.Poisoned, 60000);

			// ����Ҽ����Ͷ�buff������ 60000 / 60 = 1000�� 
			player.AddBuff(BuffID.Venom, 60000);

			// �ٺ�
			// player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " ��ũҩ��������"), 9999, 0);
			return true;
		}
	}
}