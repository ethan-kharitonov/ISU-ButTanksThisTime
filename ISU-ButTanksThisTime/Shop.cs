// Author        : Ethan Kharitonov
// File Name     : Shop.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the Shop class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ISU_ButTanksThisTime.Cannons;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// a game state where the player could buy different cannons
    /// </summary>
    internal class Shop
    {
        public static Shop Instance { get; private set; } = new Shop();
        public static void Reset() => Instance = new Shop();

        private readonly ShopPiece[] pieces = new ShopPiece[4];
        private readonly Button backBtn;

        private readonly Texture2D bgImg;

        /// <summary>
        /// Initializes the <see cref="Shop"/> state.
        /// </summary>
        private Shop()
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

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
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
                TankGame.State = GameState.Game;
                GameScene.MakePlayerLeaveShop();
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
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