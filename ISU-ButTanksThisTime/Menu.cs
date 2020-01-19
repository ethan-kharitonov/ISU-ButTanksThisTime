using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    internal static class Menu
    {
        private static Texture2D bgImg;
        private static Button playBtn;
        private static Button quitBtn;

        static Menu()
        {
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            playBtn = new Button(Tools.buttonImg, new Rectangle(Tools.Screen.Center.X - Tools.buttonImg.Width, 100, 2 * Tools.buttonImg.Width, 2 * Tools.buttonImg.Height), "PLAY");
            quitBtn = new Button(Tools.buttonImg, new Rectangle(Tools.Screen.Center.X - Tools.buttonImg.Width / 2, 300, Tools.buttonImg.Width, Tools.buttonImg.Height), "QUIT");
        }

        public static void Update(Game1 game)
        {
            if (playBtn.Update())
            {
                Game1.state = State.Game;
            }

            if (quitBtn.Update())
            {
                game.Exit();
            }
        }

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