// Author        : Ethan Kharitonov
// File Name     : Button.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the Button class
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
        // The button image, rectangle, text and color
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
            //save the image, rectangle and text to member variables
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
            //check if button disabled
            if (!isEnabled)
            {
                //change color to red to indicate its disabled
                color = Color.Red;

                //return button is not pressed
                return false;
            }

            //check if the mouse is on the button
            if (box.Contains(Mouse.GetState().Position))
            {
                //change color to grey to indicate it can be pressed
                color = Color.LightGray;

                //return true if the mouse was pressed
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
            //draw the button and the text on th centre of the screen
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