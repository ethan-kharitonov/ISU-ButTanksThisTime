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
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    /// <summary>
    /// Class CoinItem.
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class CoinItem : Item
    {
        public static readonly Item VoidObject = new CoinItem(default);

        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        /// <summary>
        /// Initializes static members of the <see cref="CoinItem"/> class.
        /// </summary>
        static CoinItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Coin_A");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinItem"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="amount">The amount.</param>
        public CoinItem(Vector2 position, int amount = 10) : base(position, amount)
        {
        }

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        public override Texture2D Img => img;

        /// <summary>
        /// Gets a value indicating whether this <see cref="CoinItem"/> is usable.
        /// </summary>
        /// <value><c>true</c> if usable; otherwise, <c>false</c>.</value>
        public override bool Usable => false;

        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <value>The type of the item.</value>
        public override ItemType ItemType => ItemType.Coin;

        /// <summary>
        /// Uses this instance.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public override void Use()
        {
            throw new NotSupportedException();
        }
    }
}