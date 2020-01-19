// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using System;
using Animation2D;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Implements the core functionality of bullet objects.
    /// </summary>
    internal abstract class Bullet
    {
        protected Vector2 Position;
        private readonly float rotation;
        private const float DEF_VELOCITY = 10f;
        private readonly float scaleFactor;
        private Rectangle box;
        public readonly Owner BulletOwner;

        /// <summary>
        /// Explosion Animation.
        /// </summary>
        /// <remarks>
        /// Defines the animation to run when the bullet hits the player.
        /// </remarks>
        protected abstract Animation ExAnim { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is dead.
        /// </summary>
        /// <value><c>true</c> if this instance is dead; otherwise, <c>false</c>.</value>
        public bool IsDead { get; protected set; }
        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>The damage.</value>
        public int Damage { get; protected set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="bulletOwner">The bullet owner.</param>
        protected Bullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner)
        {
            this.scaleFactor = scaleFactor;
            Position = position;
            this.rotation = rotation;
            BulletOwner = bulletOwner;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
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

        /// <summary>
        /// Gets the rotated rectangle.
        /// </summary>
        /// <returns>RotatedRectangle.</returns>
        public virtual RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(box, MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2(Img.Width * 0.5f * scaleFactor, Img.Height * 0.5f * scaleFactor));

        /// <summary>
        /// Collides this instance.
        /// </summary>
        public virtual void Collide()
        {
            IsDead = true;
            ExAnim.destRec.X = (int) Position.X - ExAnim.destRec.Width / 2;
            ExAnim.destRec.Y = (int) Position.Y - ExAnim.destRec.Height / 2;
        }

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>Bullet.</returns>
        public abstract Bullet Clone(Vector2 pos, float rotation);

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected abstract Texture2D Img { get; }
    }
}