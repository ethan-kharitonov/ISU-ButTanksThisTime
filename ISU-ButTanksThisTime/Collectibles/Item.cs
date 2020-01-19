﻿using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    public enum ItemType
    {
        SpeedBoost,
        Relocate,
        Coin,
        Ammo
    }

    internal abstract class Item
    {
        private readonly Vector2 position;

        private const float IMG_SCALE_FACTOR = 0.7f;
        public readonly int Amount;

        protected Item(Vector2 position, int amount = 1)
        {
            this.position = position;
            Amount = amount;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, position, null, Color.White, 0, new Vector2(Img.Width * 0.5f, Img.Height * 0.5f), IMG_SCALE_FACTOR, SpriteEffects.None, 1);
        }

        public RotatedRectangle Box => new RotatedRectangle(new Rectangle((int) position.X, (int) position.Y, (int) (Img.Width * IMG_SCALE_FACTOR), (int) (Img.Height * IMG_SCALE_FACTOR)), 0, new Vector2(Img.Width * 0.5f * IMG_SCALE_FACTOR, Img.Height * 0.5f * IMG_SCALE_FACTOR));

        public abstract Texture2D Img { get; }

        public virtual bool Usable => true;
        public abstract void Use();

        public abstract ItemType ItemType { get; }
    }
}