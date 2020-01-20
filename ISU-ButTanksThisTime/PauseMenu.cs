// Author        : Ethan Kharitonov
// File Name     : PauseMenu.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the PauseMenu class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class PauseMenu.
    /// </summary>
    internal static class PauseMenu
    {
        private static readonly Texture2D bgImg;
        private static readonly Button backBtn;
        private static readonly Button backToMenuBtn;

        /// <summary>
        /// Initializes static members of the <see cref="PauseMenu"/> class.
        /// </summary>
        static PauseMenu()
        {
            var btnImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/BTN BACK");

            backBtn = new Button(btnImg, new Rectangle(Tools.Screen.Center.X - 150, Tools.Screen.Center.Y - 75, 300, 150), "");
            backToMenuBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - 75, 450, 150, 75), "BACK TO MENU");
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public static void Update()
        {
            if (backBtn.Update())
            {
                TankGame.State = GameState.Game;
            }

            if (backToMenuBtn.Update())
            {
                GameScene.Reset();
                TankGame.State = GameState.Menu;
                Shop.Reset();
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.35f);
            backBtn.Draw(spriteBatch);
            backToMenuBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}