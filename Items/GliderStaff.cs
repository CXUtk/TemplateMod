using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using TemplateMod.Projectiles.Summoner;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod.Items
{
	public class GliderStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("僚机召唤杖");
			Tooltip.SetDefault("产生可控制的僚机为你作战");
		}
		public override void SetDefaults()
		{
			item.height = 32;
			item.width = 32;
			item.maxStack = 1;
			item.rare = 7;
			item.damage = 45;
			item.value = Item.buyPrice(0, 54, 0, 0);
			item.noMelee = true;
			item.useTime = 30;
			item.knockBack = 1f;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.mana = 10;
			item.crit = 10;
			Item.staff[item.type] = true;
			item.UseSound = SoundID.Item44;
			item.summon = true;
			item.buffType = mod.BuffType<Buffs.GliderBuff>();
			item.buffTime = 3600;
			item.shoot = mod.ProjectileType("GliderPro");
			item.shootSpeed = 10f;

		}
		public override void HoldItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer && Main.mouseRight)
			{
				foreach (var proj in Main.projectile.Where(p => p.active && p.friendly && p.type == item.shoot && p.owner == player.whoAmI))
				{
					GliderPro pro = (GliderPro)proj.modProjectile;
					pro.TargetLocation = Main.MouseWorld;
				}
			}
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			//if (player.ownedProjectileCounts[mod.ProjectileType("ExecutionerPro")] == 0)
			//{
			//    Projectile.NewProjectile(Main.MouseWorld, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)),
			//        mod.ProjectileType("ExecutionerPro"),
			//        damage, knockBack, player.whoAmI);
			//}
			//else
			//{
			//    foreach(Projectile p in Main.projectile)
			//    {
			//        if(p.active && p.friendly && p.owner == player.whoAmI && p.type == mod.ProjectileType("ExecutionerPro"))
			//        {
			//            p.Kill();
			//        }
			//    }
			//    Projectile.NewProjectile(Main.MouseWorld, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), mod.ProjectileType("ExecutionerPro"),
			//        damage, knockBack, player.whoAmI);
			//}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe re = new ModRecipe(mod);
			re.AddIngredient(ItemID.HallowedBar, 18);
			re.AddIngredient(ItemID.SoulofMight, 5);
			re.AddIngredient(ItemID.SoulofLight, 15);
			re.AddIngredient(ItemID.SoulofNight, 15);
			re.AddTile(TileID.MythrilAnvil);
			re.SetResult(this);
			re.AddRecipe();

			re = new ModRecipe(mod);
			re.AddIngredient(ItemID.HallowedBar, 18);
			re.AddIngredient(ItemID.SoulofSight, 5);
			re.AddIngredient(ItemID.SoulofLight, 15);
			re.AddIngredient(ItemID.SoulofNight, 15);
			re.AddTile(TileID.MythrilAnvil);
			re.SetResult(this);
			re.AddRecipe();

			re = new ModRecipe(mod);
			re.AddIngredient(ItemID.HallowedBar, 18);
			re.AddIngredient(ItemID.SoulofFright, 5);
			re.AddIngredient(ItemID.SoulofLight, 15);
			re.AddIngredient(ItemID.SoulofNight, 15);
			re.AddTile(TileID.MythrilAnvil);
			re.SetResult(this);
			re.AddRecipe();
		}
	}
}