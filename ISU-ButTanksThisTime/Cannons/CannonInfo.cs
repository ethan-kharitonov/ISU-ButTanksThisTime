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
    /// Class CannonInfo.
    /// </summary>
    internal class CannonInfo
    {
        public readonly int FireRate;
        public readonly int RotationSpeed;
        public readonly Texture2D Img;
        public readonly BulletInfo Bullet;
        public readonly Cannon Cannon;
        public readonly int? BurstRate;
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
        public CannonInfo(int fireRate, int rotationSpeed, Texture2D img, BulletInfo bullet,
            Cannon cannon,
            int? burstRate = null, int? burstDuration = null)
        {
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