// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-19-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ISU_ButTanksThisTime.Collectibles;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class Inventory.
    /// </summary>
    internal class Inventory
    {
        private readonly Dictionary<ItemType, Icon> icons = new Dictionary<ItemType, Icon>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Inventory"/> class.
        /// </summary>
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

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            foreach (var icon in icons.Values)
            {
                icon.Update();
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var icon in icons.Values)
            {
                icon.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(Item item)
        {
            icons[item.ItemType].Add(item.Amount);
        }

        /// <summary>
        /// Gets the current credit.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetCurrentCredit() => icons[ItemType.Coin].GetQuantity();

        /// <summary>
        /// pays a certain amount of coins
        /// </summary>
        /// <param name="price">the amount to pay</param>
        public void Pay(int price) => icons[ItemType.Coin].Add(-price);


        /// <summary>
        /// Ares any bullets left.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AreAnyBulletsLeft() => icons[ItemType.Ammo].GetQuantity() > 0;

        /// <summary>
        /// Removes the bullet.
        /// </summary>
        public void RemoveBullet()
        {
            icons[ItemType.Ammo].Add(-1);
        }
    }
}