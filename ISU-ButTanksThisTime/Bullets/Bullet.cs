using System;
using Animation2D;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    internal abstract class Bullet
    {
        protected Vector2 Position;
        private readonly float rotation;
        private const float DEF_VELOCITY = 10f;
        private readonly float scaleFactor;
        private Rectangle box;
        public readonly Owner BulletOwner;

        protected abstract Animation ExAnim { get; }
        public bool IsDead { get; protected set; }
        public int Damage { get; protected set; }


        public Bullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner)
        {
            this.scaleFactor = scaleFactor;
            Position = position;
            this.rotation = rotation;
            BulletOwner = bulletOwner;
        }

        public virtual bool Update()
        {
            if (!IsDead)
            {
                Position += new Vector2((float) Math.Cos(MathHelper.ToRadians(rotation)), (float) -Math.Sin(MathHelper.ToRadians(rotation))) * DEF_VELOCITY;
            }
            else
            {
                ExAnim.Update(Tools.GameTime);
            }

            return IsDead && !ExAnim.isAnimating || !Tools.IsBetween(Tools.ArenaBounds.Left, Position.X, Tools.ArenaBounds.Right + Img.Width) || !Tools.IsBetween(Tools.ArenaBounds.Bottom - Img.Height, Position.Y, Tools.ArenaBounds.Top);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
            {
                box = new Rectangle((int) Position.X, (int) Position.Y, (int) (Img.Width * scaleFactor), (int) (Img.Height * scaleFactor));
                spriteBatch.Draw(Img, box, null, Color.White, -MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2((float) (Img.Width / 2.0), (float) (Img.Height / 2.0)), SpriteEffects.None, 1f);
            }
            else
            {
                ExAnim.Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
        }

        public virtual RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(box, MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2(Img.Width * 0.5f * scaleFactor, Img.Height * 0.5f * scaleFactor));

        public virtual void Collide()
        {
            IsDead = true;
            ExAnim.destRec.X = (int) Position.X - ExAnim.destRec.Width / 2;
            ExAnim.destRec.Y = (int) Position.Y - ExAnim.destRec.Height / 2;
        }

        public abstract Bullet Clone(Vector2 pos, float rotation);

        protected abstract Texture2D Img { get; }
    }
}