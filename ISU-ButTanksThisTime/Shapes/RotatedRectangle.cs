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
using System;
using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime.Shapes
{
    /// <summary>
    /// Class RotatedRectangle.
    /// </summary>
    public class RotatedRectangle
    {
        public readonly Vector2 TopLeft;
        public readonly Vector2 TopRight;
        public readonly Vector2 BottomLeft;
        public readonly Vector2 BottomRight;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle"/> class.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="origin">The origin.</param>
        public RotatedRectangle(Rectangle box, float rotation, Vector2 origin)
        {
            box.X -= (int) origin.X;
            box.Y -= (int) origin.Y;
            box.Center.ToVector2();

            var originalAngle = Math.Atan2(-box.Height / 2.0, box.Width / 2.0);
            double distance = new Vector2(box.Height / 2f, box.Width / 2f).Length();

            TopRight = CalcVertex(originalAngle, rotation, distance, box.Center.ToVector2());
            TopLeft = CalcVertex(MathHelper.ToRadians(180) - originalAngle, rotation, distance, box.Center.ToVector2());
            BottomRight = CalcVertex(-originalAngle, rotation, distance, box.Center.ToVector2());
            BottomLeft = CalcVertex(MathHelper.ToRadians(180) + originalAngle, rotation, distance, box.Center.ToVector2());
        }

        /// <summary>
        /// Calculates the vertex.
        /// </summary>
        /// <param name="originalAngle">The original angle.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="distFromCentre">The dist from centre.</param>
        /// <param name="centre">The centre.</param>
        /// <returns>Vector2.</returns>
        private Vector2 CalcVertex(double originalAngle, double rotation, double distFromCentre, Vector2 centre)
        {
            var vertex = new Vector2((float) Math.Cos(originalAngle + rotation), (float) -Math.Sin(originalAngle + rotation));
            vertex *= (float) distFromCentre;
            vertex += centre;

            return vertex;
        }
    }
}