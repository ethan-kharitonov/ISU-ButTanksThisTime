// Author        : Ethan Kharitonov
// File Name     : PauseMenu.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : The menu open when the game is paused.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// The menu open when the game is paused.
    /// </summary>
    internal static class PauseMenu
    {
        //store the background image
        private static readonly Texture2D bgImg;

        //store the back to game and back to menu buttons
        private static readonly Button backToGameBtn;
        private static readonly Button backToMenuBtn;

        /// <summary>
        /// Initializes static members of the <see cref="PauseMenu"/> class.
        /// </summary>
        static PauseMenu()
        {
            //load the back button image
            var btnImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/BTN BACK");

            //load the back to game and back to menu buttons
            backToGameBtn = new Button(btnImg, new Rectangle(Tools.Screen.Center.X - 150, Tools.Screen.Center.Y - 75, 300, 150), "");
            backToMenuBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - 75, 450, 150, 75), "BACK TO MENU");
           
            //load the background image
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");
        }

        /// <summary>
        /// checks if any buttons are clicked and does the appropriate action
        /// </summary>
        public static void Update()
        {
            //check if back to game button was clicked
            if (backToGameBtn.Update())
            {
                //go to game scene
                TankGame.State = GameState.Game;
            }

            //check if bakc to menu button was clicked
            if (backToMenuBtn.Update())
            {
                //reset the game and the shop
                GameScene.Reset();
                Shop.Reset();

                //go to game scene
                TankGame.State = GameState.Menu;
            }
        }

        /// <summary>
        /// draws the pause menu.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //draws the background
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.35f);

            //draws the buttons
            backToGameBtn.Draw(spriteBatch);
            backToMenuBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}