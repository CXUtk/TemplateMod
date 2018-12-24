using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TemplateMod.Utils;

// 注意命名空间
namespace TemplateMod.Projectiles
{
    public class FalseBeam : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// 弹幕名字
			DisplayName.SetDefault("假剑气");
		}
		public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
			projectile.scale = 1.5f;
            projectile.friendly = true;
			//projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.melee = true;
            projectile.timeLeft = 600;
			projectile.ignoreWater = true;
			Main.projFrames[projectile.type] = 4;

			// 如果写这句话粒子效果也变成泰拉剑气的了
			// aiType = ProjectileID.TerraBeam;

			//projectile.aiStyle = 27;
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			// 造成的伤害越高，buff持续时间越长？
			target.AddBuff(mod.BuffType("<你的buff名字>"), damage);
		}
		public override void AI()
		{
			// 累加计时器
			projectile.frameCounter++;
			// 当计时器经过了5帧
			if(projectile.frameCounter % 5 == 0)
			{
				// 重置计时器
				projectile.frameCounter = 0;
				// 选择下一帧动画
				projectile.frame++;
				projectile.frame %= 4;
			}
			// 声明一个Vector2变量，X轴大小为5.0f，Y轴大小为3.0f
			Vector2 vector = new Vector2(5.0f, 3.0f);
			// 声明一个Vector2变量，X轴大小为5.0f，Y轴大小为3.0f
			Vector2 vector1 = new Vector2(-5.0f, -3.0f);
			// 打印出这些二维向量操作的结果

			projectile.velocity *= 0.95f;
			if (projectile.timeLeft < 597)
			{
				//// 火焰粒子特效
				//Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height
				//	, MyDustId.Fire, 0f, 0f, 100, default(Color), 3f);
				//dust.noGravity = true;
			}
			if(projectile.timeLeft < 3)
			{
				Explode();
			}
		}

		private void Explode()
		{
			// 只是个标记，防止多次爆炸
			if (projectile.tileCollide)
			{
				projectile.tileCollide = false;
				projectile.position = projectile.Center;
				projectile.width = projectile.height = 128;
				projectile.Center = projectile.position;
				projectile.timeLeft = 3;
			}
		}
 

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Explode();
		}
		public override void Kill(int timeLeft)
        {

			Main.PlaySound(SoundID.Item14, projectile.position);
			Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
			for (int i = 0; i < 30; i++)
			{
				Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
					MyDustId.PinkBrightBubble, 0, 0, 100, Color.White, 1.5f);
				d.noGravity = true;
				d.velocity *= 2;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
	}
}
