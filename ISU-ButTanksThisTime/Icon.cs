using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class Icon
    {
        private Rectangle frameBox;
        private Texture2D img;
        private Color color = Color.White;
        private Item item;

        private int quantity;

        public static readonly Texture2D Frame = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Bonus 03");
        public Icon(Vector2 position, Item item, int startingQuantity = 0)
        {
            this.item = item;
            frameBox = new Rectangle((int)position.X, (int)position.Y, Frame.Width, Frame.Height);
            img = item.Img;
            quantity = startingQuantity;
        }

        public void Update()
        {
            if (!item.Usable)
            {
                return;
            }

            if (frameBox.Contains(Mouse.GetState().Position.ToVector2()))
            {
                color = Color.LightGray;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && quantity > 0)
                {
                    item.Use();
                    --quantity;
                }
            }
            else
            {
                color = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Frame, frameBox, color);

            float scaleRatio = ScaleImg(frameBox, img.Bounds);
            Rectangle iconBox = new Rectangle(frameBox.Center.X, frameBox.Center.Y, (int)(img.Width * scaleRatio), (int)(img.Height * scaleRatio));
            spriteBatch.Draw(img, iconBox, null, color, 0, new Vector2(img.Width * 0.5f, img.Height * 0.5f), SpriteEffects.None, 1);
            spriteBatch.DrawString(Tools.Font, "x" + quantity, new Vector2(iconBox.Right - 17, iconBox.Bottom - 33) - Tools.Font.MeasureString("$" + quantity), Color.White);
        
        }

        public void Add(int amount) => quantity += amount;

        private float ScaleImg(Rectangle rec1, Rectangle rec2)
        {
            float ratio1 = rec1.Width / (float)rec2.Width;
            float ratio2 = rec1.Height / (float)rec2.Height;

            return Math.Min(ratio1, ratio2);
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public void DecreaseQuantity(int amount)
        {
            quantity -= amount;
        }

    }
}
