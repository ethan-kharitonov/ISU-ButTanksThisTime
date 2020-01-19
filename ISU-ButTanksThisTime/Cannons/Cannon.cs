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
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Class Cannon.
    /// </summary>
    internal abstract class Cannon
    {
        protected Vector2 Pos;
        private static readonly float disFromCentreBase = 35 * Tank.IMG_SCALE_FACTOR;
        protected float Rotation;

        //Shooting Variables
        private readonly Timer shootTimer;
        public bool Active;
        private readonly int rotateSpeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cannon"/> class.
        /// </summary>
        /// <param name="fireRate">The fire rate.</param>
        /// <param name="rotationSpeed">The rotation speed.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        protected Cannon(int fireRate, int rotationSpeed, bool active, Vector2 position, float rotation)
        {
            shootTimer = new Timer(fireRate);
            rotateSpeed = rotationSpeed;
            Active = active;
            Pos = CalcPos(position, rotation);
            Rotation = rotation;
        }

        /// <summary>
        /// Updates the specified base position.
        /// </summary>
        /// <param name="basePos">The base position.</param>
        /// <param name="baseRotation">The base rotation.</param>
        /// <param name="target">The target.</param>
        public virtual void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {
            Pos = CalcPos(basePos, baseRotation);

            target -= Pos;
            target *= new Vector2(1, -1);
            Rotation = Tools.RotateTowardsVector(Rotation, target, rotateSpeed);
            if (shootTimer.IsTimeUp(Tools.GameTime) && Active && !GameScene.GameIsFrozen())
            {
                shootTimer.Reset();
                var newBullet = Bullet.Clone(Pos, Rotation);
                GameScene.AddBullet(newBullet);
                if (newBullet.BulletOwner == Owner.Player)
                {
                    GameScene.RemoveBullet();
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, Pos, null, Color.White, -MathHelper.ToRadians(Rotation) + MathHelper.PiOver2, new Vector2(Img.Width * 0.5f, Img.Height * 0.75f), Tank.IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
        }

        /// <summary>
        /// Gets the bullet.
        /// </summary>
        /// <value>The bullet.</value>
        protected abstract Bullet Bullet { get; }
        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected abstract Texture2D Img { get; }

        /// <summary>
        /// Calculates the position.
        /// </summary>
        /// <param name="basePosition">The base position.</param>
        /// <param name="baseRotation">The base rotation.</param>
        /// <returns>Vector2.</returns>
        protected Vector2 CalcPos(Vector2 basePosition, float baseRotation)
        {
            var position = new Vector2((float) Math.Cos(MathHelper.ToRadians(baseRotation)), (float) -Math.Sin(MathHelper.ToRadians(baseRotation))) * -disFromCentreBase;
            position += basePosition;

            return position;
        }
    }
}