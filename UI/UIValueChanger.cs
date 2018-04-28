using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using TemplateMod.Utils;

namespace TemplateMod.UI
{
	public delegate void ValueChangeEvent(float value, UIElement sender);

    public class UIValueChanger : UIElement
    {
		public float CurrentValue { get; private set; }

		public float MaxValue { get; }

		public float MinValue { get; }

		public int Digits { get; set; }

		public event ValueChangeEvent OnValueChange;

		private UIButton _addButton;

		private UIButton _subButton;

		private UIText _valueDisplay;

		private float _step;

		public UIValueChanger(float minValue, float maxValue, float step, string label)
        {
			MinValue = minValue;
			MaxValue = maxValue;
			_step = step;
			CurrentValue = MinValue;

			_subButton = new UIButton(Drawing.Box1);
			_subButton.ButtonText = "-";
			_subButton.Left.Set(0.0f, 0.0f);
			_subButton.Top.Set(0.0f, 0.0f);
			_subButton.Width.Set(30.0f, 0.0f);
			_subButton.Height.Set(30.0f, 0.0f);
			_subButton.OnClick += _minusButton_OnClick;

			_addButton = new UIButton(Drawing.Box1);
			_addButton.ButtonText = "+";
			_addButton.Left.Set(-30.0f, 1.0f);
			_addButton.Top.Set(0.0f, 0.0f);
			_addButton.Width.Set(30.0f, 0.0f);
			_addButton.Height.Set(30.0f, 0.0f);
			_addButton.OnClick += _addButton_OnClick;

			_valueDisplay = new UIText(CurrentValue.ToString());
			_valueDisplay.Left.Set(-30.0f, 0.5f);
			_valueDisplay.Top.Set(10.0f, 0.0f);
			_valueDisplay.Width.Set(60.0f, 0.0f);
			_valueDisplay.Height.Set(30.0f, 0.0f);

			UIText label1 = new UIText(label);
			label1.Left.Set(-100f, 0.0f);
			label1.Top.Set(10.0f, 0.0f);
			label1.Width.Set(100f, 0.0f);
			label1.Height.Set(30.0f, 0.0f);

			this.Append(label1);
			this.Append(_addButton);
			this.Append(_subButton);
			this.Append(_valueDisplay);
		}

		public void SetValue(float value)
		{
			CurrentValue = value;
			UpdateText();
		}

		private void UpdateText()
		{
			if(OnValueChange != null)
				OnValueChange(CurrentValue, this);
			string format = "F" + Digits.ToString();
			_valueDisplay.SetText(CurrentValue.ToString(format));
		}

		private void _minusButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (CurrentValue > MinValue)
				CurrentValue -= _step;
			else
				CurrentValue = MinValue;

			UpdateText();
		}

		private void _addButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (CurrentValue < MaxValue)
				CurrentValue += _step;
			else
				CurrentValue = MaxValue;

			UpdateText();
		}

    }
}
