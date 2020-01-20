// Author        : Ethan Kharitonov
// File Name     : Shapes\Line.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements line objects.
using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime.Shapes
{
    /// <summary>
    /// Implements line objects.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// The start point of this line.
        /// </summary>
        public Vector2 StartPoint;
        /// <summary>
        /// The end point of this line.
        /// </summary>
        public Vector2 EndPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="startPoint">The start point.</param>
        /// <param name="endPoint">The end point.</param>
        public Line(Vector2 startPoint, Vector2 endPoint)
        {
            //store start and end points to member variables
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        /// <summary>
        /// Returns the string representation of this line.
        /// </summary>
        public override string ToString() => $"{{{StartPoint.X}, {StartPoint.Y}}},{{{EndPoint.X}, {EndPoint.Y}}}";
    }
}