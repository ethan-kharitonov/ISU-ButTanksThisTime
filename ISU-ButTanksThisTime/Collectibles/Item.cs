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
    /// Enum ItemType
    /// </summary>
    public enum ItemType
    {
        SpeedBoost,
        Relocate,
        Coin,
        Ammo
    }

    /// <summary>
    /// Class Item.
    /// </summary>
    internal abstract class Item
    {
        private readonly Vector2 position;

        private const float IMG_SCALE_FACTOR = 0.7f;
        public readonly int Amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="amount">The amount.</param>
        protected Item(Vector2 position, int amount = 1)
        {
            this.position = position;
            Amount = amount;
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, position, null, Color.White, 0, new Vector2(Img.Width * 0.5f, Img.Height * 0.5f), IMG_SCALE_FACTOR, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Gets the box.
        /// </summary>
        /// <value>The box.</value>
        public RotatedRectangle Box => new RotatedRectangle(new Rectangle((int) position.X, (int) position.Y, (int) (Img.Width * IMG_SCALE_FACTOR), (int) (Img.Height * IMG_SCALE_FACTOR)), 0, new Vector2(Img.Width * 0.5f * IMG_SCALE_FACTOR, Img.Height * 0.5f * IMG_SCALE_FACTOR));

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        public abstract Texture2D Img { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Item"/> is usable.
        /// </summary>
        /// <value><c>true</c> if usable; otherwise, <c>false</c>.</value>
        public virtual bool Usable => true;
        /// <summary>
        /// Uses this instance.
        /// </summary>
        public abstract void Use();

        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <value>The type of the item.</value>
        public abstract ItemType ItemType { get; }
    }
}