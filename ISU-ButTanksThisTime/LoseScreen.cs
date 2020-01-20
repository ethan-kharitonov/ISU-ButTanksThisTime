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

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class LoseScreen.
    /// </summary>
    internal static class LoseScreen
    {
        private static readonly Texture2D bgImg;
        private static readonly Button backToMenuBtn;
        private static readonly Button playAgainBtn;

        /// <summary>
        /// Initializes static members of the <see cref="LoseScreen"/> class.
        /// </summary>
        static LoseScreen()
        {
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            playAgainBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width * 2, 100, 4 * Tools.ButtonImg.Width, 3 * Tools.ButtonImg.Height), "PLAY AGAIN");
            backToMenuBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width, 400, 2 * Tools.ButtonImg.Width, 2 * Tools.ButtonImg.Height), "BACK TO MENU");
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public static void Update()
        {
            if (playAgainBtn.Update())
            {
                Game1.State = State.Game;
                GameScene.Reset();
                Shop.Reset();
            }
            else if (backToMenuBtn.Update())
            {
                Game1.State = State.Menu;
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
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.75f);
            playAgainBtn.Draw(spriteBatch);
            backToMenuBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}