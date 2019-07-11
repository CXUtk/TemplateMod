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

//namespace TemplateMod.UI.Component
//{

//    public class UIValueChanger : UIElement
//    {
//		/// <summary>
//		/// 当前值，浮点
//		/// </summary>
//		public float CurrentValue { get; private set; }
//		/// <summary>
//		/// 最大值
//		/// </summary>
//		public float MaxValue { get; private set; }
//		/// <summary>
//		/// 最小值
//		/// </summary>
//		public float MinValue { get; private set; }
//		/// <summary>
//		/// 保留小数点的位数
//		/// </summary>
//		public int Digits { get; set; }

//		public event ValueChangeEvent OnValueChange;

//		private UIButton _addButton;

//		private UIButton _subButton;

//		private UIText _valueDisplay;

//		private float _step;

//		private float _pressCounter;

//		private const float PRESS_DELAY = 15.0f;

//		public UIValueChanger(float minValue, float maxValue, float step, string label)
//        {
//			MinValue = minValue;
//			MaxValue = maxValue;
//			_step = step;
//			CurrentValue = MinValue;

//			_subButton = new UIButton(Drawing.Box1);
//			_subButton.ButtonText = "-";
//			_subButton.Left.Set(0.0f, 0.0f);
//			_subButton.Top.Set(0.0f, 0.0f);
//			_subButton.Width.Set(30.0f, 0.0f);
//			_subButton.Height.Set(30.0f, 0.0f);
//			_subButton.OnClick += _minusButton_OnClick;
//			_subButton.OnMouseDown += _subButton_OnMouseDown;

//			_addButton = new UIButton(Drawing.Box1);
//			_addButton.ButtonText = "+";
//			_addButton.Left.Set(-30.0f, 1.0f);
//			_addButton.Top.Set(0.0f, 0.0f);
//			_addButton.Width.Set(30.0f, 0.0f);
//			_addButton.Height.Set(30.0f, 0.0f);
//			_addButton.OnClick += _addButton_OnClick;
//			_addButton.OnMouseDown += _addButton_OnMouseDown;

//			_valueDisplay = new UIText(CurrentValue.ToString());
//			_valueDisplay.Left.Set(-30.0f, 0.5f);
//			_valueDisplay.Top.Set(10.0f, 0.0f);
//			_valueDisplay.Width.Set(60.0f, 0.0f);
//			_valueDisplay.Height.Set(30.0f, 0.0f);

//			UIText label1 = new UIText(label);
//			label1.Left.Set(-100f, 0.0f);
//			label1.Top.Set(10.0f, 0.0f);
//			label1.Width.Set(100f, 0.0f);
//			label1.Height.Set(30.0f, 0.0f);

//			this.Append(label1);
//			this.Append(_addButton);
//			this.Append(_subButton);
//			this.Append(_valueDisplay);
//		}


//		private void _addButton_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
//		{
//			_pressCounter = 0;
//		}

//		private void _subButton_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
//		{
//			_pressCounter = 0;
//		}

//		public void SetValue(float value)
//		{
//			CurrentValue = value;
//			UpdateText();
//		}

//		public override void Update(GameTime gameTime)
//		{
//			base.Update(gameTime);
//			if (Main.mouseLeft)
//			{
//				if (_subButton.IsMouseHovering || _addButton.IsMouseHovering)
//				{
//					_pressCounter++;
//				}
//				if (Main.time % 2 < 1)
//				{
//					if (_pressCounter > PRESS_DELAY && _subButton.IsMouseHovering)
//					{
//						if (CurrentValue > MinValue)
//							CurrentValue -= _step;
//						else
//							CurrentValue = MinValue;
//					}
//					else if (_pressCounter > PRESS_DELAY && _addButton.IsMouseHovering)
//					{
//						if (CurrentValue < MaxValue)
//							CurrentValue += _step;
//						else
//							CurrentValue = MaxValue;
//					}
//				}
//				UpdateText();
//			}
//		}

//		private void UpdateText()
//		{
//			if(OnValueChange != null)
//				OnValueChange(CurrentValue, this);
//			string format = "F" + Digits.ToString();
//			_valueDisplay.SetText(CurrentValue.ToString(format));
//		}

//		private void _minusButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
//		{
//			if (CurrentValue > MinValue)
//				CurrentValue -= _step;
//			else
//				CurrentValue = MinValue;
//			_pressCounter = 0;
//			UpdateText();
//		}

//		private void _addButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
//		{
//			if (CurrentValue < MaxValue)
//				CurrentValue += _step;
//			else
//				CurrentValue = MaxValue;
//			_pressCounter = 0;
//			UpdateText();
//		}

//    }
//}
