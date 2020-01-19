using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    internal static class PauseMenu
    {
        private static Texture2D bgImg;
        private static Button backBtn;
        private static Button backToMenuBtn;

        static PauseMenu()
        {
            var btnImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/BTN BACK");

            backBtn = new Button(btnImg, new Rectangle(Tools.Screen.Center.X - 150, Tools.Screen.Center.Y - 75, 300, 150), "");
            backToMenuBtn = new Button(Tools.buttonImg, new Rectangle(Tools.Screen.Center.X - 75, 450, 150, 75), "BACK TO MENU");
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");
        }

        public static void Update()
        {
            if (backBtn.Update())
            {
                Game1.state = State.Game;
            }

            if (backToMenuBtn.Update())
            {
                GameScene.Reset();
                Game1.state = State.Menu;
            }
        }

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