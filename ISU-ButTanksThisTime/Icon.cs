// Author        : Ethan Kharitonov
// File Name     : Icon.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the Icon class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ISU_ButTanksThisTime.Collectibles;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class Icon.
    /// </summary>
    internal class Icon
    {
        private Rectangle frameBox;
        private readonly Texture2D img;
        private Color color = Color.White;
        private readonly Item item;

        private int quantity;

        public static readonly Texture2D Frame = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Bonus 03");

        /// <summary>
        /// Initializes a new instance of the <see cref="Icon"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="item">The item.</param>
        /// <param name="startingQuantity">The starting quantity.</param>
        public Icon(Vector2 position, Item item, int startingQuantity = 0)
        {
            this.item = item;
            frameBox = new Rectangle((int) position.X, (int) position.Y, Frame.Width, Frame.Height);
            img = item.Img;
            quantity = startingQuantity;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (!item.IsUseSupported)
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

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Frame, frameBox, color);

            var scaleRatio = ScaleImg(frameBox, img.Bounds);
            var iconBox = new Rectangle(frameBox.Center.X, frameBox.Center.Y, (int) (img.Width * scaleRatio), (int) (img.Height * scaleRatio));
            spriteBatch.Draw(img, iconBox, null, color, 0, new Vector2(img.Width * 0.5f, img.Height * 0.5f), SpriteEffects.None, 1);
            spriteBatch.DrawString(Tools.Font, "x" + quantity, new Vector2(iconBox.Right - 17, iconBox.Bottom - 33) - Tools.Font.MeasureString("$" + quantity), Color.White);
        }

        /// <summary>
        /// Adds the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void Add(int amount)
        {
            quantity += amount;
        }

        /// <summary>
        /// Scales the img.
        /// </summary>
        /// <param name="rec1">The rec1.</param>
        /// <param name="rec2">The rec2.</param>
        /// <returns>System.Single.</returns>
        private float ScaleImg(Rectangle rec1, Rectangle rec2)
        {
            var ratio1 = rec1.Width / (float) rec2.Width;
            var ratio2 = rec1.Height / (float) rec2.Height;

            return Math.Min(ratio1, ratio2);
        }

        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetQuantity() => quantity;

    }
}