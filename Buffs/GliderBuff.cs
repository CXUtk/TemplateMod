using Terraria;
using Terraria.ModLoader;

namespace TemplateMod.Buffs
{
	public class GliderBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("僚机部队");
			Description.SetDefault("僚机群会为你战斗");
			Main.buffNoSave[Type] = true;
			// 不显示buff时间
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>(mod);
			// 如果当前有属于玩家的僚机的弹幕
			if (player.ownedProjectileCounts[mod.ProjectileType("GliderPro")] > 0)
			{
				modPlayer.Gliders = true;
			}
			// 如果玩家取消了这个召唤物就让buff消失
			if (!modPlayer.Gliders)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				// 无限buff时间
				player.buffTime[buffIndex] = 9999;
			}
		}
	}
}