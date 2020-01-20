// Author        : Ethan Kharitonov
// File Name     : HealthBar.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the HealthBar class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class HealthBar.
    /// </summary>
    internal class HealthBar
    {
        private readonly Texture2D barImg;
        private Rectangle barBox;
        private const int DIS_ABOVE_TANK = 40;
        private readonly Texture2D healthImg;
        private const int BAR_LENGTH = 85;
        private Rectangle healthBox;

        private readonly float maxHealth;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthBar"/> class.
        /// </summary>
        /// <param name="startHealth">The start health.</param>
        /// <param name="position">The position.</param>
        public HealthBar(int startHealth, Vector2 position)
        {
            maxHealth = startHealth;
            barImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Loading Bar BG");
            barBox = new Rectangle((int) position.X, (int) position.Y, BAR_LENGTH, 8);

            healthImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Loading Bar");
            healthBox = barBox;
        }

        /// <summary>
        /// Updates the specified tank position.
        /// </summary>
        /// <param name="tankPos">The tank position.</param>
        /// <param name="health">The health.</param>
        public void Update(Vector2 tankPos, float health)
        {
            barBox.X = (int) tankPos.X;
            barBox.Y = (int) tankPos.Y - DIS_ABOVE_TANK;

            healthBox.X = barBox.X - barBox.Width / 2;
            healthBox.Y = barBox.Y;

            //healthBox = barBox;
            healthBox.Width = (int) (health / maxHealth * BAR_LENGTH) - 2;
            healthBox.Height = barBox.Height;
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barImg, barBox, null, Color.White, 0, new Vector2(barImg.Width * 0.5f, barImg.Height * 0.5f), SpriteEffects.None, 1f);
            spriteBatch.Draw(healthImg, healthBox, null, Color.White, 0, new Vector2(0, healthImg.Height * 0.5f), SpriteEffects.None, 1f);


            //spriteBatch.Draw(healthImg, healthBox, Color.White);
        }
    }
}