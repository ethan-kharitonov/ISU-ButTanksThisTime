// Author        : Ethan Kharitonov
// File Name     : Inventory.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the Inventory class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ISU_ButTanksThisTime.Collectibles;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// stores the items the player has collected.
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

            //add coin icon
            var coinIcon = new Icon(new Vector2(Tools.Screen.Width - 3 * (15 + Icon.Frame.Width) + 20, 10), CoinItem.VoidObject);
            icons.Add(ItemType.Coin, coinIcon);

            //add ammo icon
            var ammoIcon = new Icon(new Vector2(Tools.Screen.Width - 4 * (15 + Icon.Frame.Width) + 20, 10), Ammo.VoidObject, 50);
            icons.Add(ItemType.Ammo, ammoIcon);
        }

        /// <summary>
        /// Updates the icons in the inventory.
        /// </summary>
        public void Update()
        {
            //updates each icon
            foreach (var icon in icons.Values)
            {
                //updates the icon
                icon.Update();
            }
        }

        /// <summary>
        /// draws each icon
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //separate spriteBatch so the are always on screen
            spriteBatch.Begin();
            
            //draw each icon
            foreach (var icon in icons.Values)
            {
                icon.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            //adds to the specified item
            icons[item.ItemType].Add(item.Amount);
        }

        /// <summary>
        /// Gets the current credit.
        /// </summary>
        /// <returns>The number of coins = the player has</returns>
        public int GetCurrentCredit() => icons[ItemType.Coin].GetQuantity();

        /// <summary>
        /// pays a certain amount of coins
        /// </summary>
        /// <param name="price">the amount to pay</param>
        public void Pay(int price) => icons[ItemType.Coin].Add(-price);


        /// <summary>
        /// checks if the player has ammo left.
        /// </summary>
        /// <returns><c>true</c> if ammo above zero, <c>false</c> otherwise.</returns>
        public bool AreAnyBulletsLeft() => icons[ItemType.Ammo].GetQuantity() > 0;

        /// <summary>
        /// Removes one bullet from ammo.
        /// </summary>
        public void RemoveBullet()
        {
            icons[ItemType.Ammo].Add(-1);
        }
    }
}