
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using ReLogic.Graphics;

namespace TemplateMod.UI.Component
{
	public delegate bool CheckPutSlotCondition(Item mouseItem);
	public delegate void ExchangeItemHandler(UIElement target);

	public class UISlot : UIAdvElement
	{
		public Texture2D SlotBackTexture { get; set; }
		public CheckPutSlotCondition CanPutInSlot { get; set; }
		public CheckPutSlotCondition CanTakeOutSlot { get; set; }
		public Item ContainedItem { get; set; }
		public Vector2 CornerSize { get; set; }
		public Color DrawColor { get; set; }
		public string Tooltip { get; set; }
		public event ExchangeItemHandler PostExchangeItem;
		public event ExchangeItemHandler OnPickItem;
		public float Scale { get; set; }
		public float Opacity { get; set; }
		public UISlot(Texture2D texture = default(Texture2D)) : base()
		{
			Scale = 1f;
			Opacity = 1f;
			ContainedItem = new Item();
			CanPutInSlot = null;
			SlotBackTexture = texture == default(Texture2D) ? Drawing.Box1 : texture;
			DrawColor = Drawing.DefaultBoxColor * 0.75f;
			CornerSize = new Vector2(10, 10);
			Tooltip = "";
		}

		public override void Update(GameTime gameTime)
		{
			if (Tooltip != "" && ContainsPoint(Main.MouseScreen) && (ContainedItem == null || ContainedItem.type == 0))
			{
				TemplateMod.ShowTooltip = Tooltip;
			}
			base.Update(gameTime);

		}

		public override void MouseUp(UIMouseEvent evt)
		{

			base.MouseUp(evt);
		}

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
			Main.playerInventory = true;
			if (Main.mouseItem.type == 0 && ContainedItem.type != 0)
			{
				if (CanTakeOutSlot == null || CanTakeOutSlot(ContainedItem))
				{
					Main.mouseItem = ContainedItem.Clone();
					ContainedItem = new Item();
					ContainedItem.SetDefaults(0, true);
					OnPickItem?.Invoke(this);
				}
			}
			else if (Main.mouseItem.type != 0 && ContainedItem.type == 0)
			{
				if (CanPutInSlot == null || CanPutInSlot(Main.mouseItem))
				{
					ContainedItem = Main.mouseItem.Clone();
					Main.mouseItem = new Item();
					Main.mouseItem.SetDefaults(0, true);
				}
			}
			else if (Main.mouseItem.type != 0 && ContainedItem.type != 0)
			{
				if (!(CanPutInSlot == null || CanPutInSlot(Main.mouseItem))) return;
				if (Main.mouseItem.type == ContainedItem.type)
				{
					ContainedItem.stack += Main.mouseItem.stack;
					if (ContainedItem.stack > ContainedItem.maxStack)
					{
						int exceed = ContainedItem.stack - ContainedItem.maxStack;
						ContainedItem.stack = ContainedItem.maxStack;
						Main.mouseItem.stack = exceed;
					}
					else
					{
						Main.mouseItem = new Item();
					}
				}
				else if ((CanPutInSlot == null || CanPutInSlot(Main.mouseItem))
					&& (CanTakeOutSlot == null || CanTakeOutSlot(ContainedItem)))
				{
					Item tmp = Main.mouseItem.Clone();
					Main.mouseItem = ContainedItem;
					ContainedItem = tmp;
				}
			}
			else
				return;
			Main.PlaySound(7, -1, -1, 1, 1f, 0.0f);
			PostExchangeItem?.Invoke(this);
		}



		protected override void DrawSelf(SpriteBatch sb)
		{
			if (ContainsPoint(Main.MouseScreen) && ContainedItem.type != 0)
			{
				Main.hoverItemName = ContainedItem.Name;
				Main.HoverItem = ContainedItem.Clone();
			}
			CalculatedStyle DrawRectangle = GetDimensions();
			Drawing.DrawAdvBox(sb, (int)DrawRectangle.X, (int)DrawRectangle.Y,
				(int)DrawRectangle.Width, (int)DrawRectangle.Height,
				DrawColor * Opacity, SlotBackTexture, CornerSize, Scale);
			if (ContainedItem.type != 0)
			{
				var frame = Main.itemAnimations[ContainedItem.type] != null ? Main.itemAnimations[ContainedItem.type].GetFrame(Main.itemTexture[ContainedItem.type]) : Main.itemTexture[ContainedItem.type].Frame(1, 1, 0, 0);
				var size = frame.Size();
				float texScale = 1f;
				if (size.X > DrawRectangle.Width || size.Y > DrawRectangle.Height)
				{
					texScale = size.X > size.Y ? size.X / DrawRectangle.Width : size.Y / DrawRectangle.Height;
					texScale = 0.7f / texScale;
					size *= texScale;
				}
				sb.Draw(Main.itemTexture[ContainedItem.type], new Vector2(DrawRectangle.X + DrawRectangle.Width / 2 - (size.X) / 2,
					DrawRectangle.Y + DrawRectangle.Height / 2 - (size.Y) / 2), new Rectangle?(frame), Color.White * Opacity, 0, Vector2.Zero, texScale * Scale, 0, 0);
				if (ContainedItem.stack > 1)
				{
					sb.DrawString(Main.fontMouseText, ContainedItem.stack.ToString(), new Vector2(DrawRectangle.X + 10, DrawRectangle.Y + DrawRectangle.Height - 20), Color.White * Opacity, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
				}
			}
		}
	}
}
