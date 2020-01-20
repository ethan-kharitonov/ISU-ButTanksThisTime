// Author        : Ethan Kharitonov
// File Name     : Collectibles\CoinItem.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the CoinItem class.
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    /// <summary>
    /// The Coin collectible item.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" /> abstract type.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Collecting this item grants the player more money.
    /// </remarks>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class CoinItem : Item
    {
        //The amount of this item the player recives when picking it up
        private const int AMOUNT = 10;
    
        /// <summary>
        /// Used to access the attributes of a collectible item that are the same for any instance of the same type, like <see cref="Img"/> or <see cref="IsUseSupported"/>.
        /// </summary>
        public static readonly Item VoidObject = new CoinItem(default);

        private static readonly Texture2D img;

        /// <summary>
        /// Initializes static members of the <see cref="CoinItem"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static CoinItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Coin_A");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinItem"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="amount">The amount of coins to be rewarded - the default is 10.</param>
        public CoinItem(Vector2 position, int amount = AMOUNT) : base(position, amount)
        {
        }

        /// <summary>
        /// Overrides the base <see cref="Item.Img"/> property.
        /// </summary>
        /// <value>The Coin object image.</value>
        public override Texture2D Img => img;

        /// <summary>
        /// Overrides the base <see cref="Item.IsUseSupported"/> property.
        /// </summary>
        /// <value><c>false</c></value>
        public override bool IsUseSupported => false;

        /// <summary>
        /// Overrides the base <see cref="Item.ItemType"/> property.
        /// </summary>
        /// <value><c>ItemType.Coin</c></value>
        public override ItemType ItemType => ItemType.Coin;

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public override void Use() => throw new NotSupportedException();
    }
}