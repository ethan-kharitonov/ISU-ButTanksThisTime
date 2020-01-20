// Author        : Ethan Kharitonov
// File Name     : Shop.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the shop where different cannons can be purchased.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ISU_ButTanksThisTime.Cannons;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Implements the shop where different cannons can be purchased.
    /// </summary>
    internal class Shop
    {
        /// <summary>
        /// Provides the singleton instance of the Shop
        /// </summary>
        public static Shop Instance { get; private set; } = new Shop();
        
        /// <summary>
        /// Resets the singleton instance of the Shop, called when the game is restarted
        /// </summary>
        

        //resets the shop
        public static void Reset() => Instance = new Shop();

        //stores the shop pieces
        private readonly ShopPiece[] pieces = new ShopPiece[4];

        //stores the back button
        private readonly Button backBtn;

        //stores the background image
        private readonly Texture2D bgImg;

        /// <summary>
        /// Initializes the <see cref="Shop"/> state.
        /// </summary>
        private Shop()
        {
            //loads the shop pieces
            var startXPos = (Tools.Screen.Width - 875) / 2;
            pieces[0] = new ShopPiece(BurstCannon.Info, new Vector2(startXPos, 100), 500);
            pieces[1] = new ShopPiece(TierFourCannon.Info, new Vector2(startXPos + (25 + ShopPiece.Dimensions.X), 100), 100);
            pieces[2] = new ShopPiece(MineDropperCannon.Info, new Vector2(startXPos + 2 * (25 + ShopPiece.Dimensions.X), 100), 250);
            pieces[3] = new ShopPiece(TierOneCannon.Info, new Vector2(startXPos + 3 * (25 + ShopPiece.Dimensions.X), 100), 0);

            //loads the background image
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");

            //loads bakc button image and button
            var backBtnImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/BTN BACK");
            backBtn = new Button(backBtnImg, new Rectangle(Tools.Screen.Center.X - backBtnImg.Width, 475, 2 * backBtnImg.Width, 2 * backBtnImg.Height), "");
        }

        /// <summary>
        /// update the shop piece
        /// </summary>
        public void Update()
        {
            //updates each shop piece
            foreach (var piece in pieces)
            {
                //if the shop piece is selected
                if (piece.Update())
                {
                    //make sure all other shop pieces are not selected
                    foreach (var pieceToDeactivate in pieces)
                    {
                        //checks if it is not the shop piece that is selected
                        if (!piece.Equals(pieceToDeactivate))
                        {
                            //deactivates the shop piece
                            pieceToDeactivate.Deactivate();
                        }
                    }
                }
            }

            //check if back button was pressed
            if (backBtn.Update())
            {
                //go back to game state
                TankGame.State = GameState.Game;
                
                //make the player leave the shop
                GameScene.MakePlayerLeaveShop();
            }
        }

        /// <summary>
        /// Draws the shop
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            //draws teh background image
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White * 0.50f);

            //draws each shop piece
            foreach (var piece in pieces)
            {
                //draws the shop piece
                piece.Draw(spriteBatch);
            }

            //draw the back button
            backBtn.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}