using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    internal static class PauseMenu
    {
        private static readonly Texture2D bgImg;
        private static readonly Button backBtn;
        private static readonly Button backToMenuBtn;

        static PauseMenu()
        {
            var btnImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/BTN BACK");

            backBtn = new Button(btnImg, new Rectangle(Tools.Screen.Center.X - 150, Tools.Screen.Center.Y - 75, 300, 150), "");
            backToMenuBtn = new Button(Tools.ButtonImg, new Rectangle(Tools.Screen.Center.X - 75, 450, 150, 75), "BACK TO MENU");
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");
        }

        public static void Update()
        {
            if (backBtn.Update())
            {
                Game1.State = State.Game;
            }

            if (backToMenuBtn.Update())
            {
                GameScene.Reset();
                Game1.State = State.Menu;
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