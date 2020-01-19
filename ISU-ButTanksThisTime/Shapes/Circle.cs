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
using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime.Shapes
{
    /// <summary>
    /// Class Circle.
    /// </summary>
    public class Circle
    {
        public Vector2 Centre;
        public readonly float Radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="centre">The centre.</param>
        /// <param name="radius">The radius.</param>
        public Circle(Vector2 centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }
    }
}