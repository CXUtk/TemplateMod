using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TemplateMod.Dusts
{
	public class Spark : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noLight = true;
			dust.color = new Color(200, 220, 230);
			dust.scale = 1.2f;
			dust.alpha = 100;
			dust.velocity *= 1.25f;
			dust.velocity = Vector2.Normalize(dust.velocity) * 5;
			dust.fadeIn = 0;
			dust.firstFrame = true;

		}


		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.velocity.Y += 0.06f;
			dust.velocity.X *= 0.95f;
			dust.rotation = dust.velocity.ToRotation() - MathHelper.PiOver4;
			Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 1f, 1f, 1f);
			dust.scale -= 0.01f;
			dust.alpha += 1;
			if(dust.alpha > 254 || dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}