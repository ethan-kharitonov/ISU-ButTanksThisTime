// Author        : Ethan Kharitonov
// File Name     : Menu.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the Menu class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class Menu.
    /// </summary>
    internal static class Menu
    {
        private static readonly Texture2D bgImg;
        private static readonly Button playBtn;
        private static readonly Button quitBtn;

        /// <summary>
        /// Initializes static members of the <see cref="Menu"/> class.
        /// </summary>
        static Menu()
        {
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            playBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width, 100, 2 * Tools.ButtonImg.Width, 2 * Tools.ButtonImg.Height), "PLAY");
            quitBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width / 2, 300, Tools.ButtonImg.Width, Tools.ButtonImg.Height), "QUIT");
        }

        /// <summary>
        /// Updates the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        public static void Update(TankGame game)
        {
            if (playBtn.Update())
            {
                TankGame.State = GameState.Game;
            }

            if (quitBtn.Update())
            {
                game.Exit();
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White);
            playBtn.Draw(spriteBatch);
            quitBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}