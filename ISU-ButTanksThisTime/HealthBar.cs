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
        //the bar image and the health bar image
        private readonly Texture2D barImg;
        private readonly Texture2D healthImg;

        //the bar and health bar rectangles
        private Rectangle barBox;
        private Rectangle healthBox;

        //the bar distance above its tank and the bar length
        private const int DIS_ABOVE_TANK = 40;
        private const int BAR_LENGTH = 85;

        //the maximum health
        private readonly float maxHealth;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthBar"/> class.
        /// </summary>
        /// <param name="startHealth">The starting health.</param>
        /// <param name="position">The inital position.</param>
        public HealthBar(int startHealth, Vector2 position)
        {
            //set starting health as max health
            maxHealth = startHealth;

            //load bar and health bar images
            barImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Loading Bar BG");
            healthImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Loading Bar");
            
            //set the rectangles for both bars
            barBox = new Rectangle((int) position.X, (int) position.Y, BAR_LENGTH, 8);
            healthBox = barBox;
        }

        /// <summary>
        /// updates the position and the length of the health bar
        /// </summary>
        /// <param name="tankPos">The tank position.</param>
        /// <param name="health">The tank health.</param>
        public void Update(Vector2 tankPos, float health)
        {
            //updates the position of the bar
            barBox.X = (int) tankPos.X;
            barBox.Y = (int) tankPos.Y - DIS_ABOVE_TANK;

            //updates the position of the health bar
            healthBox.X = barBox.X - barBox.Width / 2;
            healthBox.Y = barBox.Y;

            //updates the length of the health bar based on the health
            healthBox.Width = (int) (health / maxHealth * BAR_LENGTH) - 2;
            healthBox.Height = barBox.Height;
        }

        /// <summary>
        /// Draws the health bar
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //draws both bars
            spriteBatch.Draw(barImg, barBox, null, Color.White, 0, new Vector2(barImg.Width * 0.5f, barImg.Height * 0.5f), SpriteEffects.None, 1f);
            spriteBatch.Draw(healthImg, healthBox, null, Color.White, 0, new Vector2(0, healthImg.Height * 0.5f), SpriteEffects.None, 1f);
        }
    }
}