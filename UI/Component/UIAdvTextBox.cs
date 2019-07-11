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
using ReLogic.Graphics;
using Terraria.GameInput;
using ReLogic.OS;
using Microsoft.Xna.Framework.Input;
using Terraria.UI.Chat;

namespace TemplateMod.UI.Component
{
	public delegate void TextChangeHandler(string oldString, string curString);
	/// <summary>
	/// 神奇的Textbox，输入法支持以及滚动字幕，还支持密码屏蔽
	/// </summary>
	public class UIAdvTextBox : ToggableElement
	{
		private const int BLINK_INTERVAL = 20;
		private const float TEXT_PADDING = 8;

		private bool textBlinking;
		private int textBlinkCount;
		private string appearedText;

		public bool Focused { get; private set; }

		public DynamicSpriteFont Font { get; set; }

		public Color ForegroundColor { get; set; }

		public string Text { get; set; }

		public bool Password { get; set; }

		private bool _mouseDowned = false;

		public event TextChangeHandler OnTextChange;


		public UIAdvTextBox()
		{
			textBlinking = false;
			textBlinkCount = BLINK_INTERVAL;
			Font = Main.fontMouseText;
			ForegroundColor = Color.Black;
			Password = false;
			appearedText = "";
			Text = "";

			var textPanel = new UIAdvPanel(TemplateMod.ModTexturesTable["Box"]) {Color = Color.White};
			textPanel.Top.Set(0, 0);
			textPanel.Left.Set(0, 0);
			textPanel.Width.Set(0, 1);
			textPanel.Height.Set(0, 1);
			this.Append(textPanel);
		}


		public override void Click(UIMouseEvent evt)
		{
			Focus();
			base.Click(evt);
		}


		private void UnFocus()
		{
			Focused = false;
			textBlinkCount = 0;
			textBlinking = false;
		}

		private void Focus()
		{
			Focused = true;
			textBlinkCount = BLINK_INTERVAL;
			textBlinking = true;
		}


		public override void Update(GameTime gameTime)
		{
			if (Main.mouseLeft)
			{
				_mouseDowned = true;
			}
			if (Main.mouseLeftRelease)
			{
				if (_mouseDowned)
				{
					if (!IsMouseHovering)
					{
						UnFocus();
					}
				}
				_mouseDowned = false;
			}

			if (Focused)
			{
				if (textBlinkCount > 0)
					textBlinkCount--;
				else
				{
					textBlinkCount = BLINK_INTERVAL;
					textBlinking = !textBlinking;
				}
			}
			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (Focused)
			{
				PlayerInput.WritingText = true;
				if (Enabled)
				{
					if (!Password)
					{
						Main.instance.HandleIME();
					}
					var oldText = Text;
					Text = GetInputText(Text);
					if (oldText != Text)
					{
						// 按键事件
						OnTextChange?.Invoke(oldText, Text);
					}
					if (!Password && Platform.Current.Ime.CompositionString.Length > 0)
					{
						Main.instance.DrawWindowsIMEPanel(new Vector2(98f, (float)(Main.screenHeight - 36)), 0f);
					}
					if (Main.inputText.IsKeyDown(Keys.Escape))
					{
						UnFocus();
					}
				}
			}


			AdjustAppearText();

			var dim = GetInnerDimensions();

			var strSize = Font.MeasureString(appearedText);
			spriteBatch.DrawString(Font, appearedText, new Vector2(dim.Position().X + TEXT_PADDING, dim.Center().Y - strSize.Y / 2 + 4), ForegroundColor);
		}


		private void AdjustAppearText()
		{
			appearedText = Text;
			if (!Enabled) return;
			if (Focused) appearedText += Platform.Current.Ime.CompositionString;
			if (Password)
			{
				var sb = new StringBuilder();
				for (var i = 0; i < appearedText.Length; i++)
				{
					sb.Append('*');
				}
				appearedText = sb.ToString();
			}
			var strSize = Font.MeasureString(appearedText);
			if(strSize.X > Width.Pixels - TEXT_PADDING * 2 - 4)
			{
				var sb = new StringBuilder();
				for(var i = appearedText.Length - 1; i >= 0; i--)
				{
					sb.Append(appearedText[i]);
					if (Font.MeasureString(sb.ToString()).X >= Width.Pixels - TEXT_PADDING * 2 - 4)
					{
						break;
					}
				}
				var tmp = sb.ToString();
				sb.Clear();
				for(var i = tmp.Length - 1; i >= 0; i--)
				{
					sb.Append(tmp[i]);
				}
				appearedText = sb.ToString();
			}
			if (textBlinking)
			{
				appearedText += "|";
			}
		}

		private static int backSpaceCount;

		private static int lastCompSize;

		private static string GetInputText(string oldString)
		{
			if (!Main.hasFocus)
			{
				return oldString;
			}
			Main.inputTextEnter = false;
			Main.inputTextEscape = false;
			var text = oldString;
			var text2 = "";
			if (text == null)
			{
				text = "";
			}
			var flag = false;
			if (Main.inputText.IsKeyDown(Keys.LeftControl) || Main.inputText.IsKeyDown(Keys.RightControl))
			{
				if (Main.inputText.IsKeyDown(Keys.Z) && !Main.oldInputText.IsKeyDown(Keys.Z))
				{
					text = "";
				}
				else if (Main.inputText.IsKeyDown(Keys.X) && !Main.oldInputText.IsKeyDown(Keys.X))
				{
					Platform.Current.Clipboard = oldString;
					text = "";
				}
				else if ((Main.inputText.IsKeyDown(Keys.C) && !Main.oldInputText.IsKeyDown(Keys.C)) || (Main.inputText.IsKeyDown(Keys.Insert) && !Main.oldInputText.IsKeyDown(Keys.Insert)))
				{
					Platform.Current.Clipboard = oldString;
				}
				else if (Main.inputText.IsKeyDown(Keys.V) && !Main.oldInputText.IsKeyDown(Keys.V))
				{
					text2 += Platform.Current.Clipboard;
				}
			}
			else
			{
				if (Main.inputText.PressingShift())
				{
					if (Main.inputText.IsKeyDown(Keys.Delete) && !Main.oldInputText.IsKeyDown(Keys.Delete))
					{
						Platform.Current.Clipboard = oldString;
						text = "";
					}
					if (Main.inputText.IsKeyDown(Keys.Insert) && !Main.oldInputText.IsKeyDown(Keys.Insert))
					{
						var text3 = Platform.Current.Clipboard;
						for (var i = 0; i < text3.Length; i++)
						{
							if (text3[i] < ' ' || text3[i] == '\u007f')
							{
								text3 = text3.Replace(string.Concat(text3[i--]), "");
							}
						}
						text2 += text3;
					}
				}
				for (var j = 0; j < Main.keyCount; j++)
				{
					var num = Main.keyInt[j];
					var str = Main.keyString[j];
					switch (num)
					{
						case 13:
							Main.inputTextEnter = true;
							break;
						case 27:
							Main.inputTextEscape = true;
							break;
						default:
						{
							if (num >= 32 && num != 127)
							{
								text2 += str;
							}
							break;
						}
					}
				}
			}
			Main.keyCount = 0;
			text += text2;
			Main.oldInputText = Main.inputText;
			Main.inputText = Keyboard.GetState();
			var pressedKeys = Main.inputText.GetPressedKeys();
			var pressedKeys2 = Main.oldInputText.GetPressedKeys();
			if (Main.inputText.IsKeyDown(Keys.Back) && Main.oldInputText.IsKeyDown(Keys.Back))
			{
				if (backSpaceCount == 0)
				{
					backSpaceCount = 7;
					flag = true;
				}
				backSpaceCount--;
			}
			else
			{
				backSpaceCount = 15;
			}
			foreach (var k in pressedKeys)
			{
				var flag2 = true;
				foreach (var key in pressedKeys2)
				{
					if (k == key)
					{
						flag2 = false;
					}
				}
				var a = string.Concat(k);
				if (a == "Back" && lastCompSize == 0 && (flag2 || flag) && text.Length > 0)
				{
					var array = ChatManager.ParseMessage(text, Color.White).ToArray();
					text = array[array.Length - 1].DeleteWhole ? text.Substring(0, text.Length - array[array.Length - 1].TextOriginal.Length) : text.Substring(0, text.Length - 1);
				}
			}
			lastCompSize = Platform.Current.Ime.CompositionString.Length;
			return text;
		}

	}
}
