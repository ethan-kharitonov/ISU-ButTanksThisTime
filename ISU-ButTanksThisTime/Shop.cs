using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    static class Shop
    {
        private static Texture2D slot;
        private static Rectangle slotBox;
        private static CannonInfo rewardInfo = BurstCannon.Info;
        private static int price;
        static Shop()
        {
            slot = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Inventory Cell");
            slotBox = new Rectangle(100, 100, 150, 200);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(slot, slotBox, Color.White);
            spriteBatch.Draw(rewardInfo.Img, new Vector2(slotBox.Center.X, slotBox.Top + 20), null, Color.White, 0, new Vector2(rewardInfo.Img.Width / 2f, rewardInfo.Img.Height / 2f), 1, SpriteEffects.None, 1);
            spriteBatch.End();
        }
    }
}
