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

namespace ISU_ButTanksThisTime.Collectibles
{
    /// <summary>
    /// Class Ammo.
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class Ammo : Item
    {
        public static readonly Item VoidObject = new Ammo(default);

        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        /// <summary>
        /// Initializes static members of the <see cref="Ammo"/> class.
        /// </summary>
        static Ammo() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Shotgun_Shells");

        /// <summary>
        /// Initializes a new instance of the <see cref="Ammo"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Ammo(Vector2 position) : base(position, 15)
        {
        }

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        public override Texture2D Img => img;
        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <value>The type of the item.</value>
        public override ItemType ItemType => ItemType.Ammo;
        /// <summary>
        /// Gets a value indicating whether this <see cref="Ammo"/> is usable.
        /// </summary>
        /// <value><c>true</c> if usable; otherwise, <c>false</c>.</value>
        public override bool Usable => false;

        /// <summary>
        /// Uses this instance.
        /// </summary>
        public override void Use()
        {
        }
    }
}