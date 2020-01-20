// Author        : Ethan Kharitonov
// File Name     : LoseScreen.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the screen shown when the game is over
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Implements the screen shown when the game is over
    /// </summary>
    internal static class LoseScreen
    {
        //stores the bacgkround image
        private static readonly Texture2D bgImg;

        //stores the back to menu and play again buttons
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
            //loads the background image
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            //loads the back to menu and play again buttons
            playAgainBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width * 2, 100, 4 * Tools.ButtonImg.Width, 3 * Tools.ButtonImg.Height), "PLAY AGAIN");
            backToMenuBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width, 400, 2 * Tools.ButtonImg.Width, 2 * Tools.ButtonImg.Height), "BACK TO MENU");
        }

        /// <summary>
        /// Updates the state from the user input.
        /// </summary>
        public static void Update()
        {
            //check wether play again button was clicked
            if (playAgainBtn.Update())
            {
                //go to game scene
                TankGame.State = GameState.Game;

                //reset game and shop scene
                GameScene.Reset();
                Shop.Reset();
            }
            
            //chekc if back to menu button is pressed
            if (backToMenuBtn.Update())
            {
                //go to menu scene
                TankGame.State = GameState.Menu;

                //reset game and shop scene
                GameScene.Reset();
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

            //draw the background
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.75f);

            //draw both buttons
            playAgainBtn.Draw(spriteBatch);
            backToMenuBtn.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}