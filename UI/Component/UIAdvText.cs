//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.UI;
//using Terraria.GameContent.UI.Elements;

//namespace MusicBox.UI
//{
//    public class UIAdvText : UIElement
//    {

//		/// <summary>
//		/// 文本的颜色
//		/// </summary>
//		public Color TextDefaultColor { get; set; }
//		/// <summary>
//		/// 文本的颜色
//		/// </summary>
//		public Color TextShadowColor { get; set; }

//		private float _alpha;

//		private UIText _text;

//        public UIAdvText(string textFalse, string textTrue)
//        {
			
//			TextDefaultColor = new Color(255, 255, 255);
//			TextChangeColor = Color.White;
//			ChangeScale = 1.2f;
//			Value = false;
//			_text = new UIText(ButtonText);
//			_text.Left.Set(0.0f, 0.0f);
//			_text.Top.Set(0.0f, 0.0f);
//			_text.Width.Set(60.0f, 1.0f);
//			_text.Height.Set(30.0f, 1.0f);
//			_text.TextColor = TextDefaultColor;
//			_text.OnClick += _text_OnClick;

//			this.Append(_text);
//		}

//		private void _text_OnClick(UIMouseEvent evt, UIElement listeningElement)
//		{
//			Value = !Value;
//			if (Value)
//			{
//				ButtonText = ButtonTextTrue;
//			}
//			else
//			{
//				ButtonText = ButtonTextFalse;
//			}
//		}

//		public override void MouseOver(UIMouseEvent evt)
//		{
//			Main.PlaySound(12, -1, -1, 1, 1f, 0f);
//			base.MouseOver(evt);
//		}


//		public override void Update(GameTime gameTime)
//		{
//			if (!IsMouseHovering)
//			{
//				if (_alpha > 0)
//					_alpha -= 0.1f;
//				_text.TextColor = Color.Lerp(TextDefaultColor, TextChangeColor, _alpha);
//				_text.SetText(ButtonText, MathHelper.Lerp(1.0f, ChangeScale, _alpha), false);

//			}
//			else
//			{
//				if (_alpha < 1.0f)
//					_alpha += 0.1f;
//				_text.TextColor = Color.Lerp(TextDefaultColor, TextChangeColor, _alpha);
//				_text.SetText(ButtonText, MathHelper.Lerp(1.0f, ChangeScale, _alpha), false);
//			}
//			base.Update(gameTime);
//		}

//    }
//}
