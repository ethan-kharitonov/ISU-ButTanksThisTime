using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    abstract class Bullet
    {
        protected Texture2D img;
        private Vector2 position;
        private float rotation;
        private const float DEF_VELOCITY = 10f;
        private readonly float SCALE_FACTOR;
        private Rectangle box;
        private bool isDead = false;
        public readonly Owner bulletOwner;

        public Bullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner)
        {
            SCALE_FACTOR = scaleFactor;
            this.position = position;
            this.rotation = rotation;
            this.bulletOwner = bulletOwner;
        }

        public bool Update()
        {
            position += new Vector2((float)Math.Cos(MathHelper.ToRadians(rotation)), (float)-Math.Sin(MathHelper.ToRadians(rotation))) * DEF_VELOCITY;
            return isDead || !Tools.IsBetween(Tools.ArenaBounds.Left, position.X, Tools.ArenaBounds.Right + img.Width) || !Tools.IsBetween(Tools.ArenaBounds.Bottom - img.Height, position.Y, Tools.ArenaBounds.Top);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            box = new Rectangle((int)position.X, (int)position.Y, (int)(img.Width * SCALE_FACTOR), (int)(img.Height * SCALE_FACTOR));
            spriteBatch.Draw(img, box, null, Color.White, -MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2((float)(img.Width / 2.0), (float)(img.Width / 2.0)), SpriteEffects.None, 1f);

            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().TopLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().TopRight, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().BotomLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().BotomRight, Color.White);

        }

        public RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(box, -MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2((img.Width * 0.5f * SCALE_FACTOR), (img.Height * 0.5f * SCALE_FACTOR)));
   
        public void Collide()
        {
            isDead = true;
        }

        public abstract Bullet Clone(Vector2 pos, float rotation);
    }
}
