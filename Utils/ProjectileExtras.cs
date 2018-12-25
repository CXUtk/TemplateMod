using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace TemplateMod
{//Extract from Source code
	public static class ProjectileExtras
	{
		public delegate void ExtraAction();
		public static void YoyoAi(int index, float seconds, float length, float acceleration = 14f, float rotationSpeed = 0.45f, ExtraAction action = null, ExtraAction initialize = null)
		{
			Projectile projectile = Main.projectile[index];
			bool haveProj = false;
			if (initialize != null && projectile.localAI[1] == 0f)
			{
				projectile.localAI[1] = 1f;
				initialize();
			}
			for (int i = 0; i < projectile.whoAmI; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == projectile.owner && Main.projectile[i].type == projectile.type)
				{
					haveProj = true;
				}
			}
			if (projectile.owner == Main.myPlayer)
			{
				projectile.localAI[0] += 1f;
				if (haveProj)
				{
					projectile.localAI[0] += Main.rand.Next(10, 31) * 0.1f;
				}
				float time = projectile.localAI[0] / 60f;
				time /= (1f + Main.player[projectile.owner].meleeSpeed) / 2f;
				if (time > seconds)
				{
					projectile.ai[0] = -1f;
				}
			}
			bool flag2 = false;
			if (Main.player[projectile.owner].dead)
			{
				projectile.Kill();
				return;
			}
			if (!flag2 && !haveProj)
			{
				Main.player[projectile.owner].heldProj = projectile.whoAmI;
				Main.player[projectile.owner].itemAnimation = 2;
				Main.player[projectile.owner].itemTime = 2;
				if (projectile.position.X + projectile.width / 2 > Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2)
				{
					Main.player[projectile.owner].ChangeDir(1);
					projectile.direction = 1;
				}
				else
				{
					Main.player[projectile.owner].ChangeDir(-1);
					projectile.direction = -1;
				}
			}
			if (projectile.velocity.HasNaNs())
			{
				projectile.Kill();
			}
			projectile.timeLeft = 6;
			float long1 = length;
			if (Main.player[projectile.owner].yoyoString)
			{
				long1 = long1 * 1.25f + 30f;
			}
			long1 /= (1f + Main.player[projectile.owner].meleeSpeed * 3f) / 4f;
			float acc = acceleration / ((1f + Main.player[projectile.owner].meleeSpeed * 3f) / 4f);
			float num4 = 14f - acc / 2f;
			float num5 = 5f + acc / 2f;
			if (haveProj)
			{
				num5 += 20f;
			}
			if (projectile.ai[0] >= 0f)
			{
				if (projectile.velocity.Length() > acc)
				{
					projectile.velocity *= 0.98f;
				}
				bool reachMax = false;
				bool reachMax2 = false;
				Vector2 vector = Main.player[projectile.owner].Center - projectile.Center;
				if (vector.Length() > long1)
				{
					reachMax = true;
					if (vector.Length() > long1 * 1.3)
					{
						reachMax2 = true;
					}
				}
				if (projectile.owner == Main.myPlayer)
				{
					if (!Main.player[projectile.owner].channel || Main.player[projectile.owner].stoned || Main.player[projectile.owner].frozen)
					{
						projectile.ai[0] = -1f;
						projectile.ai[1] = 0f;
						projectile.netUpdate = true;
					}
					else
					{
						Vector2 vector2 = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
						float x = vector2.X;
						float y = vector2.Y;
						Vector2 vector3 = new Vector2(x, y) - Main.player[projectile.owner].Center;
						if (vector3.Length() > long1)
						{
							vector3.Normalize();
							vector3 *= long1;
							vector3 = Main.player[projectile.owner].Center + vector3;
							x = vector3.X;
							y = vector3.Y;
						}
						if (projectile.ai[0] != x || projectile.ai[1] != y)
						{
							Vector2 vector4 = new Vector2(x, y);
							Vector2 vector5 = vector4 - Main.player[projectile.owner].Center;
							if (vector5.Length() > long1 - 1f)
							{
								vector5.Normalize();
								vector5 *= long1 - 1f;
								vector4 = Main.player[projectile.owner].Center + vector5;
								x = vector4.X;
								y = vector4.Y;
							}
							projectile.ai[0] = x;
							projectile.ai[1] = y;
							projectile.netUpdate = true;
						}
					}
				}
				if (reachMax2 && projectile.owner == Main.myPlayer)
				{
					projectile.ai[0] = -1f;
					projectile.netUpdate = true;
				}
				if (projectile.ai[0] >= 0f)
				{
					if (reachMax)
					{
						num4 /= 2f;
						acc *= 2f;
						if (projectile.Center.X > Main.player[projectile.owner].Center.X && projectile.velocity.X > 0f)
						{
							projectile.velocity.X = projectile.velocity.X * 0.5f;
						}
						if (projectile.Center.Y > Main.player[projectile.owner].Center.Y && projectile.velocity.Y > 0f)
						{
							projectile.velocity.Y = projectile.velocity.Y * 0.5f;
						}
						if (projectile.Center.X < Main.player[projectile.owner].Center.X && projectile.velocity.X > 0f)
						{
							projectile.velocity.X = projectile.velocity.X * 0.5f;
						}
						if (projectile.Center.Y < Main.player[projectile.owner].Center.Y && projectile.velocity.Y > 0f)
						{
							projectile.velocity.Y = projectile.velocity.Y * 0.5f;
						}
					}
					Vector2 vector6 = new Vector2(projectile.ai[0], projectile.ai[1]);
					Vector2 difference = vector6 - projectile.Center;
					projectile.velocity.Length();
					if (difference.Length() > num5)
					{
						difference.Normalize();
						difference *= acc;
						projectile.velocity = (projectile.velocity * (num4 - 1f) + difference) / num4;
					}
					else if (haveProj)
					{
						if (projectile.velocity.Length() < acc * 0.6)
						{
							difference = projectile.velocity;
							difference.Normalize();
							difference *= acc * 0.6f;
							projectile.velocity = (projectile.velocity * (num4 - 1f) + difference) / num4;
						}
					}
					else
					{
						projectile.velocity *= 0.8f;
					}
					if (haveProj && !reachMax && projectile.velocity.Length() < acc * 0.6)
					{
						projectile.velocity.Normalize();
						projectile.velocity *= acc * 0.6f;
					}
					if (action != null)
					{
						action();
					}
				}
			}
			else
			{
				num4 = (int)(num4 * 0.8);
				acc *= 1.5f;
				projectile.tileCollide = false;
				Vector2 diff = Main.player[projectile.owner].position - projectile.Center;
				float len = diff.Length();
				if (len < acc + 10f || len == 0f)
				{
					projectile.Kill();
				}
				else
				{
					diff.Normalize();
					diff *= acc;
					projectile.velocity = (projectile.velocity * (num4 - 1f) + diff) / num4;
				}
			}
			projectile.rotation += rotationSpeed;
		}

		public static void SpearAi(int index, float protractSpeed = 1.5f, float retractSpeed = 1.4f, ExtraAction action = null, ExtraAction initialize = null)
		{
			Projectile projectile = Main.projectile[index];
			Player p = Main.player[projectile.owner];
			if (initialize != null && projectile.localAI[1] == 0f)
			{
				projectile.localAI[1] = 1f;
				initialize();
			}
			Vector2 playerCenter = p.RotatedRelativePoint(p.MountedCenter, true);
			projectile.direction = p.direction;
			p.heldProj = projectile.whoAmI;
			p.itemTime = Main.player[projectile.owner].itemAnimation;
			projectile.position = playerCenter - projectile.Size / 2;
			if (!p.frozen)
			{
				if (projectile.ai[0] == 0f)
				{
					projectile.ai[0] = 3f;
					projectile.netUpdate = true;
				}
				if (p.itemAnimation < p.itemAnimationMax / 3)
				{
					projectile.ai[0] -= retractSpeed;
				}
				else
				{
					projectile.ai[0] += protractSpeed;
				}
			}
			projectile.position += projectile.velocity * projectile.ai[0];
			if (p.itemAnimation == 0)
			{
				projectile.Kill();
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 2.355f;
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= 1.57f;
			}
			if (action != null)
			{
				action();
			}
		}

		public static void FlailAi(int index, float initialRange = 160f, float weaponOutRange = 300f, float retractRange = 100f, ExtraAction action = null, ExtraAction initialize = null)
		{
			Projectile projectile = Main.projectile[index];
			if (initialize != null && projectile.localAI[1] == 0f)
			{
				projectile.localAI[1] = 1f;
				initialize();
			}
			if (Main.player[projectile.owner].dead)
			{
				projectile.Kill();
				return;
			}
			Main.player[projectile.owner].itemAnimation = 10;
			Main.player[projectile.owner].itemTime = 10;
			if (projectile.position.X + projectile.width / 2 > Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2)
			{
				Main.player[projectile.owner].ChangeDir(1);
				projectile.direction = 1;
			}
			else
			{
				Main.player[projectile.owner].ChangeDir(-1);
				projectile.direction = -1;
			}
			Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
			float num = mountedCenter.X - vector.X;
			float num2 = mountedCenter.Y - vector.Y;
			float num3 = (float)Math.Sqrt(num * num + num2 * num2);
			if (projectile.ai[0] == 0f)
			{
				projectile.tileCollide = true;
				if (num3 > initialRange)
				{
					projectile.ai[0] = 1f;
					projectile.netUpdate = true;
				}
				else if (!Main.player[projectile.owner].channel)
				{
					if (projectile.velocity.Y < 0f)
					{
						projectile.velocity.Y = projectile.velocity.Y * 0.9f;
					}
					projectile.velocity.Y = projectile.velocity.Y + 1f;
					projectile.velocity.X = projectile.velocity.X * 0.9f;
				}
			}
			else if (projectile.ai[0] == 1f)
			{
				float num4 = 14f / Main.player[projectile.owner].meleeSpeed;
				float num5 = 0.9f / Main.player[projectile.owner].meleeSpeed;
				if (projectile.ai[1] == 1f)
				{
					projectile.tileCollide = false;
				}
				if (!Main.player[projectile.owner].channel || num3 > weaponOutRange || !projectile.tileCollide)
				{
					projectile.ai[1] = 1f;
					if (projectile.tileCollide)
					{
						projectile.netUpdate = true;
					}
					projectile.tileCollide = false;
					if (num3 < 20f)
					{
						projectile.Kill();
					}
				}
				if (!projectile.tileCollide)
				{
					num5 *= 2f;
				}
				int num6 = (int)retractRange;
				if (num3 > num6 || !projectile.tileCollide)
				{
					num3 = num4 / num3;
					num *= num3;
					num2 *= num3;
					float num7 = num - projectile.velocity.X;
					float num8 = num2 - projectile.velocity.Y;
					float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
					num9 = num5 / num9;
					num7 *= num9;
					num8 *= num9;
					projectile.velocity.X = projectile.velocity.X * 0.98f;
					projectile.velocity.Y = projectile.velocity.Y * 0.98f;
					projectile.velocity.X = projectile.velocity.X + num7;
					projectile.velocity.Y = projectile.velocity.Y + num8;
				}
				else
				{
					if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 6f)
					{
						projectile.velocity.X = projectile.velocity.X * 0.96f;
						projectile.velocity.Y = projectile.velocity.Y + 0.2f;
					}
					if (Main.player[projectile.owner].velocity.X == 0f)
					{
						projectile.velocity.X = projectile.velocity.X * 0.96f;
					}
				}
			}
			if (projectile.velocity.X < 0f)
			{
				projectile.rotation -= (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
			}
			else
			{
				projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
			}
			if (action != null)
			{
				action();
			}
		}

		public static bool FlailTileCollide(int index, Vector2 oldVelocity)
		{
			Projectile projectile = Main.projectile[index];
			bool flag = false;
			if (oldVelocity.X != projectile.velocity.X)
			{
				if (Math.Abs(oldVelocity.X) > 4f)
				{
					flag = true;
				}
				projectile.velocity.X = -oldVelocity.X * 0.2f;
			}
			if (oldVelocity.Y != projectile.velocity.Y)
			{
				if (Math.Abs(oldVelocity.Y) > 4f)
				{
					flag = true;
				}
				projectile.velocity.Y = -oldVelocity.Y * 0.2f;
			}
			projectile.ai[0] = 1f;
			if (flag)
			{
				projectile.netUpdate = true;
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
			}
			return false;
		}

		public static void BoomerangAi(int index, float retractTime = 30f, float speed = 9f, float speedAcceleration = 0.4f, ExtraAction action = null, ExtraAction initialize = null)
		{
			Projectile projectile = Main.projectile[index];
			if (initialize != null && projectile.localAI[1] == 0f)
			{
				projectile.localAI[1] = 1f;
				initialize();
			}
			if (projectile.soundDelay == 0)
			{
				projectile.soundDelay = 8;
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 7);
			}
			if (projectile.ai[0] == 0f)
			{
				projectile.ai[1] += 1f;
				if (projectile.ai[1] >= retractTime)
				{
					projectile.ai[0] = 1f;
					projectile.ai[1] = 0f;
					projectile.netUpdate = true;
				}
			}
			else
			{
				projectile.tileCollide = false;
				Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
				float num = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - vector.X;
				float num2 = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - vector.Y;
				float num3 = (float)Math.Sqrt(num * num + num2 * num2);
				if (num3 > 3000f)
				{
					projectile.Kill();
				}
				num3 = speed / num3;
				num *= num3;
				num2 *= num3;
				if (projectile.velocity.X < num)
				{
					projectile.velocity.X = projectile.velocity.X + speedAcceleration;
					if (projectile.velocity.X < 0f && num > 0f)
					{
						projectile.velocity.X = projectile.velocity.X + speedAcceleration;
					}
				}
				else if (projectile.velocity.X > num)
				{
					projectile.velocity.X = projectile.velocity.X - speedAcceleration;
					if (projectile.velocity.X > 0f && num < 0f)
					{
						projectile.velocity.X = projectile.velocity.X - speedAcceleration;
					}
				}
				if (projectile.velocity.Y < num2)
				{
					projectile.velocity.Y = projectile.velocity.Y + speedAcceleration;
					if (projectile.velocity.Y < 0f && num2 > 0f)
					{
						projectile.velocity.Y = projectile.velocity.Y + speedAcceleration;
					}
				}
				else if (projectile.velocity.Y > num2)
				{
					projectile.velocity.Y = projectile.velocity.Y - speedAcceleration;
					if (projectile.velocity.Y > 0f && num2 < 0f)
					{
						projectile.velocity.Y = projectile.velocity.Y - speedAcceleration;
					}
				}
				if (Main.myPlayer == projectile.owner)
				{
					Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
					Rectangle rectangle2 = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
					if (rectangle.Intersects(rectangle2))
					{
						projectile.Kill();
					}
				}
			}
			projectile.rotation += 0.4f * projectile.direction;
			if (action != null)
			{
				action();
			}
		}

		public static bool BoomerangTileCollide(int index, Vector2 oldVelocity)
		{
			Projectile projectile = Main.projectile[index];
			Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
			projectile.ai[0] = 1f;
			projectile.velocity.X = -oldVelocity.X;
			projectile.velocity.Y = -oldVelocity.Y;
			projectile.netUpdate = true;
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
			return false;
		}

		public static void BoomerangOnHitEntity(int index)
		{
			Projectile projectile = Main.projectile[index];
			if (projectile.ai[0] == 0f)
			{
				projectile.velocity.X = -projectile.velocity.X;
				projectile.velocity.Y = -projectile.velocity.Y;
				projectile.netUpdate = true;
			}
			projectile.ai[0] = 1f;
		}

		public static void ThrowingKnifeAi(int index, int airTime = 20, ExtraAction action = null, ExtraAction initialize = null)
		{
			Projectile projectile = Main.projectile[index];
			if (initialize != null && projectile.localAI[1] == 0f)
			{
				projectile.localAI[1] = 1f;
				initialize();
			}
			projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.03f * projectile.direction;
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] >= airTime)
			{
				projectile.velocity.Y = projectile.velocity.Y + 0.4f;
				projectile.velocity.X = projectile.velocity.X * 0.98f;
			}
			else
			{
				projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			}
			if (projectile.velocity.Y > 16f)
			{
				projectile.velocity.Y = 16f;
			}
			if (action != null)
			{
				action();
			}
		}

		public static void Explode(int index, int sizeX, int sizeY, ExtraAction visualAction = null)
		{
			Projectile projectile = Main.projectile[index];
			if (!projectile.active)
			{
				return;
			}
			projectile.tileCollide = false;
			projectile.alpha = 255;
			projectile.position.X = projectile.position.X + (projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (projectile.height / 2);
			projectile.width = sizeX;
			projectile.height = sizeY;
			projectile.position.X = projectile.position.X - (projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (projectile.height / 2);
			projectile.Damage();
			Main.projectileIdentity[projectile.owner, projectile.identity] = -1;
			projectile.position.X = projectile.position.X + projectile.width / 2;
			projectile.position.Y = projectile.position.Y + projectile.height / 2;
			projectile.width = (int)(sizeX / 5.8f);
			projectile.height = (int)(sizeY / 5.8f);
			projectile.position.X = projectile.position.X - (projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (projectile.height / 2);
			if (visualAction == null)
			{
				for (int i = 0; i < 30; i++)
				{
					int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.4f;
				}
				for (int j = 0; j < 20; j++)
				{
					int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].velocity *= 7f;
					num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num2].velocity *= 3f;
				}
				for (int k = 0; k < 2; k++)
				{
					float num3 = 0.4f;
					if (k == 1)
					{
						num3 = 0.8f;
					}
					int num4 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
					Gore gore = Main.gore[num4];
					gore.velocity *= num3;
					gore.velocity.X = gore.velocity.X + 1f;
					gore.velocity.Y = gore.velocity.Y + 1f;
					num4 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
					gore = Main.gore[num4];
					gore.velocity *= num3;
					gore.velocity.X = gore.velocity.X - 1f;
					gore.velocity.Y = gore.velocity.Y + 1f;
					num4 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
					gore = Main.gore[num4];
					gore.velocity *= num3;
					gore.velocity.X = gore.velocity.X + 1f;
					gore.velocity.Y = gore.velocity.Y - 1f;
					num4 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
					gore = Main.gore[num4];
					gore.velocity.X = gore.velocity.X - 1f;
					gore.velocity.Y = gore.velocity.Y - 1f;
				}
				return;
			}
			visualAction();
		}
		public static void RecordOldPos(Projectile proj)
		{
			for (int i = proj.oldPos.Length - 1; i > 0; i--)
			{
				proj.oldPos[i] = proj.oldPos[i - 1];
			}
			proj.oldPos[0] = proj.position;
		}

		public static void DrawFishString(int index, Vector2 to = default(Vector2))
		{
			Projectile projectile = Main.projectile[index];
			Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 vector = mountedCenter;
			vector.Y += Main.player[projectile.owner].gfxOffY;
			if (to != default(Vector2))
			{
				vector = to;
			}
			float num = projectile.Center.X - vector.X;
			float num2 = projectile.Center.Y - vector.Y;
			float num3 = (float)Math.Atan2(num2, num) - 1.57f;
			if (!projectile.counterweight)
			{
				int num4 = -1;
				if (projectile.position.X + projectile.width / 2 < Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2)
				{
					num4 = 1;
				}
				num4 *= -1;
				Main.player[projectile.owner].itemRotation = (float)Math.Atan2(num2 * num4, num * num4);
			}
			bool flag = true;
			if (num == 0f && num2 == 0f)
			{
				flag = false;
			}
			else
			{
				float num5 = (float)Math.Sqrt(num * num + num2 * num2);
				num5 = 12f / num5;
				num *= num5;
				num2 *= num5;
				vector.X -= num * 0.1f;
				vector.Y -= num2 * 0.1f;
				num = projectile.position.X + projectile.width * 0.5f - vector.X;
				num2 = projectile.position.Y + projectile.height * 0.5f - vector.Y;
			}
			while (flag)
			{
				float num6 = 12f;
				float num7 = (float)Math.Sqrt(num * num + num2 * num2);
				float num8 = num7;
				if (float.IsNaN(num7) || float.IsNaN(num8))
				{
					flag = false;
				}
				else
				{
					if (num7 < 20f)
					{
						num6 = num7 - 8f;
						flag = false;
					}
					num7 = 12f / num7;
					num *= num7;
					num2 *= num7;
					vector.X += num;
					vector.Y += num2;
					num = projectile.position.X + projectile.width * 0.5f - vector.X;
					num2 = projectile.position.Y + projectile.height * 0.1f - vector.Y;
					if (num8 > 12f)
					{
						float num9 = 0.3f;
						float num10 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
						if (num10 > 16f)
						{
							num10 = 16f;
						}
						num10 = 1f - num10 / 16f;
						num9 *= num10;
						num10 = num8 / 80f;
						if (num10 > 1f)
						{
							num10 = 1f;
						}
						num9 *= num10;
						if (num9 < 0f)
						{
							num9 = 0f;
						}
						num9 *= num10;
						num9 *= 0.5f;
						if (num2 > 0f)
						{
							num2 *= 1f + num9;
							num *= 1f - num9;
						}
						else
						{
							num10 = Math.Abs(projectile.velocity.X) / 3f;
							if (num10 > 1f)
							{
								num10 = 1f;
							}
							num10 -= 0.5f;
							num9 *= num10;
							if (num9 > 0f)
							{
								num9 *= 2f;
							}
							num2 *= 1f + num9;
							num *= 1f - num9;
						}
					}
					num3 = (float)Math.Atan2(num2, num) - 1.57f;
					int stringColor = Main.player[projectile.owner].stringColor;
					Color color = WorldGen.paintColor(stringColor);
					if (color.R < 75)
					{
						color.R = 75;
					}
					if (color.G < 75)
					{
						color.G = (75);
					}
					if (color.B < 75)
					{
						color.B = (75);
					}
					if (stringColor == 13)
					{
						color = new Color(20, 20, 20);
					}
					else if (stringColor == 14 || stringColor == 0)
					{
						color = new Color(200, 200, 200);
					}
					else if (stringColor == 28)
					{
						color = new Color(163, 116, 91);
					}
					else if (stringColor == 27)
					{
						color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
					}
					color.A = ((byte)(color.A * 0.4f));
					float num11 = 0.5f;
					color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f), color);
					color = new Color((byte)(color.R * num11), (byte)(color.G * num11), (byte)(color.B * num11), (byte)(color.A * num11));
					Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(vector.X - Main.screenPosition.X + Main.fishingLineTexture.Width * 0.5f, vector.Y - Main.screenPosition.Y + Main.fishingLineTexture.Height * 0.5f) - new Vector2(6f, 0f), new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num6), color, num3, new Vector2(Main.fishingLineTexture.Width * 0.5f, 0f), 1f, 0, 0f);
				}
			}
		}

		public static void DrawChain(int index, Vector2 to, string chainPath)
		{
			Texture2D texture = ModLoader.GetTexture(chainPath);
			Projectile projectile = Main.projectile[index];
			Vector2 vector = projectile.Center;
			Vector2 vector2 = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
			float num = texture.Height;
			Vector2 vector3 = to - vector;
			float num2 = (float)Math.Atan2(vector3.Y, vector3.X) - 1.57f;
			var flag = !(float.IsNaN(vector.X) && float.IsNaN(vector.Y));
			if (float.IsNaN(vector3.X) && float.IsNaN(vector3.Y))
			{
				flag = false;
			}
			while (flag)
			{
				if (vector3.Length() < num + 1.0)
				{
					flag = false;
				}
				else
				{
					Vector2 vector4 = vector3;
					vector4.Normalize();
					vector += vector4 * num;
					vector3 = to - vector;
					Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16.0));
					color = projectile.GetAlpha(color);
					Main.spriteBatch.Draw(texture, vector - Main.screenPosition, null, color, num2, vector2, 1f, 0, 0f);
				}
			}
		}

		public static void DrawAroundOrigin(int index, Color lightColor, SpriteEffects effect = SpriteEffects.None)
		{
			Projectile projectile = Main.projectile[index];
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 vector = new Vector2(texture2D.Width * 0.5f, texture2D.Height / Main.projFrames[projectile.type] * 0.5f);
			Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition, new Rectangle?(texture2D.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame)), lightColor, projectile.rotation, vector, projectile.scale, effect, 0f);
		}

		public static void DrawAroundOrigin(Vector2 pos, Projectile proj, Color lightColor, SpriteEffects effect = SpriteEffects.None)
		{
			Projectile projectile = proj;
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 vector = new Vector2(texture2D.Width * 0.5f, texture2D.Height / Main.projFrames[projectile.type] * 0.5f);
			Main.spriteBatch.Draw(texture2D, pos - Main.screenPosition, new Rectangle?(texture2D.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame)), lightColor, projectile.rotation, vector, projectile.scale, effect, 0f);
		}

		public static void DrawAroundOrigin(Vector2 pos, Projectile proj, Color lightColor)
		{
			Projectile projectile = proj;
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Vector2 vector = new Vector2(texture2D.Width * 0.5f, texture2D.Height / Main.projFrames[projectile.type] * 0.5f);
			SpriteEffects spriteEffects = (projectile.direction == -1) ? SpriteEffects.FlipHorizontally : 0;
			Main.spriteBatch.Draw(texture2D, pos - Main.screenPosition, new Rectangle?(texture2D.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame)), lightColor, projectile.rotation, vector, projectile.scale, spriteEffects, 0f);
		}

		public static void DrawSpear(int index, Color lightColor)
		{
			Projectile projectile = Main.projectile[index];
			Vector2 zero = Vector2.Zero;
			SpriteEffects spriteEffects = 0;
			if (projectile.spriteDirection == -1)
			{
				zero.X = Main.projectileTexture[projectile.type].Width;
				spriteEffects = (SpriteEffects)1;
			}
			Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + projectile.width / 2, projectile.position.Y - Main.screenPosition.Y + projectile.height / 2 + projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), projectile.GetAlpha(lightColor), projectile.rotation, zero, projectile.scale, spriteEffects, 0f);
		}
		public static void UltraDraw(SpriteBatch sb, Projectile projectile, Vector2 origin, Color lightColor, SpriteEffects effect, Rectangle? source = null)
		{

			sb.Draw(Main.projectileTexture[projectile.type],
				new Vector2(projectile.position.X - Main.screenPosition.X + projectile.width / 2,
					projectile.position.Y - Main.screenPosition.Y + projectile.height / 2),
				source,
				lightColor,
				projectile.rotation,
				origin,
				projectile.scale,
				effect,
				0f);
		}
		public static void UltraDraw(SpriteBatch sb, Projectile proj, Vector2 pos, Vector2 size, Vector2 origin, Color lightColor, Rectangle? source = null)
		{

			sb.Draw(Main.projectileTexture[proj.type],
				new Vector2(pos.X - Main.screenPosition.X + size.X / 2,
					pos.Y - Main.screenPosition.Y + size.Y / 2),
				source,
				lightColor,
				proj.rotation,
				origin,
				proj.scale,
				0,
				0f);
		}
		public static void UltraDraw(SpriteBatch sb, Projectile proj, Vector2 pos, Color color, float scale = 1f)
		{

			sb.Draw(Main.projectileTexture[proj.type],
				new Vector2(pos.X - Main.screenPosition.X + proj.Size.X / 2,
					pos.Y - Main.screenPosition.Y + proj.Size.Y / 2),
				null,
				color,
				proj.rotation,
				new Vector2(Main.projectileTexture[proj.type].Width / 2, Main.projectileTexture[proj.type].Height / 2),
				scale,
				0,
				0f);
		}


		public static Vector2 WanderingVec(Entity entity, ref Vector2 wanderTarget,
			float radius = 60f, float randScale = 5f, float distanceHead = 100f, float incVelocity = 1f)
		{
			wanderTarget += new Vector2(Main.rand.NextFloatDirection() * randScale, Main.rand.NextFloatDirection() * randScale);
			wanderTarget.Normalize();
			wanderTarget *= radius;
			Vector2 target = wanderTarget + Vector2.Normalize(entity.velocity) * distanceHead;
			return Vector2.Normalize(target + entity.velocity * 2) * incVelocity;
		}

		public static Vector2 WanderingVec(Entity entity,
	float radius = 60f, float randScale = 5f, float distanceHead = 100f, float incVelocity = 1f)
		{
			Vector2 wanderTarget = new Vector2(Main.rand.NextFloatDirection() * randScale, Main.rand.NextFloatDirection() * randScale);
			wanderTarget.Normalize();
			wanderTarget *= radius;
			Vector2 target = wanderTarget + Vector2.Normalize(entity.velocity) * distanceHead;
			return Vector2.Normalize(target) * incVelocity;
		}

		public static Vector2 ArriveVec(Entity entity, Vector2 target, float maxSpeed, float deceleration = 0.9f)
		{
			Vector2 toTarget = target - entity.Center;
			float distance = toTarget.Length();
			float speed = Math.Min(distance / deceleration, maxSpeed);
			Vector2 resVec = toTarget * speed / distance;
			return resVec - entity.velocity;
		}
	}
}
