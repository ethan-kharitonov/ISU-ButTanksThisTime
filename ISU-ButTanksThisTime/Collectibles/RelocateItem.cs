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
    /// Class RelocateItem.
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class RelocateItem : Item
    {
        public static readonly Item VoidObject = new RelocateItem(default);

        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        /// <summary>
        /// Initializes static members of the <see cref="RelocateItem"/> class.
        /// </summary>
        static RelocateItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Mobility_Icon");

        /// <summary>
        /// Initializes a new instance of the <see cref="RelocateItem"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public RelocateItem(Vector2 position) : base(position)
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
        public override ItemType ItemType => ItemType.Relocate;

        /// <summary>
        /// Uses this instance.
        /// </summary>
        public override void Use()
        {
            GameScene.FreezeGame();
        }
    }
}