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
    /// Class SpeedBoostItem.
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class SpeedBoostItem : Item
    {
        public static readonly Item VoidObject = new SpeedBoostItem(default);

        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        /// <summary>
        /// Initializes static members of the <see cref="SpeedBoostItem"/> class.
        /// </summary>
        static SpeedBoostItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Speed_Bonus");

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedBoostItem"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public SpeedBoostItem(Vector2 position) : base(position)
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
        public override ItemType ItemType => ItemType.SpeedBoost;

        /// <summary>
        /// Uses this instance.
        /// </summary>
        public override void Use()
        {
            GameScene.SpeedUpPlayer();
        }
    }
}