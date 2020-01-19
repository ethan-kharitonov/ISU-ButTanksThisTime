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
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Class BulletInfo.
    /// </summary>
    internal class BulletInfo
    {
        public readonly Texture2D Img;
        public readonly int Damage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletInfo"/> class.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="damage">The damage.</param>
        public BulletInfo(Texture2D img, int damage)
        {
            Img = img;
            Damage = damage;
        }
    }
}