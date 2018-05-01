using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using TemplateMod.UI;
using TemplateMod.Utils;

namespace TemplateMod.UICollection
{
	public class DustTestUI : WindowUIState
	{
		class DustInfo
		{
			public bool noGravity = false;
			public bool noLight = false;
			public int numberPerFrame = 1;
			public int dustID = 0;
			public int alpha = 100;
			public float scale = 1.0f;
			public float speedX = 0.0f;
			public float speedY = 0.0f;
			public Color color = Color.White;
		}

		private UIButton _testButton;

		private DustInfo _dustInfo;

		private UIValueChanger _valueChangerID;

		private UIValueChanger _valueChangerNumber;

		private UIValueChanger _valueChangerScale;

		private UITextButton _textButton;

		private bool DustOn = false;

		protected override void Initialize(UIPanel WindowPanel)
		{
			_dustInfo = new DustInfo();
			WindowPanel.SetPadding(0);
			WindowPanel.Left.Set(Main.screenWidth / 2 - 150f, 0f);
			WindowPanel.Top.Set(Main.screenHeight / 2 - 150f, 0f);
			WindowPanel.Width.Set(300f, 0f);
			WindowPanel.Height.Set(300f, 0f);
			WindowPanel.BackgroundColor = Color.Gray * 0.7f;

			_testButton = new UIButton(Drawing.Box1);
			_testButton.ButtonText = "释放Dust";
			_testButton.ButtonDefaultColor = Color.Green * 0.8f;
			_testButton.Left.Set(-45, 0.5f);
			_testButton.Top.Set(-70, 1.0f);
			_testButton.Width.Set(90, 0f);
			_testButton.Height.Set(60, 0f);
			_testButton.OnClick += _testButton_OnClick;

			_valueChangerID = new UIValueChanger(0.0f, 300.0f, 1.0f, "DustID：");
			_valueChangerID.Left.Set(120, 0.0f);
			_valueChangerID.Top.Set(20, 0.0f);
			_valueChangerID.Width.Set(100, 0f);
			_valueChangerID.Height.Set(30, 0f);
			_valueChangerID.OnValueChange += _valueChanger_OnValueChange;

			_valueChangerNumber = new UIValueChanger(1.0f, 15.0f, 1.0f, "Dust数量：");
			_valueChangerNumber.Left.Set(120, 0.0f);
			_valueChangerNumber.Top.Set(60, 0.0f);
			_valueChangerNumber.Width.Set(100, 0f);
			_valueChangerNumber.Height.Set(30, 0f);
			_valueChangerNumber.OnValueChange += _valueChangerID_OnValueChange;

			_valueChangerScale = new UIValueChanger(0.0f, 5.0f, 0.25f, "Dust大小：");
			_valueChangerScale.Digits = 2;
			_valueChangerScale.Left.Set(120, 0.0f);
			_valueChangerScale.Top.Set(100, 0.0f);
			_valueChangerScale.Width.Set(100, 0f);
			_valueChangerScale.Height.Set(30, 0f);
			_valueChangerScale.OnValueChange += _valueChangerScale_OnValueChange;
			_valueChangerScale.SetValue(1.0f);

			_textButton = new UITextButton("重力关闭", "重力开启");
			_textButton.TextChangeColor = Color.Yellow;
			_textButton.Left.Set(120, 0.0f);
			_textButton.Top.Set(150, 0.0f);
			_textButton.Width.Set(60, 0f);
			_textButton.Height.Set(30, 0f);
			_textButton.OnClick += TextButton_OnClick;

			WindowPanel.Append(_textButton);
			WindowPanel.Append(_valueChangerScale);
			WindowPanel.Append(_valueChangerID);
			WindowPanel.Append(_valueChangerNumber);
			WindowPanel.Append(_testButton);
		}

		private void TextButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			_dustInfo.noGravity = _textButton.Value;
		}

		private void _valueChangerScale_OnValueChange(float value, UIElement sender)
		{
			_dustInfo.scale = value;
		}

		private void _valueChangerID_OnValueChange(float value, UIElement sender)
		{
			_dustInfo.numberPerFrame = (int)_valueChangerNumber.CurrentValue;
		}

		private void _valueChanger_OnValueChange(float value, UIElement sender)
		{
			_dustInfo.dustID = (int)_valueChangerID.CurrentValue;
		}

		private void _testButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			DustOn = !DustOn;
		}

		protected override void OnClose(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuOpen);
			TemplateMod.ShowDustTestUI = false;
		}

		public override void Update(GameTime gameTime)
		{
			if (DustOn)
			{
				_testButton.ButtonDefaultColor = Color.Red * 0.8f;
				_testButton.ButtonText = "关闭Dust";
			}
			else
			{
				_testButton.ButtonDefaultColor = Color.Green * 0.8f;
				_testButton.ButtonText = "释放Dust";
			}
			if (DustOn)
			{
				for (int i = 0; i < _dustInfo.numberPerFrame; i++)
				{
					Dust d = Dust.NewDustDirect(Main.MouseWorld - new Vector2(10, 10),
						20, 20, _dustInfo.dustID, _dustInfo.speedX, _dustInfo.speedY,
						_dustInfo.alpha, _dustInfo.color, _dustInfo.scale);
					d.noGravity = _dustInfo.noGravity;
					d.noLight = _dustInfo.noLight;
				}
			}
			base.Update(gameTime);
		}

	}
}