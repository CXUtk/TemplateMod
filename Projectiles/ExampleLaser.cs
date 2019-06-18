using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

// 从ExampleMod上搬下来的，也是我写的

namespace TemplateMod.Projectiles
{
	// The following laser shows a channeled ability, after charging up the laser will be fired
	// Using custom drawing, dust effects, and custom collision checks for tiles
	public class ExampleLaser : ModProjectile
	{
		// The maximum charge value
		private const float MaxChargeValue = 50f;
		//The distance charge particle from the player center
		private const float MoveDistance = 60f;

		private Vector2 reflectVelocity;

		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}


		public float Distance2
		{
			get { return projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}

		// The actual charge value is stored in the localAI0 field
		public float Charge
		{
			get { return projectile.localAI[0]; }
			set { projectile.localAI[0] = value; }
		}

		// Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
		public bool AtMaxCharge { get { return Charge == MaxChargeValue; } }

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.magic = true;
			projectile.hide = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//// 最大距离
			//float maxDistance = 1000f;
			//// 间隔
			//float step = 1f;
			//Vector2 unit = projectile.velocity;
			//unit.Normalize();
			//// 方块的旋转弧度
			//float r = unit.ToRotation() - 1.57f;
			//Vector2 targetPos = projectile.Center + unit;
			//for (float i = 1; i < Distance; i += step)
			//{
			//	float r2 = ((targetPos - projectile.Center).ToRotation() + 1.57f);

			//	// 高度为30的激光贴图小块
			//	spriteBatch.Draw(Main.projectileTexture[projectile.type], targetPos - Main.screenPosition, null,
			//		Color.White, r + r2, Main.projectileTexture[projectile.type].Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			//	targetPos += Vector2.Normalize((unit + r2.ToRotationVector2() * 4));
			//}
			// We start drawing the laser if we have charged up
			if (AtMaxCharge)
			{
				DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], Main.player[projectile.owner].Center,
					projectile.velocity, 18, projectile.damage, -1.57f, 1f, 2000f, Color.White, (int)MoveDistance);
			}
			return false;
		}

		// The core function of drawing a laser
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{

			Vector2 origin = start;
			float r = unit.ToRotation() + rotation;

			#region Draw laser body
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = Color.White;
				origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					null, i < transDist ? Color.Transparent : c, r,
					texture.Size() * 0.5f, scale, 0, 0);
			}
			if(Distance2 > 1)
			{
				r = reflectVelocity.ToRotation() + rotation;
				for (float i = 0; i <= Distance2; i += step)
				{
					Color c = Color.White;
					origin = start + Distance * unit + i * reflectVelocity;
					spriteBatch.Draw(texture, origin - Main.screenPosition,
						null, c, r,
						texture.Size() * 0.5f, scale, 0, 0);
				}
			}
			#endregion

			//#region Draw laser tail
			//spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
			//	new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			//#endregion

			//#region Draw laser head
			//spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
			//	new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			//#endregion
		}

		// Change the way of collision check of the projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Player player = Main.player[projectile.owner];
			Vector2 unit = projectile.velocity;
			float point = 0f;
			if(Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
				player.Center + unit * Distance, 18, ref point))
			{
				return true;
			}
			else if(Distance2 > 1)
			{
				return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center + unit * Distance,
				player.Center + unit * Distance + reflectVelocity * Distance2, 18, ref point);
			}
			return false;
		}

		// Set custom immunity time on hitting an NPC
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;
		}

		// The AI of the projectile
		public override void AI()
		{
			Vector2 mousePos = Main.MouseWorld;
			Player player = Main.player[projectile.owner];

			#region Set projectile position
			// Multiplayer support here, only run this code if the client running it is the owner of the projectile
			if (projectile.owner == Main.myPlayer)
			{
				Vector2 diff = mousePos - player.Center;
				diff.Normalize();
				projectile.velocity = diff;
				projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
				projectile.netUpdate = true;
			}
			projectile.position = player.Center + projectile.velocity * MoveDistance;
			projectile.timeLeft = 2;
			int dir = projectile.direction;
			player.ChangeDir(dir);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);
			#endregion
			#region Charging process
			// Kill the projectile if the player stops channeling
			if (!player.channel)
			{
				projectile.Kill();
			}
			else
			{
				// Do we still have enough mana? If not, we kill the projectile because we cannot use it anymore
				if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
				{
					projectile.Kill();
				}
				Vector2 offset = projectile.velocity;
				offset *= MoveDistance - 20;
				Vector2 pos = player.Center + offset - new Vector2(10, 10);
				if (Charge < MaxChargeValue)
				{
					Charge++;
				}
				int chargeFact = (int)(Charge / 20f);
				Vector2 dustVelocity = Vector2.UnitX * 18f;
				dustVelocity = dustVelocity.RotatedBy(projectile.rotation - 1.57f, default(Vector2));
				Vector2 spawnPos = projectile.Center + dustVelocity;
				for (int k = 0; k < chargeFact + 1; k++)
				{
					Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - (chargeFact * 2));
					Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, 226, projectile.velocity.X / 2f,
						projectile.velocity.Y / 2f, 0, default(Color), 1f)];
					dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
					dust.noGravity = true;
					dust.scale = Main.rand.Next(10, 20) * 0.05f;
				}
			}
			#endregion

			#region Set laser tail position and dusts
			if (Charge < MaxChargeValue) return;
			Vector2 unit = projectile.velocity;
			float[] samples = new float[2];
			Collision.LaserScan(projectile.Center, unit, 18f, 2000f, samples);
			float maxDis = (samples[0] + samples[1]) * 0.5f;
			Distance = maxDis;
			NPC hittednpc = null;
			float point = Distance;
			foreach (var npc in Main.npc)
			{
				if (npc.active && !npc.friendly && !npc.dontTakeDamage)
				{
					if (Collision.CheckAABBvLineCollision(npc.Hitbox.TopLeft(), npc.Hitbox.Size(), player.Center,
						player.Center + unit * Distance, 18, ref point))
					{
						if (Distance > point)
						{
							Distance = point;
							hittednpc = npc;
						}
					}
				}
			}
			if (hittednpc != null)
			{
				Vector2 normal = Vector2.Normalize(player.Center + unit * Distance - hittednpc.Center);
				reflectVelocity = Vector2.Reflect(projectile.velocity, normal);
				reflectVelocity.Normalize();
				float[] samples2 = new float[2];
				Collision.LaserScan(player.Center + unit * Distance, reflectVelocity, 18f, 2000f - Distance, samples2);
				float maxdist = (samples2[0] + samples2[1]) * 0.5f;
				Distance2 = maxdist;
			}
			else
			{
				Distance2 = 0;
			}



			Vector2 dustPos = player.Center + projectile.velocity * Distance;
			//Imported dust code from source because I'm lazy
			for (int i = 0; i < 2; ++i)
			{
				float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 226, dustVel.X, dustVel.Y, 0, new Color(), 1f)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				dust = Dust.NewDustDirect(Main.player[projectile.owner].Center, 0, 0, 31,
					-unit.X * Distance, -unit.Y * Distance);
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
				dust.color = Color.Cyan;
			}
			if (Main.rand.Next(5) == 0)
			{
				Vector2 offset = projectile.velocity.RotatedBy(1.57f, new Vector2()) * ((float)Main.rand.NextDouble() - 0.5f) *
								 projectile.width;
				Dust dust = Main.dust[
					Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity = dust.velocity * 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);

				unit = dustPos - Main.player[projectile.owner].Center;
				unit.Normalize();
				dust = Main.dust[
					Dust.NewDust(Main.player[projectile.owner].Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity = dust.velocity * 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			}
			#endregion

			//Add lights
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Terraria.Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MoveDistance), 26,
				DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = projectile.velocity;
			Terraria.Utils.PlotTileLine(projectile.Center, projectile.Center + unit * Distance, (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
		}
	}
}
