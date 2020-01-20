// Author        : Ethan Kharitonov
// File Name     : Collectibles\SpeedBoostItem.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the Speed Boost collectible item.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    /// <summary>
    /// Implements the Speed Boost collectible item.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" /> abstract type.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Collecting this item allows the player to boost his/her speed.
    /// </remarks>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class SpeedBoostItem : Item
    {
        /// <summary>
        /// Used to access the attributes of a collectible item that are the same for any instance of the same type, like <see cref="Img"/> or <see cref="Item.IsUseSupported"/>.
        /// </summary>
        public static readonly Item VoidObject = new SpeedBoostItem(default);

        //stores the image of this item
        private static readonly Texture2D img;

        /// <summary>
        /// Initializes static members of the <see cref="SpeedBoostItem"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static SpeedBoostItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Speed_Bonus");

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedBoostItem"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        public SpeedBoostItem(Vector2 position) : base(position)
        {
        }

        /// <summary>
        /// Overrides the base <see cref="Item.Img"/> property.
        /// </summary>
        /// <value>The Speed Boost object image.</value>
        public override Texture2D Img => img;

        /// <summary>
        /// Overrides the base <see cref="Item.ItemType"/> property.
        /// </summary>
        /// <value><c>ItemType.SpeedBoost</c></value>
        public override ItemType ItemType => ItemType.SpeedBoost;

        /// <summary>
        /// Use this item, i.e. boost the player's speed.
        /// </summary>
        public override void Use() => GameScene.SpeedUpPlayer();
    }
}