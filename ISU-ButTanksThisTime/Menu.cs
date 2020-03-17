// Author        : Ethan Kharitonov
// File Name     : Menu.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : The main game menu.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// The main game menu.
    /// </summary>
    internal static class Menu
    {
        //store the background image of the menu
        private static readonly Texture2D bgImg;

        //store the play again, quite and instructions buttons
        private static readonly Button playBtn;
        private static readonly Button quitBtn;
        private static readonly Button instructionBtn;


        /// <summary>
        /// Initializes static members of the <see cref="Menu"/> class.
        /// </summary>
        static Menu()
        {
            //load the background image
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            //load the play again, quite and instructions buttons
            playBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width, 100, 2 * Tools.ButtonImg.Width, 2 * Tools.ButtonImg.Height), "PLAY");
            quitBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width / 2, 350, Tools.ButtonImg.Width, Tools.ButtonImg.Height), "QUIT");
            instructionBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width / 2, 250, Tools.ButtonImg.Width, Tools.ButtonImg.Height), "INSTRUCTIONS");
        }

        /// <summary>
        /// checks if any buttons are clicked and does the appropriate action
        /// </summary>
        /// <param name="game">The TankGame object.</param>
        public static void Update(TankGame game)
        {
            //chekc if play again button is pressed
            if (playBtn.Update())
            {
                //go to game scene
                TankGame.State = GameState.Game;
            }

            //chekc if quit button is pressed
            if (quitBtn.Update())
            {
                //Exit game
                game.Exit();
            }

            //chekc if instructions button is pressed
            if (instructionBtn.Update())
            {
                //go to instructions scene
                TankGame.State = GameState.Instructions;
            }
        }

        /// <summary>
        /// draws the menu scene
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //draws the background image
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White);

            //draws the buttons
            playBtn.Draw(spriteBatch);
            quitBtn.Draw(spriteBatch);
            instructionBtn.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}