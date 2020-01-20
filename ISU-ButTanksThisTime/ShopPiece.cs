// Author        : Ethan Kharitonov
// File Name     : ShopPiece.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements pieces that can be purchased in the shop. Each piece is associated with a different cannon type.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ISU_ButTanksThisTime.Cannons;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Implements pieces that can be purchased in the shop. Each piece is associated with a different cannon type.
    /// </summary>
    internal class ShopPiece
    {
        //the image and rectangle of the background of the shop piece
        private readonly Texture2D slot;
        private Rectangle slotBox;

        //stores the inormation displayed on the shop piece
        private readonly CannonInfo rewardInfo;

        //stores teh price to purchase the item on the shop piece
        private int price;

        //stores the button that buys the item on the piece
        private readonly Button buyBtn;

        //stores teh dimensions of the piece (width and height)
        public static readonly Vector2 Dimensions = new Vector2(200, 310);

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopPiece"/> class.
        /// </summary>
        /// <param name="info">The displayed information.</param>
        /// <param name="position">The position.</param>
        /// <param name="price">The price.</param>
        public ShopPiece(CannonInfo info, Vector2 position, int price)
        {
            //save the price and info to a member variable
            this.price = price;
            rewardInfo = info;
            
            //load the background image and rectangle
            slot = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Inventory Cell");
            slotBox = new Rectangle((int) position.X, (int) position.Y, (int) Dimensions.X, (int) Dimensions.Y);

            //create the buy button
            var buttonImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Button BG shadow");
            buyBtn = new Button(buttonImg, new Rectangle(slotBox.Left + 5, slotBox.Bottom - 50, slotBox.Width - 15, 35), "$" + price);

            //if the price is initally zero then the item is initally active
            if (price == 0)
            {
                //change the text on the button to "ACTIVE"
                buyBtn.ChangeText("ACTIVE");
            }
        }

        /// <summary>
        /// checks if a purhcase has been made
        /// </summary>
        /// <returns><c>true</c> if a purchase has been made, <c>false</c> otherwise.</returns>
        public bool Update()
        {
            //stores if the player can afford what is being sold
            var canBuy = GameScene.GetCurrentCredit() >= price;

            //checks if the buy button is pressed with the canBuy bool
            if (buyBtn.Update(canBuy))
            {
                //gives player the cannon in the piece
                GameScene.GivePlayerNewCannon(rewardInfo.Cannon, price);

                //sets the text to active and sets price to 0
                buyBtn.ChangeText("ACTIVE");
                price = 0;

                //returns that a purchase has been made
                return true;
            }

            //returns that a purchase has not been made
            return false;
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //draws the piece background image
            spriteBatch.Draw(slot, slotBox, Color.White);

            //draws the cannon and bullet image fromm the info
            spriteBatch.Draw(rewardInfo.Img, new Vector2((int) (slotBox.Left + slotBox.Width / 4.0), slotBox.Top + rewardInfo.Img.Height / 4f + 20), null, Color.White, 0, new Vector2(rewardInfo.Img.Width / 2f, rewardInfo.Img.Height / 2f), 0.5f, SpriteEffects.None, 1);
            spriteBatch.Draw(rewardInfo.Bullet.Img, new Vector2((int) (slotBox.Left + 3 * slotBox.Width / 4.0), slotBox.Top + rewardInfo.Img.Height / 4f + 10), null, Color.White, 0, new Vector2(rewardInfo.Bullet.Img.Width / 2f, rewardInfo.Bullet.Img.Height / 2f), 0.5f, SpriteEffects.None, 1);

            //draws all the stats from info
            spriteBatch.DrawString(Tools.Font, "Fire Rate: " + rewardInfo.FireRate, new Vector2(slotBox.Left + 10, slotBox.Top + 133), Color.White);
            spriteBatch.DrawString(Tools.Font, "Rotation Speed: " + rewardInfo.RotationSpeed, new Vector2(slotBox.Left + 10, slotBox.Top + 158), Color.White);
            spriteBatch.DrawString(Tools.Font, "Burst Rate: " + (rewardInfo.BurstRate == null ? "N/A" : rewardInfo.BurstRate.Value.ToString()), new Vector2(slotBox.Left + 10, slotBox.Top + 183), Color.White);
            spriteBatch.DrawString(Tools.Font, "Burst Duration: " + (rewardInfo.BurstDuration == null ? "N/A" : rewardInfo.BurstDuration.Value.ToString()), new Vector2(slotBox.Left + 10, slotBox.Top + 208), Color.White);
            spriteBatch.DrawString(Tools.Font, "Bullet Damage: " + rewardInfo.Bullet.Damage, new Vector2(slotBox.Left + 10, slotBox.Top + 233), Color.White);

            buyBtn.Draw(spriteBatch);
        }

        /// <summary>
        /// changes the text from "ACTIVE" to "PURCHASED" if needed
        /// </summary>
        public void Deactivate()
        {
            //if this item is purchased
            if (price == 0)
            {
                //set text to "PURCHASED"
                buyBtn.ChangeText("PURCHASED");
            }
        }
    }
}