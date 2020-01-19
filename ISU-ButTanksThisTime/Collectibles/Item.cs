using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    public enum ItemType
    {
        SpeedBoost,
        Relocate,
        Coin,
        Ammo
    }

    abstract class Item
    {
        private Vector2 position;

        private const float IMG_SCALE_FACTOR = 0.7f;
        public readonly int Amount;
        
        public Item(Vector2 position, int amount = 1)
        {
            this.position = position;
            Amount = amount;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, position, null, Color.White, 0, new Vector2(Img.Width * 0.5f, Img.Height * 0.5f), IMG_SCALE_FACTOR, SpriteEffects.None, 1);
        }

        public RotatedRectangle Box => new RotatedRectangle(new Rectangle((int)(position.X), (int)(position.Y), (int)(Img.Width * IMG_SCALE_FACTOR), (int)(Img.Height * IMG_SCALE_FACTOR)), 0, new Vector2(Img.Width * 0.5f * IMG_SCALE_FACTOR, Img.Height * 0.5f * IMG_SCALE_FACTOR));

        public abstract Texture2D Img { get; }

        public virtual bool Usable => true;
        public abstract void Use();

        public abstract ItemType ItemType { get; }
    }
}
