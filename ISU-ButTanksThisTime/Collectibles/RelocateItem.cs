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
    /// The RelocateItem collectible item.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Collectibles.Item" /> abstract type.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Collecting this item allows the player to relocate safely to another place.
    /// </remarks>
    /// <seealso cref="ISU_ButTanksThisTime.Collectibles.Item" />
    internal class RelocateItem : Item
    {
        /// <summary>
        /// Used to access the attributes of a collectible item that are the same for any instance of the same type, like <see cref="Img"/> or <see cref="Item.IsUseSupported"/>.
        /// </summary>
        public static readonly Item VoidObject = new RelocateItem(default);

        private static readonly Texture2D img;

        /// <summary>
        /// Initializes static members of the <see cref="RelocateItem"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static RelocateItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Mobility_Icon");

        /// <summary>
        /// Initializes a new instance of the <see cref="RelocateItem"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        public RelocateItem(Vector2 position) : base(position)
        {
        }

        /// <summary>
        /// Overrides the base <see cref="Item.Img"/> property.
        /// </summary>
        /// <value>The Relocate Item object image.</value>
        public override Texture2D Img => img;

        /// <summary>
        /// Overrides the base <see cref="Item.ItemType"/> property.
        /// </summary>
        /// <value><c>ItemType.Relocate</c></value>
        public override ItemType ItemType => ItemType.Relocate;

        /// <summary>
        /// Use this item, i.e. freeze the game.
        /// </summary>
        public override void Use() => GameScene.FreezeGame();
    }
}