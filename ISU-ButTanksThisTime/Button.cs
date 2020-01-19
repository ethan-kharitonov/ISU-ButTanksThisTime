using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    internal class Button
    {
        private Texture2D img;
        private Rectangle box;
        private string text;
        private Color color = Color.White;
        private bool active = true;

        public Button(Texture2D img, Rectangle box, string text)
        {
            this.img = img;
            this.box = box;
            this.text = text;
        }

        public bool Update(bool active = true)
        {
            this.active = active;
            if (!active)
            {
                color = Color.Red;
                return false;
            }

            if (box.Contains(Mouse.GetState().Position))
            {
                color = Color.LightGray;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    return true;
                }

                return false;
            }

            color = Color.White;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, box, color);
            spriteBatch.DrawString(Tools.Font, text, box.Center.ToVector2() - Tools.Font.MeasureString(text) / 2f, color);
        }

        public void ChangeText(string newText)
        {
            text = newText;
        }
    }
}