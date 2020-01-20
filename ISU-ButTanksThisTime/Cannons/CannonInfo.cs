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
using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Stores all the information about a cannon
    /// </summary>
    internal class CannonInfo
    {
        //Time between shoots
        public readonly int FireRate;

        //How fast it rotates
        public readonly int RotationSpeed;

        //the image of the cannon
        public readonly Texture2D Img;

        //the information about the bullet it shoots
        public readonly BulletTypeInfo Bullet;

        //a void object of that cannon
        public readonly Cannon Cannon;

        //the time between bursts (if applicable)
        public readonly int? BurstRate;

        //the duration of its bursts (if applicable)
        public readonly int? BurstDuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CannonInfo"/> class.
        /// </summary>
        /// <param name="fireRate">The fire rate.</param>
        /// <param name="rotationSpeed">The rotation speed.</param>
        /// <param name="img">The img.</param>
        /// <param name="bullet">The bullet.</param>
        /// <param name="cannon">The cannon.</param>
        /// <param name="burstRate">The burst rate.</param>
        /// <param name="burstDuration">Duration of the burst.</param>
        public CannonInfo(int fireRate, int rotationSpeed, Texture2D img, BulletTypeInfo bullet,
            Cannon cannon,
            int? burstRate = null, int? burstDuration = null)
        {
            //saves all the given data to member variables
            FireRate = fireRate;
            RotationSpeed = rotationSpeed;
            Img = img;
            Bullet = bullet;
            Cannon = cannon;
            BurstRate = burstRate;
            BurstDuration = burstDuration;
        }
    }
}