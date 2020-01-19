using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    internal static class Menu
    {
        private static readonly Texture2D bgImg;
        private static readonly Button playBtn;
        private static readonly Button quitBtn;

        static Menu()
        {
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            playBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width, 100, 2 * Tools.ButtonImg.Width, 2 * Tools.ButtonImg.Height), "PLAY");
            quitBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - Tools.ButtonImg.Width / 2, 300, Tools.ButtonImg.Width, Tools.ButtonImg.Height), "QUIT");
        }

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