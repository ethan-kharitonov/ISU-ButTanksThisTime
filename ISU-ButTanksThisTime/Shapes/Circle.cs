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
    /// Represents circle objects.
    /// </summary>
    public class Circle
    {
        /// <summary>
        /// The circle centre.
        /// </summary>
        public Vector2 Centre;
        /// <summary>
        /// The circle radius.
        /// </summary>
        public readonly float Radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="centre">The circle centre.</param>
        /// <param name="radius">The circle radius.</param>
        public Circle(Vector2 centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }
    }
}