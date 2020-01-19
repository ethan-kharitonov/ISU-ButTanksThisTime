using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    static class Menu
    {
        private static Texture2D bgImg;
        private static Button playBtn;

        static Menu()
        {
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            playBtn = new Button(Tools.buttonImg, new Rectangle(Tools.Screen.Center.X - Tools.buttonImg.Width, 100, 2 * Tools.buttonImg.Width, 2 * Tools.buttonImg.Height), "PLAY");
        }

        public static void Update()
        {
            if (playBtn.Update())
            {
                Game1.state = State.Game;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White);
            playBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
