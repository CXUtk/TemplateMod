/*
 * 模板MOD 
 * TemplateMod.cs
 * 作者：DXTsT
 * 
 * 允许编辑修改
 */

using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using System.Text;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TemplateMod.UICollection;
using TemplateMod.UI;
using TemplateMod.VecMap;
using TemplateMod.Sky;

// 用两个斜杠开头的句子都是注释QAQ，对程序运行没有任何影响，读我就行了，不用删

// 以上都是需要使用的程序集，不要乱动，不然代码会出问题

// 引入命名空间，这里就是MOD的名字TemplateMod
namespace TemplateMod
{
	// MOD的主类名字，需要与文件名、MOD名完全一致，并且继承Mod类
	public class TemplateMod : Mod
	{
		public static bool ShowDustTestUI = false;

		public static TemplateMod Instance;

		public static ModHotKey DustTestKey;

		public static Dictionary<string, Effect> MODEffectTable;

		// public static EffectManager _effectManager;

		// public static EffectManager2 _twistEffectManager;

		public static Vector2 TwistedPos;

		public static float TwistedStrength;

		private static List<ConditionalInterface> _userInterfaces;




		public TemplateMod()
		{
			// MOD的初始化函数，用来设置一些属性
			// 注意，这跟Load() 函数不一样
			Properties = new ModProperties()
			{
				// 自动加载贴图什么的
				Autoload = true,

				// 自动加载血块贴图
				AutoloadGores = true,

				// 自动加载声音文件
				AutoloadSounds = true,

				// 自动加载背景图片
				AutoloadBackgrounds = true
			};

			// 以上这些了解一下就行了，每个mod都要有这个
		}

		public override void Load()
		{
			Instance = this;
			if (!Main.dedServ)
			{
				DustTestKey = RegisterHotKey("呼出Dust测试界面", "K");
				_userInterfaces = new List<ConditionalInterface>();
				AddUI(new DustTestUI());
				MODEffectTable = new Dictionary<string, Effect>();
				MODEffectTable["Comic"] = GetEffect("Effects/Comic");
				MODEffectTable["Comic2"] = GetEffect("Effects/comic2");
				MODEffectTable["Swirl"] = GetEffect("Effects/Swirl");
				MODEffectTable["Disorder"] = GetEffect("Effects/Disorder");
				MODEffectTable["Bloom"] = GetEffect("Effects/Bloom");
				MODEffectTable["DisortScreen"] = GetEffect("Effects/DisortScreen");
				MODEffectTable["Color"] = GetEffect("Effects/Color");
				MODEffectTable["Edge"] = GetEffect("Effects/Edge");
				//MODEffectTable["DisortScreen"] = GetEffect("Effects/DisortScreen");
				var effect = new Ref<Effect>(MODEffectTable["Disorder"]);
				var effect2 = new Ref<Effect>(MODEffectTable["Bloom"]);
				Filters.Scene["Template:UltraLight"] = new Filter(
					new TestScreenShaderData(effect, "Pass1"),
					EffectPriority.High);
				SkyManager.Instance["Template:UltraLight"] = new TestSky();

				Filters.Scene["Template:Disort"] = new Filter(
					new ScreenShaderData(effect2, "Pass1")/*.UseImage(GetTexture("Images/noise1"), 0, SamplerState.AnisotropicClamp)*/,
					EffectPriority.VeryHigh);
				SkyManager.Instance["Template:Disort"] = new DisortSky();


				LoadVec.LoadVectors();
				//_effectManager = new EffectManager();
				//_twistEffectManager = new EffectManager2("Disorder", 10);
				TwistedStrength = 0;
				Filters.Scene.OnPostDraw += Scene_OnPostDraw;
				Main.OnPostDraw += Main_OnPostDraw;
			}
		}

		public override void Unload()
		{
			Instance = null;
			Main.OnPostDraw -= Main_OnPostDraw;
		}

		private void Main_OnPostDraw(GameTime obj)
		{
		}

		private void Scene_OnPostDraw()
		{
			// DrawSuperEffectString(Main.spriteBatch, "测试字体效果", new Vector2(100, 100));
			//if (_twistEffectManager.CanDraw)
			//{
			//	RenderTarget2D renderTarget2D = Main.screenTarget;
			//	GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			//	for (int i = 0; i < _twistEffectManager.Length; i++)
			//	{
			//		if (!_twistEffectManager[i].IsDead)
			//		{
			//			RenderTarget2D renderTarget = renderTarget2D != Main.screenTarget ? Main.screenTarget : Main.screenTargetSwap;
			//			graphicsDevice.SetRenderTarget(renderTarget);
			//			graphicsDevice.Clear(Color.Black);
			//			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			//			_twistEffectManager[i].Effect.Parameters["uIntensity"].SetValue(TemplateMod.TwistedStrength);

			//			_twistEffectManager[i].Effect.Parameters["uLengthSq"].SetValue(0.03f);
			//			_twistEffectManager[i].Effect.Parameters["uEffectPos"].SetValue(_twistEffectManager[i].Rect.Center() - Main.screenPosition);
			//			_twistEffectManager[i].Effect.CurrentTechnique.Passes["Pass1"].Apply();
			//			Main.spriteBatch.Draw((Texture2D)renderTarget2D, Vector2.Zero, Main.bgColor);
			//			Main.spriteBatch.End();
			//			renderTarget2D = renderTarget2D != Main.screenTarget ? Main.screenTarget : Main.screenTargetSwap;
			//		}
			//	}
			//	graphicsDevice.SetRenderTarget((RenderTarget2D)null);
			//	graphicsDevice.Clear(Color.Black);
			//	if ((double)Main.player[Main.myPlayer].gravDir == -1.0)
			//		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.EffectMatrix);
			//	else
			//		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

			//	Main.spriteBatch.Draw((Texture2D)renderTarget2D, Vector2.Zero, Color.White);
			//	Main.spriteBatch.End();
			//	for (int index = 0; index < 8; ++index)
			//		graphicsDevice.Textures[index] = (Texture)null;
			//}
		}
		

		private static void AddUI(UIState state)
		{
			ConditionalInterface tmpUI = new ConditionalInterface(() => ShowDustTestUI);
			tmpUI.SetState(state);
			_userInterfaces.Add(tmpUI);
		}

		public override void PostDrawInterface(SpriteBatch spriteBatch)
		{
			foreach (var ui in _userInterfaces)
			{
				if (ui.CanShow())
				{
					ui.Draw(spriteBatch, Main._drawInterfaceGameTime);
				}
			}
			//if (_effectManager.CanDraw)
			//{
			//	_effectManager.Draw(spriteBatch);
			//}

			//Main.NewText(Main.instance.GraphicsDevice.Textures[0].ToString());
			
			// Main.NewText(Main.screenTarget.ToString());


		}
		//private void DrawSuperEffectString(SpriteBatch sb, string str, Vector2 position)
		//{
		//	try
		//	{
		//		Vector2 size = Main.fontMouseText.MeasureString(str);
		//		GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
		//		RenderTarget2D renderTarget = new RenderTarget2D(graphicsDevice, (int)(size.X + 5), (int)(size.Y + 5));
		//		Texture2D backbuffer = new Texture2D(graphicsDevice, Main.screenWidth, Main.screenHeight);
		//		//backbuffer.GetData(buffer);
		//		graphicsDevice.SetRenderTarget(renderTarget);
		//		graphicsDevice.Clear(Color.Transparent);

		//		sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
		//		DynamicSpriteFontExtensionMethods.DrawString(sb, Main.fontMouseText, str, new Vector2(0, 0),
		//					Color.White);
		//		sb.End();

		//		graphicsDevice.SetRenderTarget(null);
		//		sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
		//		sb.Draw(Main.screenTarget, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
		//		sb.End();
		//		sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
		//		TemplateMod.MODEffectTable["Color"].CurrentTechnique.Passes["Pass1"].Apply();
		//		sb.Draw((Texture2D)renderTarget, position, Color.White);
		//		sb.End();
		//	}
		//	catch (Exception ex)
		//	{
		//		Main.NewText(ex);
		//	}
		//}

		//public override void UpdateUI(GameTime gameTime)
		//{
		//	foreach (var ui in _userInterfaces)
		//	{
		//		if (ui.CanShow())
		//		{
		//			ui.Update(gameTime);
		//		}
		//	}
		//}

	}

}
