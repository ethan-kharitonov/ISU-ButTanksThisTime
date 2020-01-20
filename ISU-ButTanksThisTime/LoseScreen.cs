// Author        : Ethan Kharitonov
// File Name     : LoseScreen.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the LoseScreen class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Implements the screen shown when the game is over
    /// </summary>
    internal static class LoseScreen
    {
        private static readonly Texture2D bgImg;
        private static readonly Button backToMenuBtn;
        private static readonly Button playAgainBtn;

        /// <summary>
        /// Initializes static members of the <see cref="LoseScreen"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static LoseScreen()
        {
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            playAgainBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width * 2, 100, 4 * Tools.ButtonImg.Width, 3 * Tools.ButtonImg.Height), "PLAY AGAIN");
            backToMenuBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width, 400, 2 * Tools.ButtonImg.Width, 2 * Tools.ButtonImg.Height), "BACK TO MENU");
        }

        /// <summary>
        /// Updates the state from the user input.
        /// </summary>
        public static void Update()
        {
            if (playAgainBtn.Update())
            {
                TankGame.State = GameState.Game;
                GameScene.Reset();
                Shop.Reset();
            }
            else if (backToMenuBtn.Update())
            {
                TankGame.State = GameState.Menu;
                Shop.Reset();
            }
        }

        /// <summary>
        /// Draws the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.75f);
            playAgainBtn.Draw(spriteBatch);
            backToMenuBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}