using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace TemplateMod.NPCs
{
	public class TestNPC : FSM_NPC
	{
		enum NPCState
		{
			Initialize,
			// 收缩
			ReadyShrink,
			// 正常状态
			ReadyNormal,
			// 跳跃状态
			Jump,
			Boredom,
			Attack,
		}

		private bool _isFriendly = true;

		public int Timer2
		{
			get { return (int)npc.ai[2]; }
			set { npc.ai[2] = value; }
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("测试史莱姆");
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 26;
			npc.damage = 20;
			npc.defense = 8;
			npc.lifeMax = 68;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.aiStyle = -1;
			npc.value = 100;
			Main.npcFrameCount[npc.type] = 2;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0.5f;
		}

		public override void FindFrame(int frameHeight)
		{
			if (State == (int)NPCState.ReadyShrink || State == (int)NPCState.Jump)
			{
				npc.frame.Y = frameHeight;
			}
			else
			{
				npc.frame.Y = 0;
			}
		}
		public override void AI()
		{
			// 此处可以写全局状态，也就是不依赖于某一种状态的代码
			// 当npc变为敌对状态的时候，寻找最近的玩家作为目标
			if (!_isFriendly)
			{
				npc.TargetClosest();
				Player player = Main.player[npc.target];
				npc.ai[2] = player.Center.X > npc.Center.X ? 1 : -1;
			}

			if(Math.Abs(npc.velocity.Y) > 0.1f)
			{
				SwitchState((int)NPCState.Jump);
			}

			// 对于NPC处于的每种状态，都提供一个独立的处理方式
			switch ((NPCState)State)
			{
				// 初始化随机决定史莱姆的跳跃方向
				case NPCState.Initialize:
					{
						npc.direction = Main.rand.NextBool() ? -1 : 1;
						SwitchState((int)NPCState.ReadyNormal);
						break;
					}
				case NPCState.ReadyNormal:
					{

						if (npc.velocity.Y == 0 && npc.collideY)
						{
							npc.velocity.X *= 0.9f;
						}
						Timer++;
						int threashold = _isFriendly ? 300 : 165;
						if(Timer > threashold)
						{
							Timer = 0;
							Vector2 vel = new Vector2();
							if (!_isFriendly && Main.player[npc.target].Distance(npc.Center) < 900f && getShootVectorBinary(out vel))
							{
								Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("GravPro"), 1, 0, 0);
							}
							else
							{
								vel.X = npc.direction * 5;
								vel.Y = -10;
							}
							npc.velocity.X = vel.X;
							npc.velocity.Y = vel.Y;
							SwitchState((int)NPCState.Jump);
							break;
						}
						if(Timer % 30 < 1)
						{
							SwitchState((int)NPCState.ReadyShrink);
						}
						break;
					}
				case NPCState.ReadyShrink:
					{
						Timer++;
						if(Timer % 30 == 15)
						{
							SwitchState((int)NPCState.ReadyNormal);
						}
						break;
					}
				case NPCState.Jump:
					{
						if (npc.collideY)
						{
							SwitchState((int)NPCState.ReadyNormal);
						}

						break;
					}
				case NPCState.Attack:
					{
						npc.direction = Main.player[npc.target].Center.X < npc.Center.X ? 1 : -1;//确定NPC攻击玩家的X方向
						npc.directionY = Main.player[npc.target].Center.Y < npc.Center.Y ? 1 : -1;//Y方向
						//move();
						Timer++;
						if(Timer > 600)
						{
							// 进入无聊状态
							SwitchState((int)NPCState.Boredom);
						}
						break;
					}
				case NPCState.Boredom:
					{
						NPC boss = Main.npc[(int)npc.ai[2]];

						if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							Timer = 0; //如果玩家处于可以攻击状态重置无聊计时器
							SwitchState((int)NPCState.Attack);
						}
						// 如果超过无聊时间就重新开始攻击
						if(Timer > 1200)
						{
							Timer = 0;
							SwitchState((int)NPCState.Attack);
						}
						break;
					}
			}
		}

		private bool getShootVectorBinary(out Vector2 result)
		{
			Player player = Main.player[npc.target];
			int dir = player.Center.X < npc.Center.X ? -1 : 1;
			float l = -MathHelper.PiOver2;
			float r = -MathHelper.PiOver4;
			float shootspeed = 15f;

			// 高度是否足够
			bool heightenough = false;
			// 水平距离是否能够达到
			bool distanceenough = false;
			while(Math.Abs(l - r) > 0.01f)
			{
				float mid = (l + r) * 0.5f;
				float real = new Vector2(dir, 0).ToRotation() + mid * dir;
				Vector2 unit = real.ToRotationVector2() * shootspeed;
				Vector2 pos = npc.Center;
				float beyond = 0f;
				float maxHeight = 99999f;
				// 这段普通计算机在100000次模拟以下都不会有任何卡顿
				for(int i = 0; i < 1000; i++)
				{
					pos += unit;
					unit.Y += 0.32f;
					maxHeight = Math.Min(pos.Y, maxHeight);
					if (unit.Y > 1f && pos.Y > player.Center.Y)
					{
						beyond = pos.X - player.Center.X;
						break;
					}
				}
				if(maxHeight > player.Center.Y)
				{
					r = mid;
					continue;
				}
				else
				{
					heightenough = true;
				}
				beyond *= dir;
				if(beyond > 0f)
				{
					distanceenough = true;
					r = mid;
				}
				else
				{
					l = mid;
				}
			}
			result = (new Vector2(dir, 0).ToRotation() + l * dir).ToRotationVector2() * shootspeed;
			return heightenough && distanceenough;
		}

		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
		{
			_isFriendly = false;
			base.OnHitByItem(player, item, damage, knockback, crit);
		}
		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			_isFriendly = false;
			base.OnHitByProjectile(projectile, damage, knockback, crit);
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life > 0)
			{
				int num332 = 0;
				while (num332 < damage / npc.lifeMax * 30.0)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
					num332++;
				}
				return;
			}
			for (int num333 = 0; num333 < 15; num333++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 5, 2 * hitDirection, -2f, 0, default(Color), 1f);
			}
		}
	}
}
