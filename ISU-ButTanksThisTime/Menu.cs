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
        public static void Update(Game1 game)
        {
            if (playBtn.Update())
            {
                Game1.State = State.Game;
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