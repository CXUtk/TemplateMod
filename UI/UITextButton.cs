using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TemplateMod.Utils;
using Terraria.GameContent.UI.Elements;

namespace TemplateMod.UI
{
    public class UITextButton : UIElement
    {
        /// <summary>
        /// 显现在按钮上的文本
        /// </summary>
        public string ButtonText { get; set; }
		/// <summary>
		/// 显现在按钮上的文本
		/// </summary>
		public string ButtonTextTrue { get; set; }
		/// <summary>
		/// 显现在按钮上的文本
		/// </summary>
		public string ButtonTextFalse { get; set; }
		/// <summary>
		/// 按钮文本的颜色
		/// </summary>
		public Color TextDefaultColor { get; set; }
		/// <summary>
		/// 按钮变换的颜色
		/// </summary>
		public Color TextChangeColor { get; set; }
		/// <summary>
		/// 鼠标移动放大倍数
		/// </summary>
		public float ChangeScale { get; set; }
		/// <summary>
		/// 默认值，true
		/// </summary>
		public bool Value { get; private set; }

        private float _alpha;

		private UIText _text;

        public UITextButton(string textFalse, string textTrue)
        {
			ButtonText = textFalse;
			ButtonTextFalse = textFalse;
			ButtonTextTrue = textTrue;
			TextDefaultColor = new Color(200, 200, 200);
			TextChangeColor = Color.White;
			ChangeScale = 1.2f;
			Value = false;
			_text = new UIText(ButtonText);
			_text.Left.Set(0.0f, 0.0f);
			_text.Top.Set(0.0f, 0.0f);
			_text.Width.Set(60.0f, 1.0f);
			_text.Height.Set(30.0f, 1.0f);
			_text.TextColor = TextDefaultColor;
			_text.OnClick += _text_OnClick;

			this.Append(_text);
		}

		private void _text_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Value = !Value;
			if (Value)
			{
				ButtonText = ButtonTextTrue;
			}
			else
			{
				ButtonText = ButtonTextFalse;
			}
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			Main.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.MouseOver(evt);
		}


		public override void Update(GameTime gameTime)
		{
			if (!IsMouseHovering)
			{
				if (_alpha > 0)
					_alpha -= 0.1f;
				_text.TextColor = Color.Lerp(TextDefaultColor, TextChangeColor, _alpha);
				_text.SetText(ButtonText, MathHelper.Lerp(1.0f, ChangeScale, _alpha), false);

			}
			else
			{
				if (_alpha < 1.0f)
					_alpha += 0.1f;
				_text.TextColor = Color.Lerp(TextDefaultColor, TextChangeColor, _alpha);
				_text.SetText(ButtonText, MathHelper.Lerp(1.0f, ChangeScale, _alpha), false);
			}
			base.Update(gameTime);
		}

    }
}
