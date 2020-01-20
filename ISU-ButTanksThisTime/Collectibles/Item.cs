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
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    /// <summary>
    /// Denotes the available types of collectible items
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// When collected allows the player to boost the player's speed
        /// </summary>
        SpeedBoost,
        /// <summary>
        /// When collected allows the player to freeze the game and relocate to another place
        /// </summary>
        Relocate,
        /// <summary>
        /// When collected adds money to be spent at the store
        /// </summary>
        Coin,
        /// <summary>
        /// When collected adds ammo to the player
        /// </summary>
        Ammo
    }

    /// <summary>
    /// Implements the core functionality of any collectible item
    /// </summary>
    internal abstract class Item
    {
        //stores the position of this item
        private readonly Vector2 position;

        //A factor that the image of this item is scaled down by
        private const float IMG_SCALE_FACTOR = 0.7f;

        //The amount of this item the player recives when picking it up
        public readonly int Amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="position">The initial position of this collectible item.</param>
        /// <param name="amount">Denotes how many pieces of that item does the player get when the item is collected.</param>
        protected Item(Vector2 position, int amount = 1)
        {
            //saves given position and amount to member variables 
            this.position = position;
            Amount = amount;
        }

        /// <summary>
        /// Draws this collectible item.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, position, null, Color.White, 0, new Vector2(Img.Width * 0.5f, Img.Height * 0.5f), IMG_SCALE_FACTOR, SpriteEffects.None, 1);
        }

        /// <summary>
        /// The bounding box of this collectible item.
        /// </summary>
        /// <seealso cref="RotatedRectangle"/>
        public RotatedRectangle Box => new RotatedRectangle(new Rectangle((int) position.X, (int) position.Y, (int) (Img.Width * IMG_SCALE_FACTOR), (int) (Img.Height * IMG_SCALE_FACTOR)), 0, new Vector2(Img.Width * 0.5f * IMG_SCALE_FACTOR, Img.Height * 0.5f * IMG_SCALE_FACTOR));

        /// <summary>
        /// The image of this collectible item.
        /// </summary>
        public abstract Texture2D Img { get; }

        /// <summary>
        /// Indicates whether the <see cref="Use"/> function is supported.
        /// </summary>
        /// <value><c>true</c> if supported; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// The default value is <c>true</c>.
        /// </remarks>
        public virtual bool IsUseSupported => true;
        
        /// <summary>
        /// Use this collectible item, e.g. relocate the player or boost the speed.
        /// </summary>
        public abstract void Use();

        /// <summary>
        /// The item type.
        /// </summary>
        /// <seealso cref="ItemType"/>
        public abstract ItemType ItemType { get; }
    }
}