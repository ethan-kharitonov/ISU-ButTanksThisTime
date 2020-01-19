using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ISU_ButTanksThisTime.Collectibles;

namespace ISU_ButTanksThisTime
{
    internal class Inventory
    {
        private readonly Dictionary<ItemType, Icon> icons = new Dictionary<ItemType, Icon>();

        public Inventory()
        {
            //Add re-locate icon
            var reLocateIcon = new Icon(new Vector2(Tools.Screen.Width - 10 - Icon.Frame.Width, 10), RelocateItem.VoidObject);
            icons.Add(ItemType.Relocate, reLocateIcon);

            //Add speed boost icon
            var speedBoostIcon = new Icon(new Vector2(Tools.Screen.Width - 2 * (10 + Icon.Frame.Width), 10), SpeedBoostItem.VoidObject);
            icons.Add(ItemType.SpeedBoost, speedBoostIcon);

            var coinIcon = new Icon(new Vector2(Tools.Screen.Width - 3 * (15 + Icon.Frame.Width) + 20, 10), CoinItem.VoidObject);
            icons.Add(ItemType.Coin, coinIcon);

            var ammoIcon = new Icon(new Vector2(Tools.Screen.Width - 4 * (15 + Icon.Frame.Width) + 20, 10), Ammo.VoidObject, 50);
            icons.Add(ItemType.Ammo, ammoIcon);
        }

        public void Update()
        {
            foreach (var icon in icons.Values)
            {
                icon.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var icon in icons.Values)
            {
                icon.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public void AddItem(Item item)
        {
            icons[item.ItemType].Add(item.Amount);
        }

        public int GetCurrentCredit() => icons[ItemType.Coin].GetQuantity();

        public int Pay(int price) => icons[ItemType.Coin].GetQuantity();

        public bool AreAnyBulletsLeft() => icons[ItemType.Ammo].GetQuantity() > 0;

        public void RemoveBullet()
        {
            icons[ItemType.Ammo].DecreaseQuantity(1);
        }
    }
}
