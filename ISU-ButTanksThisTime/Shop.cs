using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ISU_ButTanksThisTime.Cannons;

namespace ISU_ButTanksThisTime
{
    internal static class Shop
    {
        private static readonly ShopPiece[] pieces = new ShopPiece[4];
        private static readonly Button backBtn;

        private static readonly Texture2D bgImg;

        static Shop()
        {
            var startXPos = (Tools.Screen.Width - 875) / 2;
            pieces[0] = new ShopPiece(BurstCannon.Info, new Vector2(startXPos, 100), 500);
            pieces[1] = new ShopPiece(TierFourCannon.Info, new Vector2(startXPos + (25 + ShopPiece.Dimensions.X), 100), 100);
            pieces[2] = new ShopPiece(MineDropperCannon.Info, new Vector2(startXPos + 2 * (25 + ShopPiece.Dimensions.X), 100), 250);
            pieces[3] = new ShopPiece(TierOneCannon.Info, new Vector2(startXPos + 3 * (25 + ShopPiece.Dimensions.X), 100), 0);

            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            var backBtnImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/BTN BACK");
            backBtn = new Button(backBtnImg, new Rectangle(Tools.Screen.Center.X - backBtnImg.Width, 475, 2 * backBtnImg.Width, 2 * backBtnImg.Height), "");
        }

        public static void Update()
        {
            foreach (var piece in pieces)
            {
                if (piece.Update())
                {
                    foreach (var pieceToDeactivate in pieces)
                    {
                        if (!piece.Equals(pieceToDeactivate))
                        {
                            pieceToDeactivate.Deactivate();
                        }
                    }
                }
            }

            if (backBtn.Update())
            {
                Game1.State = State.Game;
                GameScene.MakePlayerLeaveShop();
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.50f);
            foreach (var piece in pieces)
            {
                piece.Draw(spriteBatch);
            }

            backBtn.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}