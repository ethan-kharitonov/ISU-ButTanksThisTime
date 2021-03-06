﻿// Author        : Ethan Kharitonov
// File Name     : Collectibles\CoinPileItem.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the Coin Pile collectible item.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    /// <summary>
    /// Implements the Coin Pile collectible item.
    /// <para>
    /// Refines the <see cref="ISU_ButTanksThisTime.Collectibles.CoinItem" /> type.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Collecting this item grants the player a pile of money.
    /// </remarks>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.CoinItem" />
    internal class CoinPileItem : CoinItem
    {
        //The amount of this item the player recives when picking it up
        private const int AMOUNT = 25;

        //stores the images of this item
        private static readonly Texture2D img;

        /// <summary>
        /// Initializes static members of the <see cref="CoinPileItem"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static CoinPileItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Coin_B");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinPileItem"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <remarks>
        /// Grants 25 coins.
        /// </remarks>
        public CoinPileItem(Vector2 position) : base(position, AMOUNT)
        {
        }

        /// <summary>
        /// Overrides the base <see cref="Item.Img"/> property.
        /// </summary>
        /// <value>The Coin Pile object image.</value>
        public override Texture2D Img => img;
    }
}