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
using TemplateMod.UI;
using TemplateMod.VecMap;
using TemplateMod.Sky;
using TemplateMod.Files;

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


		// public static EffectManager _effectManager;

		// public static EffectManager2 _twistEffectManager;

		public static Vector2 TwistedPos;

		public static float TwistedStrength;

		internal static Dictionary<string, Texture2D> ModTexturesTable;

		public GUIManager GUIManager { get; private set; }
		public static ToolBarServiceManager ToolBarServiceManager { get; internal set; }
		public static string ShowTooltip { get; internal set; }
		public static bool SelectMode { get; internal set; }
		public static bool BuildMode { get; internal set; }
		public static Point SelectUpperLeft { get; internal set; }
		public static Point SelectLowerRight { get; internal set; }
		public TileFileManager TileFileManager { get; internal set; }

		public TemplateMod()
		{
			Instance = this;
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

			if (!Main.dedServ)
			{
				ResourcesLoader.LoadAllTextures();

				
				ToolBarServiceManager = new ToolBarServiceManager();
				// GUI管理器
				TileFileManager = new TileFileManager();
				GUIManager = new GUIManager(this);
				TwistedStrength = 0;

			}
		}

		public override void Unload()
		{
			Instance = null;
			TileFileManager = null;
			ToolBarServiceManager = null;
			GUIManager = null;
			ResourcesLoader.Unload();

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
		

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			var cursorIndex = Math.Max(0, layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Cursor"));
			var MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (MouseTextIndex != -1)
			{
				// 插入在鼠标信息之前
				layers.Insert(MouseTextIndex, new UILayer(GUIManager));
			}
			else
			{
				throw new Exception("无法将UI层插入绘制列表");
			}
		}


		public override void UpdateUI(GameTime gameTime)
		{
			GUIManager.UpdateUI(gameTime);
			base.UpdateUI(gameTime);
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
