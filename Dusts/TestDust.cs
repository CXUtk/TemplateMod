using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TemplateMod.Dusts
{
	public class TestDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noLight = true;
			dust.color = new Color(200, 220, 230);
			dust.scale = 1.2f;
			dust.noGravity = true;
			dust.velocity /= 2f;
			dust.alpha = 100;
			dust.fadeIn = 0;
		}

		public override bool Update(Dust dust)
		{
			dust.fadeIn++;
			if(dust.fadeIn < 5.0f)
			{
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.8f, 0.8f, 0.2f);
				dust.color = Color.Lerp(new Color(255, 255, 100), Color.Orange, (dust.fadeIn) / 5f);
				dust.scale += 0.01f;
			}
			else if(dust.fadeIn < 10)
			{
				Lighting.AddLight(dust.position, Vector3.Lerp(
					new Vector3(0.8f, 0.3f, 0.3f), new Vector3(0.1f, 0.1f, 0.1f), (10 - dust.fadeIn) / 5f));
				dust.color = Color.Lerp(Color.Orange, Color.Black, (dust.fadeIn - 5) / 5f);
				dust.scale += 0.1f;
			}
			else if(dust.fadeIn < 60)
			{
				dust.color = Color.Black;
				dust.scale += 0.03f;
			}
			else
			{
				dust.scale -= 0.03f;
				if (dust.scale < 0.5f)
				{
					dust.active = false;
				}
			}
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			//Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.05f, 0.15f, 0.2f);
			return false;
		}
	}
}