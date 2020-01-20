// Author        : Ethan Kharitonov
// File Name     : Shapes\Circle.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements circle objects.
using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime.Shapes
{
    /// <summary>
    /// Implements circle objects.
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
            //store given centre and raius to member variables
            Centre = centre;
            Radius = radius;
        }
    }
}