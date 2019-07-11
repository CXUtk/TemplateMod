using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Terraria;

namespace TemplateMod.UI.Component
{
    public class Drawing
    {
        public static Texture2D Box1;
        public static Texture2D Box2;
		public static Texture2D Bar1;
        public static readonly string BuiltRegex = "(#[^#]*?;)";
        public const string RegexColorAdd = @"#\+([0-9a-f]{1,2});";
        public const string RegexColorBrighter = "#>([0-9]{1,3});";
        public const string RegexColorDarker = "#<([0-9]{1,3});";
        public const string RegexColorInvert = @"#\^;";
        public const string RegexColorSub = @"#\-([0-9a-f]{1,2});";
        public const string RegexColorSwitch = "#([RGB]{3});";
        public const string RegexColor1 = "#([0-9a-f]);";
        public const string RegexColor3 = "#([0-9a-f]{3});";
        public const string RegexColor6 = "#([0-9a-f]{6});";
        public const string RegexReset = "#;";
        public const string RegexScale = "#%([0-9]{1,3});";
        public static readonly int[][] ShadowOffset;
        public static Color DefaultBoxColor
        {
            get
            {
                return new Color(0x3f, 0x41, 0x97);
            }
        }
        static Drawing()
        {
            var numArray = new int[4][];
            var numArray2 = new int[2];
            numArray2[0] = -1;
            numArray[0] = numArray2;
            var numArray3 = new int[2];
            numArray3[0] = 1;
            numArray[1] = numArray3;
            var numArray4 = new int[2];
            numArray4[1] = -1;
            numArray[2] = numArray4;
            var numArray5 = new int[2];
            numArray5[1] = 1;
            numArray[3] = numArray5;
            ShadowOffset = numArray;
            Box1 = null;
            Box2 = null;
			Bar1 = null;
        }
		public static void DrawAdvBox(SpriteBatch sp, Rectangle rect, Color c, Texture2D img, Vector2 size4)
		{
			DrawAdvBox(sp, rect.X, rect.Y, rect.Width, rect.Height, c, img, size4);
		}
		public static void DrawAdvBox(SpriteBatch sp, int x, int y, int w, int h, Color c, Texture2D img, Vector2 size4, float scale = 1f)
        {
            var box = img;
			int nw = (int)(w * scale);
			int nh = (int)(h * scale);
			x += (w - nw) / 2;
			y += (h - nh) / 2;
			w = nw;
			h = nh;
            var width = (int)size4.X;
            var height = (int)size4.Y;
            if (w < size4.X)
            {
                w = width;
            }
            if (h < size4.Y)
            {
                h = width;
            }
            sp.Draw(box, new Rectangle(x, y, width, height), new Rectangle(0, 0, width, height), c);
            sp.Draw(box, new Rectangle(x + width, y, w - width * 2, height), new Rectangle(width, 0, box.Width - width * 2, height), c);
            sp.Draw(box, new Rectangle((x + w) - width, y, width, height), new Rectangle(box.Width - width, 0, width, height), c);
            sp.Draw(box, new Rectangle(x, y + height, width, h - height * 2), new Rectangle(0, height, width, box.Height - height * 2), c);
            sp.Draw(box, new Rectangle(x + width, y + height, w - width * 2, h - height * 2), new Rectangle(width, height, box.Width - width * 2, box.Height - height * 2), c);
            sp.Draw(box, new Rectangle((x + w) - width, y + height, width, h - height * 2), new Rectangle(box.Width - width, height, width, box.Height - height * 2), c);
            sp.Draw(box, new Rectangle(x, (y + h) - height, width, height), new Rectangle(0, box.Height - height, width, height), c);
            sp.Draw(box, new Rectangle(x + width, (y + h) - height, w - width * 2, height), new Rectangle(width, box.Height - height, box.Width - width * 2, height), c);
            sp.Draw(box, new Rectangle((x + w) - width, (y + h) - height, width, height), new Rectangle(box.Width - width, box.Height - height, width, height), c);
        }
        public static void DrawBox(SpriteBatch sp, int x, int y, int w, int h, Color c, Texture2D box = null)
        {
            box = box ?? Box1;
            if (w < 10)
            {
                w = 10;
            }
            if (h < 10)
            {
                h = 10;
            }
            sp.Draw(box, new Rectangle(x, y, 10, 10), new Rectangle(0, 0, 10, 10), c);
            sp.Draw(box, new Rectangle(x + 10, y, w - 20, 10), new Rectangle(10, 0, box.Width - 20, 10), c);
            sp.Draw(box, new Rectangle((x + w) - 10, y, 10, 10), new Rectangle(box.Width - 10, 0, 10, 10), c);
            sp.Draw(box, new Rectangle(x, y + 10, 10, h - 20), new Rectangle(0, 10, 10, box.Height - 20), c);
            sp.Draw(box, new Rectangle(x + 10, y + 10, w - 20, h - 20), new Rectangle(10, 10, box.Width - 20, box.Height - 20), c);
            sp.Draw(box, new Rectangle((x + w) - 10, y + 10, 10, h - 20), new Rectangle(box.Width - 10, 10, 10, box.Height - 20), c);
            sp.Draw(box, new Rectangle(x, (y + h) - 10, 10, 10), new Rectangle(0, box.Height - 10, 10, 10), c);
            sp.Draw(box, new Rectangle(x + 10, (y + h) - 10, w - 20, 10), new Rectangle(10, box.Height - 10, box.Width - 20, 10), c);
            sp.Draw(box, new Rectangle((x + w) - 10, (y + h) - 10, 10, 10), new Rectangle(box.Width - 10, box.Height - 10, 10, 10), c);
        }

        public static void DrawBox(SpriteBatch sp, float x, float y, float w, float h, Color c)
        {
            DrawBox(sp, (int)x, (int)y, (int)w, (int)h, c);

        }

        public static void DrawBox(SpriteBatch sp, float x, float y, float w, float h, float a = 0.785f)
        {
            DrawBox(sp, (int)x, (int)y, (int)w, (int)h, (Color)(new Color(0x3f, 0x41, 0x97) * a));
        }

        public static void DrawBox(SpriteBatch sp, Rectangle rect, Color c, float a = 0.785f, Texture2D tex = null)
        {
            DrawBox(sp, rect.X, rect.Y, rect.Width, rect.Height, c * a, tex);
        }

        public static Vector2 DrawColorCodedString(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, float maxW = -1f)
        {
            return DrawColorCodedString(sb, font, text, pos, Color.White, 0f, new Vector2(), 1f, maxW);
        }

        public static Vector2 DrawColorCodedString(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, Color baseColor, float rotation = 0f, Vector2 origin = new Vector2(), float scale = 1f, float maxW = -1f)
        {
            return DrawColorCodedString(sb, font, text, pos, baseColor, rotation, origin, new Vector2(scale, scale), maxW, false);
        }

        public static Vector2 DrawColorCodedString(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxW = -1f, bool ignoreColors = false)
        {
            var position = new Vector2(pos.X, pos.Y);
            var vector2 = position;
            var strArray = text.Split('\n');
            var x = font.MeasureString(" ").X;
            var white = baseColor;
            var num2 = 1f;
            var num3 = 0f;
            foreach (var str in strArray)
            {
                foreach (var str2 in Regex.Split(str, BuiltRegex))
                {
                    if (Regex.Match(str2, BuiltRegex).Success)
                    {
                        if (Regex.Match(str2, "#;").Success)
                        {
                            num2 = 1f;
                            if (!ignoreColors)
                            {
                                white = Color.White;
                            }
                        }
                        else
                        {
                            var match = Regex.Match(str2, "#([0-9a-f]);");
                            if (match.Success)
                            {
                                if (!ignoreColors)
                                {
                                    var r = ((float)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber)) / 15f;
                                    white = new Color(r, r, r, ((float)white.A) / 255f);
                                }
                            }
                            else
                            {
                                match = Regex.Match(str2, "#([0-9a-f]{3});");
                                if (match.Success)
                                {
                                    if (!ignoreColors)
                                    {
                                        var source = match.Groups[1].Value;
                                        var num5 = (int.Parse(source.ElementAt(0).ToString(), NumberStyles.HexNumber)) / 15f;
                                        var g = (int.Parse(source.ElementAt(1).ToString(), NumberStyles.HexNumber)) / 15f;
                                        var b = (int.Parse(source.ElementAt(2).ToString(), NumberStyles.HexNumber)) / 15f;
                                        white = new Color(num5, g, b, ((float)white.A) / 255f);
                                    }
                                }
                                else
                                {
                                    match = Regex.Match(str2, "#([0-9a-f]{6});");
                                    if (match.Success)
                                    {
                                        if (!ignoreColors)
                                        {
                                            var str4 = match.Groups[1].Value;
                                            var num8 = ((float)int.Parse(str4.Substring(0, 2), NumberStyles.HexNumber)) / 255f;
                                            var num9 = ((float)int.Parse(str4.Substring(2, 2), NumberStyles.HexNumber)) / 255f;
                                            var num10 = ((float)int.Parse(str4.Substring(4, 2), NumberStyles.HexNumber)) / 255f;
                                            white = new Color(num8, num9, num10, ((float)white.A) / 255f);
                                        }
                                    }
                                    else
                                    {
                                        match = Regex.Match(str2, "#>([0-9]{1,3});");
                                        if (match.Success)
                                        {
                                            if (!ignoreColors)
                                            {
                                                var num11 = int.Parse(match.Groups[1].Value);
                                                white = new Color((((float)white.R) / 255f) * (1f + (num11 * 0.01f)), (((float)white.G) / 255f) * (1f + (num11 * 0.01f)), (((float)white.B) / 255f) * (1f + (num11 * 0.01f)), ((float)white.A) / 255f);
                                            }
                                        }
                                        else
                                        {
                                            match = Regex.Match(str2, "#<([0-9]{1,3});");
                                            if (match.Success)
                                            {
                                                if (!ignoreColors)
                                                {
                                                    var num12 = int.Parse(match.Groups[1].Value);
                                                    white = new Color((((float)white.R) / 255f) * (1f - (num12 * 0.01f)), (((float)white.G) / 255f) * (1f - (num12 * 0.01f)), (((float)white.B) / 255f) * (1f - (num12 * 0.01f)), ((float)white.A) / 255f);
                                                }
                                            }
                                            else
                                            {
                                                match = Regex.Match(str2, @"#\+([0-9a-f]{1,2});");
                                                if (match.Success)
                                                {
                                                    if (!ignoreColors)
                                                    {
                                                        var num13 = int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
                                                        white = new Color((byte)Math.Min(white.R + num13, 0xff), (byte)Math.Min(white.G + num13, 0xff), (byte)Math.Min(white.B + num13, 0xff), white.A);
                                                    }
                                                }
                                                else
                                                {
                                                    match = Regex.Match(str2, @"#\-([0-9a-f]{1,2});");
                                                    if (match.Success)
                                                    {
                                                        if (!ignoreColors)
                                                        {
                                                            var num14 = int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
                                                            white = new Color((byte)Math.Max(white.R - num14, 0), (byte)Math.Max(white.G - num14, 0), (byte)Math.Max(white.B - num14, 0), white.A);
                                                        }
                                                    }
                                                    else if (Regex.Match(str2, @"#\^;").Success)
                                                    {
                                                        if (!ignoreColors)
                                                        {
                                                            white = new Color((byte)(0xff - white.R), (byte)(0xff - white.G), (byte)(0xff - white.B), white.A);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        match = Regex.Match(str2, "#([RGB]{3});");
                                                        if (match.Success)
                                                        {
                                                            if (!ignoreColors)
                                                            {
                                                                var str5 = match.Groups[1].Value;
                                                                int num15 = (str5.ElementAt(0) == 'R') ? white.R : ((str5.ElementAt(0) == 'G') ? white.G : white.B);
                                                                int num16 = (str5.ElementAt(1) == 'R') ? white.R : ((str5.ElementAt(1) == 'G') ? white.G : white.B);
                                                                int num17 = (str5.ElementAt(2) == 'R') ? white.R : ((str5.ElementAt(2) == 'G') ? white.G : white.B);
                                                                white = new Color(num15, num16, num17, white.A);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            match = Regex.Match(str2, "#%([0-9]{1,3});");
                                                            if (match.Success)
                                                            {
                                                                num2 = int.Parse(match.Groups[1].Value) * 0.01f;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var strArray3 = str2.Split(' ');
                        for (var i = 0; i < strArray3.Length; i++)
                        {
                            if (i != 0)
                            {
                                position.X += (x * baseScale.X) * num2;
                            }
                            if (maxW > 0f)
                            {
                                var num19 = (font.MeasureString(strArray3[i]).X * baseScale.X) * num2;
                                if (((position.X - pos.X) + num19) > maxW)
                                {
                                    position.X = pos.X;
                                    position.Y += (font.LineSpacing * num3) * baseScale.Y;
                                    vector2.Y = Math.Max(vector2.Y, position.Y);
                                    num3 = 0f;
                                }
                            }
                            if (num3 < num2)
                            {
                                num3 = num2;
                            }
                            sb.DrawString(font, strArray3[i], position, white, rotation, origin, (Vector2)(baseScale * num2), SpriteEffects.None, 0f);
                            position.X += (font.MeasureString(strArray3[i]).X * baseScale.X) * num2;
                            vector2.X = Math.Max(vector2.X, position.X);
                        }
                    }
                }
                position.X = pos.X;
                position.Y += (font.LineSpacing * num3) * baseScale.Y;
                vector2.Y = Math.Max(vector2.Y, position.Y);
                num3 = 0f;
            }
            return vector2;
        }

        public static void DrawColorCodedStringShadow(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, float maxW = -1f, int offset = 1)
        {
            DrawColorCodedStringShadow(sb, font, text, pos, new Color(0f, 0f, 0f, 0.5f), 0f, new Vector2(), (float)1f, maxW, offset);
        }

        public static void DrawColorCodedStringShadow(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, Color color, float rotation, Vector2 origin, Vector2 scale, float maxW = -1f, int offset = 1)
        {
            color = new Color((int)color.R, (int)color.G, (int)color.B, (int)((byte)(Math.Pow((double)(((float)color.A) / 255f), 2.0) * 255.0)));
            foreach (var t in ShadowOffset)
            {
                DrawColorCodedString(sb, font, text, new Vector2(pos.X + (t[0] * offset), pos.Y + (t[1] * offset)), color, rotation, origin, scale, maxW, true);
            }
        }

        public static void DrawColorCodedStringShadow(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, Color color, float rotation = 0f, Vector2 origin = new Vector2(), float scale = 1f, float maxW = -1f, int offset = 1)
        {
            DrawColorCodedStringShadow(sb, font, text, pos, color, rotation, origin, new Vector2(scale, scale), maxW, offset);
        }

        public static void DrawStringShadow(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, int offset = 1)
        {
            DrawStringShadow(sb, font, text, pos, new Color(0f, 0f, 0f, 0.5f), 0f, new Vector2(), (float)1f, offset);
        }

        public static void DrawStringShadow(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, Color color, float rotation, Vector2 origin, Vector2 scale, int offset = 1)
        {
            color = new Color((int)color.R, (int)color.G, (int)color.B, (int)((byte)(Math.Pow((double)(((float)color.A) / 255f), 2.0) * 255.0)));
            foreach (var t in ShadowOffset)
            {
                sb.DrawString(font, text, new Vector2(pos.X + (t[0] * offset), pos.Y + (t[1] * offset)), color, rotation, origin, scale, SpriteEffects.None, 0f);
            }
        }

        public static void DrawStringShadow(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, Color color, float rotation = 0f, Vector2 origin = new Vector2(), float scale = 1f, int offset = 1)
        {
            DrawStringShadow(sb, font, text, pos, color, rotation, origin, new Vector2(scale, scale), offset);
        }

        public static string DropColorCodes(string text)
        {
            var strArray = Regex.Split(text, BuiltRegex);
            var builder = new StringBuilder();
            foreach (var str in strArray)
            {
                if (!Regex.Match(str, BuiltRegex).Success)
                {
                    builder.Append(str);
                }
            }
            return builder.ToString();
        }

        public static Vector2 DString(SpriteBatch sp, string se, Vector2 ve, Color ce, float fe = 1f, float rex = 0f, float rey = 0f, int font = 0)
        {
            var fontMouseText = Main.fontMouseText;
            if (font == 1)
            {
                fontMouseText = Main.fontDeathText;
            }
            for (var i = -1; i < 2; i++)
            {
                for (var j = -1; j < 2; j++)
                {
                    sp.DrawString(fontMouseText, se, ve + ((Vector2)(new Vector2((float)i, (float)j) * 1f)), Color.Black, 0f, new Vector2(rex, rey) * fontMouseText.MeasureString(se), fe, SpriteEffects.None, 0f);
                }
            }
            sp.DrawString(fontMouseText, se, ve, ce, 0f, new Vector2(rex, rey) * fontMouseText.MeasureString(se), fe, SpriteEffects.None, 0f);
            return (Vector2)(fontMouseText.MeasureString(se) * fe);
        }

        public static Vector2 MeasureColorCodedString(ReLogic.Graphics.DynamicSpriteFont font, string text)
        {
            return font.MeasureString(DropColorCodes(text));
        }

        public static void StringShadowed(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, int offset = 2)
        {
            StringShadowed(sb, font, text, pos, Color.White, 1f, new Vector2(), offset);
        }

        public static void StringShadowed(SpriteBatch sb, ReLogic.Graphics.DynamicSpriteFont font, string text, Vector2 pos, Color c, float scale = 1f, Vector2 origin = new Vector2(), int offset = 2)
        {
            DrawStringShadow(sb, font, text, pos, new Color(0, 0, 0, c.A), 0f, origin, scale, offset);
            sb.DrawString(font, text, pos, c, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb">spritebatch画图类</param>
        /// <param name="tex">需要绘制的图案</param>
        /// <param name="start">开始点</param>
        /// <param name="end">结束点</param>
        /// <param name="scale"></param>
        /// <param name="rotation">旋转弧度</param>
        /// <param name="c"></param>
        public static void DrawLine(SpriteBatch sb,
            Texture2D tex,
            Vector2 start,
            Vector2 end,
            Color c = default(Color),
            float step = 4f,
            float scale = 1f, 
            float rotation = 0f)
        {
            var dis = Vector2.Distance(start, end);
            var unit = (end - start) / dis;
            var orig = start;
            var r = unit.ToRotation() + rotation;
            var size = tex.Size() / 2;
            for (float i = 0; i <= dis; i += step)
            {
                sb.Draw(tex, orig - Main.screenPosition, null, c, r, size, scale, 0, 0);
                orig = start + i * unit;
            }
            sb.Draw(tex, end, null, c, r, size, scale, 0, 0);
        }
        public static void DrawLine(SpriteBatch sb,
                Vector2 start,
                Vector2 end,
                Color c = default(Color),
                float step = 4f,
                float scale = 1f,
                float rotation = 0f)
        {
            start -= Main.screenPosition;
            end -= Main.screenPosition;
            var dis = Vector2.Distance(start, end);
            var unit = (end - start) / dis;
            var orig = start;
            var r = unit.ToRotation() + rotation;
            for (float i = 0; i <= dis; i += step)
            {
                sb.Draw(Main.magicPixel, orig, new Rectangle(0, 0, 2, 2), c, r, Vector2.One, scale, 0, 0);
                orig = start + i * unit;
            }
            sb.Draw(Main.magicPixel, orig, new Rectangle(0, 0, 2, 2), c, r, Vector2.One, scale, 0, 0);
        }
        public static void CenterDraw(SpriteBatch sb, Texture2D tex, Vector2 center, Color c, Rectangle? rect = null, float rotation = 0f, float scale = 1f, SpriteEffects effect = SpriteEffects.None)
        {
            sb.Draw(tex, center, rect, c, rotation, new Vector2(tex.Width * 0.5f, tex.Height * 0.5f), scale, effect, 0);
        }

    }
}
