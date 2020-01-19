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
    /// The Ammo collectible item.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" /> abstract type.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Collecting this item grants the player more ammunition.
    /// </remarks>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class Ammo : Item
    {
        private const int AMOUNT = 15;

        /// <summary>
        /// Used to access the attributes of a collectible item that are the same for any instance of the same type, like <see cref="Img"/> or <see cref="IsUseSupported"/>.
        /// </summary>
        public static readonly Item VoidObject = new Ammo(default);

        private static readonly Texture2D img;

        /// <summary>
        /// Initializes static members of the <see cref="Ammo"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static Ammo() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Shotgun_Shells");

        /// <summary>
        /// Initializes a new instance of the <see cref="Ammo"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <remarks>
        /// Grants 15 bullets.
        /// </remarks>
        public Ammo(Vector2 position) : base(position, AMOUNT)
        {
        }

        /// <summary>
        /// Overrides the base <see cref="Item.Img"/> property.
        /// </summary>
        /// <value>The Ammo object image.</value>
        public override Texture2D Img => img;

        /// <summary>
        /// Overrides the base <see cref="Item.ItemType"/> property.
        /// </summary>
        /// <value><c>ItemType.Ammo</c></value>
        public override ItemType ItemType => ItemType.Ammo;

        /// <summary>
        /// Overrides the base <see cref="Item.IsUseSupported"/> property.
        /// </summary>
        /// <value><c>false</c></value>
        public override bool IsUseSupported => false;

        /// <summary>
        /// Not supported.
        /// </summary>
        public override void Use() => throw new NotSupportedException();
    }
}