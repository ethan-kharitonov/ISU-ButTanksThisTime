using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    internal static class LoseScreen
    {
        private static Texture2D bgImg;
        private static Button BackToMenuBtn;
        private static Button playAgainBtn;

        static LoseScreen()
        {
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            playAgainBtn = new Button(Tools.buttonImg, new Rectangle(Tools.Screen.Center.X - Tools.buttonImg.Width * 2, 100, 4 * Tools.buttonImg.Width, 3 * Tools.buttonImg.Height), "PLAY AGAIN");
            BackToMenuBtn = new Button(Tools.buttonImg, new Rectangle(Tools.Screen.Center.X - Tools.buttonImg.Width, 400, 2 * Tools.buttonImg.Width, 2 * Tools.buttonImg.Height), "BACK TO MENU");
        }

        public static void Update()
        {
            if (playAgainBtn.Update())
            {
                Game1.state = State.Game;
                GameScene.Reset();
            }
            else if (BackToMenuBtn.Update())
            {
                Game1.state = State.Menu;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.75f);
            playAgainBtn.Draw(spriteBatch);
            BackToMenuBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}