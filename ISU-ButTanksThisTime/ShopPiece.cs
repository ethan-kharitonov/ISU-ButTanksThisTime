using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ISU_ButTanksThisTime.Cannons;

namespace ISU_ButTanksThisTime
{
    internal class ShopPiece
    {
        private Texture2D slot;
        private Rectangle slotBox;
        private CannonInfo rewardInfo;
        private int price;
        private Button button;

        public static readonly Vector2 Dimensions = new Vector2(200, 310);

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

        public bool Update()
        {
            var canBuy = GameScene.GetCurrentCredit() >= price;
            if (button.Update(canBuy))
            {
                GameScene.GivePlayerNewCannon(rewardInfo.cannon, price);
                button.ChangeText("ACTIVE");
                price = 0;
                return true;
            }

            return false;
        }

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

        public void Deactivate()
        {
            if (price == 0)
            {
                button.ChangeText("PURCHASED");
            }
        }
    }
}