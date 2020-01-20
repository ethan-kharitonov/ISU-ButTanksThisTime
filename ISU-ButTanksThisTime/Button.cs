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
    /// Represents the game button objects.
    /// </summary>
    internal class Button
    {
        // The button properties, like color, text, box and image.
        private readonly Texture2D img;
        private Rectangle box;
        private string text;
        private Color color = Color.White;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="img">The button image.</param>
        /// <param name="box">The button box.</param>
        /// <param name="text">The button text.</param>
        public Button(Texture2D img, Rectangle box, string text)
        {
            this.img = img;
            this.box = box;
            this.text = text;
        }

        /// <summary>
        /// Updates the state of the button.
        /// </summary>
        /// <param name="isEnabled">Indicates whether the button is enabled.</param>
        /// <returns><c>true</c> if pressed, <c>false</c> otherwise.</returns>
        public bool Update(bool isEnabled = true)
        {
            if (!isEnabled)
            {
                // disabled buttons cannot be pressed
                color = Color.Red;
                return false;
            }

            if (box.Contains(Mouse.GetState().Position))
            {
                // pressed if the mouse inside and the left button is clicked
                color = Color.LightGray;
                return Mouse.GetState().LeftButton == ButtonState.Pressed;
            }

            // enabled but not pressed
            color = Color.White;
            return false;
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, box, color);
            spriteBatch.DrawString(Tools.Font, text, box.Center.ToVector2() - Tools.Font.MeasureString(text) / 2f, color);
        }

        /// <summary>
        /// Changes the button text.
        /// </summary>
        /// <param name="newText">The new text.</param>
        public void ChangeText(string newText) => text = newText;
    }
}