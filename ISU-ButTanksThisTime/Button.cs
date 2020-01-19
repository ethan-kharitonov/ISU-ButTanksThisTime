using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_ButTanksThisTime
{
    internal class Button
    {
        private readonly Texture2D img;
        private Rectangle box;
        private string text;
        private Color color = Color.White;

        public Button(Texture2D img, Rectangle box, string text)
        {
            this.img = img;
            this.box = box;
            this.text = text;
        }

        public bool Update(bool active = true)
        {
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