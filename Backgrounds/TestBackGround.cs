using Terraria;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TemplateMod.Backgrounds
{
	public class TestBackGround : ModSurfaceBgStyle
	{
		public override bool ChooseBgStyle()
		{

			return !Main.gameMenu && false;
		}

		// Use this to keep far Backgrounds like the mountains.
		public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}

		public override int ChooseFarTexture()
		{
			return mod.GetBackgroundSlot("Backgrounds/ExampleBiomeSurfaceMid0");
		}

		public override int ChooseMiddleTexture()
		{
			return mod.GetBackgroundSlot("Backgrounds/ExampleBiomeSurfaceMid0");
		}
		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
			return mod.GetBackgroundSlot("Backgrounds/ExampleBiomeSurfaceMid0");
		}

		public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black);
			return false;
		}
	}
}