﻿using System;
using Animation2D;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    internal abstract class Bullet
    {
        protected Texture2D img;
        protected Vector2 position;
        private float rotation;
        private const float DEF_VELOCITY = 10f;
        private readonly float SCALE_FACTOR;
        private Rectangle box;
        public readonly Owner bulletOwner;

        protected abstract Animation ExAnim { get; }
        public bool IsDead { get; protected set; } = false;
        public int Damage { get; protected set; }


        public Bullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner)
        {
            SCALE_FACTOR = scaleFactor;
            this.position = position;
            this.rotation = rotation;
            this.bulletOwner = bulletOwner;
        }

        public virtual bool Update()
        {
            if (!IsDead)
            {
                position += new Vector2((float) Math.Cos(MathHelper.ToRadians(rotation)), (float) -Math.Sin(MathHelper.ToRadians(rotation))) * DEF_VELOCITY;
            }
            else
            {
                ExAnim.Update(Tools.GameTime);
            }

            return IsDead && !ExAnim.isAnimating || !Tools.IsBetween(Tools.ArenaBounds.Left, position.X, Tools.ArenaBounds.Right + img.Width) || !Tools.IsBetween(Tools.ArenaBounds.Bottom - img.Height, position.Y, Tools.ArenaBounds.Top);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
            {
                box = new Rectangle((int) position.X, (int) position.Y, (int) (img.Width * SCALE_FACTOR), (int) (img.Height * SCALE_FACTOR));
                spriteBatch.Draw(img, box, null, Color.White, -MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2((float) (img.Width / 2.0), (float) (img.Height / 2.0)), SpriteEffects.None, 1f);
            }
            else
            {
                ExAnim.Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
        }

        public virtual RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(box, MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2(img.Width * 0.5f * SCALE_FACTOR, img.Height * 0.5f * SCALE_FACTOR));

        public virtual void Collide()
        {
            IsDead = true;
            ExAnim.destRec.X = (int) position.X - ExAnim.destRec.Width / 2;
            ExAnim.destRec.Y = (int) position.Y - ExAnim.destRec.Height / 2;
        }

        public abstract Bullet Clone(Vector2 pos, float rotation);
    }
}