// Author        : Ethan Kharitonov
// File Name     : Cannons\Cannon.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the base cannon type which points and shoots at a target and draws itself.
using System;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Implements the base cannon type which points and shoots at a target and draws itself.
    /// </summary>
    internal abstract class Cannon
    {
        //stores the distance of the cannon from the centre of the tank
        private static readonly float disFromCentreBase = 35 * Tank.IMG_SCALE_FACTOR;
        
        //stores the poition and rotation of the cannon
        protected Vector2 Pos;
        protected float Rotation;

        //stores the shooting timer (time left untill next shot)
        private readonly Timer shootTimer;

        //stores wheter this cannon is active (will shoot) and the speed at whihc it rotates
        public bool Active;
        private readonly int rotateSpeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cannon"/> class.
        /// </summary>
        /// <param name="fireRate">The fire rate.</param>
        /// <param name="rotationSpeed">The rotation speed.</param>
        /// <param name="active">Is the cannon active</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        protected Cannon(int fireRate, int rotationSpeed, bool active, Vector2 position, float rotation)
        {
            //stores all given variables to member variables
            shootTimer = new Timer(fireRate);
            rotateSpeed = rotationSpeed;
            Active = active;
            Rotation = rotation;

            //calculates and stores the position of the cannon
            Pos = CalcPos(position, rotation);
        }

        /// <summary>
        /// moves along with the tank, rotates towards the given target and shoots if active
        /// </summary>
        /// <param name="basePos">The position of the tank.</param>
        /// <param name="baseRotation">The rotation of the tank.</param>
        /// <param name="target">The target.</param>
        public virtual void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {
            //calculates the position of the cannon based on the base position and rotation
            Pos = CalcPos(basePos, baseRotation);

            //rotates towards the target
            target -= Pos;
            target *= new Vector2(1, -1);
            Rotation = Tools.RotateTowardsVector(Rotation, target, rotateSpeed);

            //shoots if active, game is not frozen and the shoot timer is up
            if (shootTimer.IsTimeUp(Tools.GameTime) && Active && !GameScene.GameIsFrozen())
            {
                //resets the time until next shot
                shootTimer.Reset();

                //creates a new bullet and shoots it
                var newBullet = Bullet.Clone(Pos, Rotation);
                GameScene.AddBullet(newBullet);

                //if this cannon belongs to the player update the ammo
                if (newBullet.BulletOwner == Owner.Player)
                {
                    //update te ammo
                    GameScene.RemoveBullet();
                }
            }
        }

        /// <summary>
        /// draws the cannon
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //draws the cannon
            spriteBatch.Draw(Img, Pos, null, Color.White, -MathHelper.ToRadians(Rotation) + MathHelper.PiOver2, new Vector2(Img.Width * 0.5f, Img.Height * 0.75f), Tank.IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
        }

        /// <summary>
        /// Gets the bullet this cannon shoots.
        /// </summary>
        /// <value>The bullet.</value>
        protected abstract Bullet Bullet { get; }
        /// <summary>
        /// Gets the img of this cannon.
        /// </summary>
        /// <value>The img.</value>
        protected abstract Texture2D Img { get; }

        /// <summary>
        /// Calculates the position of the cannon based on a given tank position and rotation.
        /// </summary>
        /// <param name="basePosition">The tank position.</param>
        /// <param name="baseRotation">The tank rotation.</param>
        /// <returns>the new position of the cannon.</returns>
        protected Vector2 CalcPos(Vector2 basePosition, float baseRotation)
        {
            //calculate the new cannon position
            var position = new Vector2((float) Math.Cos(MathHelper.ToRadians(baseRotation)), (float) -Math.Sin(MathHelper.ToRadians(baseRotation))) * -disFromCentreBase;
            position += basePosition;

            //return the new cannon position
            return position;
        }
    }
}