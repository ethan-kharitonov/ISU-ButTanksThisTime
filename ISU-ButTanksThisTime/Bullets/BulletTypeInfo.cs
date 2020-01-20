// Author        : Ethan Kharitonov
// File Name     : Bullets\BulletTypeInfo.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Holds a few bits of information specific to a bullet type rather than individual bullet.
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Holds a few bits of information specific to a bullet type rather than individual bullet.
    /// </summary>
    internal class BulletTypeInfo
    {
        /// <summary>
        /// The image of the particular bullet type.
        /// </summary>
        public readonly Texture2D Img;
        /// <summary>
        /// The damage inflicted by the particular bullet type.
        /// </summary>
        public readonly int Damage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletTypeInfo"/> class.
        /// </summary>
        /// <param name="img">The bullet image.</param>
        /// <param name="damage">The bullet damage.</param>
        public BulletTypeInfo(Texture2D img, int damage)
        {
            //Implements the image and damage of the bullet
            Img = img;
            Damage = damage;
        }
    }
}