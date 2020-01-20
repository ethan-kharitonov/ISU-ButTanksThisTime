// Author        : Ethan Kharitonov
// File Name     : Bullets\Bullet.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the core functionality of bullet objects.
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
        //Stores the position, rotation and velocity of the bullet
        protected Vector2 Position;
        private readonly float rotation;
        private const float DEF_VELOCITY = 12f;

        //stores the factor by whihc the bullet image is scaled
        private readonly float scaleFactor;

        //stores the rectangle of the bullet
        private Rectangle box;

        //stores the owner of the bullet
        public readonly Owner BulletOwner;

        /// <summary>
        /// Explosion Animation.
        /// </summary>
        /// <remarks>
        /// Defines the animation to run when a bullet hits a game object.
        /// <para>
        /// Must be implemented by every kind of a bullet.
        /// </para>
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
        /// <param name="position">The initial bullet position.</param>
        /// <param name="rotation">The initial bullet rotation.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="bulletOwner">The bullet owner.</param>
        /// <seealso cref="Owner"/>
        protected Bullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner)
        {
            //save all given data to member variables
            this.scaleFactor = scaleFactor;
            Position = position;
            this.rotation = rotation;
            BulletOwner = bulletOwner;
        }

        /// <summary>
        /// Updates the core state of the bullet.
        /// </summary>
        /// <returns><c>true</c> if the bullet should be removed from the game scene, <c>false</c> otherwise.</returns>
        public virtual bool Update()
        {
            //move bullet if it is not dead update the explosion animation otherwise
            if (!IsDead)
            {
                //calculate the bullet x and y move amount and add them onto the bullet position
                Position += new Vector2((float) Math.Cos(MathHelper.ToRadians(rotation)), (float) -Math.Sin(MathHelper.ToRadians(rotation))) * DEF_VELOCITY;
            }
            else
            {
                //update the explosion animation
                ExAnim.Update(Tools.GameTime);
            }

            //return true if the bullet is dead and done exploding or has left the screen, false otherwise
            return IsDead && !ExAnim.isAnimating || !Tools.IsBetween(Tools.ArenaBounds.Left, Position.X, Tools.ArenaBounds.Right + Img.Width) || !Tools.IsBetween(Tools.ArenaBounds.Bottom - Img.Height, Position.Y, Tools.ArenaBounds.Top);
        }

        /// <summary>
        /// draws the bullet or its explosion animation
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //if bullet is not dead draw it, if it is draw its explosion
            if (!IsDead)
            {
                //draw bullet
                box = new Rectangle((int) Position.X, (int) Position.Y, (int) (Img.Width * scaleFactor), (int) (Img.Height * scaleFactor));
                spriteBatch.Draw(Img, box, null, Color.White, -MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2((float) (Img.Width / 2.0), (float) (Img.Height / 2.0)), SpriteEffects.None, 1f);
            }
            else
            {
                //draw the explosion animation
                ExAnim.Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
        }

        /// <summary>
        /// Computes and returns the rotated rectangle of this bullet
        /// </summary>
        /// <seealso cref="RotatedRectangle"/>
        public virtual RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(box, MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2(Img.Width * 0.5f * scaleFactor, Img.Height * 0.5f * scaleFactor));

        /// <summary>
        /// kills the bullet
        /// </summary>
        /// <remarks>
        /// Sets the bullet is dead and triggers the explosion animation.
        /// </remarks>
        /// <seealso cref="IsDead"/>
        /// <seealso cref="ExAnim"/>
        public virtual void Collide()
        {
            //set bullet to dead
            IsDead = true;

            //update the explosion animation coordinates to match with the place the bullet died
            ExAnim.destRec.X = (int) Position.X - ExAnim.destRec.Width / 2;
            ExAnim.destRec.Y = (int) Position.Y - ExAnim.destRec.Height / 2;
        }

        /// <summary>
        /// Clones the bullet with the new position and rotation.
        /// </summary>
        /// <param name="pos">The new position.</param>
        /// <param name="rotation">The new rotation.</param>
        /// <returns>A new bullet of the same kind at the new position and rotation.</returns>
        public abstract Bullet Clone(Vector2 pos, float rotation);

        /// <summary>
        /// The bullet image.
        /// </summary>
        /// <remarks>
        /// Must be overridden by the derived types.
        /// </remarks>
        protected abstract Texture2D Img { get; }
    }
}