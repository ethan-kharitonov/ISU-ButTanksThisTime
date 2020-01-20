// Author        : Ethan Kharitonov
// File Name     : ShopPiece.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the ShopPiece class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ISU_ButTanksThisTime.Cannons;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class ShopPiece.
    /// </summary>
    internal class ShopPiece
    {
        private readonly Texture2D slot;
        private Rectangle slotBox;
        private readonly CannonInfo rewardInfo;
        private int price;
        private readonly Button button;

        public static readonly Vector2 Dimensions = new Vector2(200, 310);

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopPiece"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="position">The position.</param>
        /// <param name="price">The price.</param>
        public ShopPiece(CannonInfo info, Vector2 position, int price)
        {
            this.price = price;
            rewardInfo = info;
            slot = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Inventory Cell");
            slotBox = new Rectangle((int) position.X, (int) position.Y, (int) Dimensions.X, (int) Dimensions.Y);

            var buttonImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Button BG shadow");
            button = new Button(buttonImg, new Rectangle(slotBox.Left + 5, slotBox.Bottom - 50, slotBox.Width - 15, 35), "$" + price);

            if (price == 0)
            {
                button.ChangeText("ACTIVE");
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Update()
        {
            var canBuy = GameScene.GetCurrentCredit() >= price;
            if (button.Update(canBuy))
            {
                GameScene.GivePlayerNewCannon(rewardInfo.Cannon, price);
                button.ChangeText("ACTIVE");
                price = 0;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(slot, slotBox, Color.White);

            spriteBatch.Draw(rewardInfo.Img, new Vector2((int) (slotBox.Left + slotBox.Width / 4.0), slotBox.Top + rewardInfo.Img.Height / 4f + 20), null, Color.White, 0, new Vector2(rewardInfo.Img.Width / 2f, rewardInfo.Img.Height / 2f), 0.5f, SpriteEffects.None, 1);
            spriteBatch.Draw(rewardInfo.Bullet.Img, new Vector2((int) (slotBox.Left + 3 * slotBox.Width / 4.0), slotBox.Top + rewardInfo.Img.Height / 4f + 10), null, Color.White, 0, new Vector2(rewardInfo.Bullet.Img.Width / 2f, rewardInfo.Bullet.Img.Height / 2f), 0.5f, SpriteEffects.None, 1);

            spriteBatch.DrawString(Tools.Font, "Fire Rate: " + rewardInfo.FireRate, new Vector2(slotBox.Left + 10, slotBox.Top + 133), Color.White);
            spriteBatch.DrawString(Tools.Font, "Rotation Speed: " + rewardInfo.RotationSpeed, new Vector2(slotBox.Left + 10, slotBox.Top + 158), Color.White);
            spriteBatch.DrawString(Tools.Font, "Burst Rate: " + (rewardInfo.BurstRate == null ? "N/A" : rewardInfo.BurstRate.Value.ToString()), new Vector2(slotBox.Left + 10, slotBox.Top + 183), Color.White);
            spriteBatch.DrawString(Tools.Font, "Burst Duration: " + (rewardInfo.BurstDuration == null ? "N/A" : rewardInfo.BurstDuration.Value.ToString()), new Vector2(slotBox.Left + 10, slotBox.Top + 208), Color.White);
            spriteBatch.DrawString(Tools.Font, "Bullet Damage: " + rewardInfo.Bullet.Damage, new Vector2(slotBox.Left + 10, slotBox.Top + 233), Color.White);

            button.Draw(spriteBatch);
        }

        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        public void Deactivate()
        {
            if (price == 0)
            {
                button.ChangeText("PURCHASED");
            }
        }
    }
}