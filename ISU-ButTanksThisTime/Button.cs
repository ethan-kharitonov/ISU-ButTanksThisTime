// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-19-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class Button.
    /// </summary>
    internal class Button
    {
        private readonly Texture2D img;
        private Rectangle box;
        private string text;
        private Color color = Color.White;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="box">The box.</param>
        /// <param name="text">The text.</param>
        public Button(Texture2D img, Rectangle box, string text)
        {
            this.img = img;
            this.box = box;
            this.text = text;
        }

        /// <summary>
        /// Updates the specified active.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, box, color);
            spriteBatch.DrawString(Tools.Font, text, box.Center.ToVector2() - Tools.Font.MeasureString(text) / 2f, color);
        }

        /// <summary>
        /// Changes the text.
        /// </summary>
        /// <param name="newText">The new text.</param>
        public void ChangeText(string newText)
        {
            text = newText;
        }
    }
}