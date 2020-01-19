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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using ISU_ButTanksThisTime.Shapes;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Enum Owner
    /// </summary>
    internal enum Owner
    {
        Enemy = 0,
        Player = 1
    }

    /// <summary>
    /// Enum Stage
    /// </summary>
    internal enum Stage
    {
        Low = 0,
        // ReSharper disable UnusedMember.Global
        Medium = 1,
        High = 2,
        // ReSharper restore UnusedMember.Global
        Player = 3
    }

    /// <summary>
    /// Class Tools.
    /// </summary>
    public static class Tools
    {
        public static Rectangle Screen;
        public static readonly Random Rnd = new Random();
        private const int ROUND_PRECISION = 3;
        public static Rectangle ArenaBounds;

        public static GameTime GameTime;

        //public static Texture2D RedSquare;
        public static ContentManager Content;
        public static GraphicsDevice Graphics;
        public static Vector2 TrueMousePos;
        public static SpriteFont Font;
        public static Texture2D ButtonImg;
        private const float TOLERANCE = 0.00001F;

        /// <summary>
        /// Approaches the value.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="target">The target.</param>
        /// <param name="speed">The speed.</param>
        /// <returns>System.Single.</returns>
        public static float ApproachValue(float current, float target, float speed)
        {
            if (current < target)
            {
                current += speed;
                current = current > target ? target : current;
            }

            if (current > target)
            {
                current -= speed;
                current = current < target ? target : current;
            }

            return current;
        }

        /// <summary>
        /// Atans the specified y.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        /// <returns>System.Single.</returns>
        private static float Atan(float y, float x)
        {
            var angle = (float) Math.Atan2(y, x);
            angle = angle < 0 ? angle + MathHelper.TwoPi : angle;
            return angle;
        }

        /// <summary>
        /// Rotates the towards vector.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="target">The target.</param>
        /// <param name="rotationSpeed">The rotation speed.</param>
        /// <returns>System.Single.</returns>
        public static float RotateTowardsVector(float current, Vector2 target, float rotationSpeed)
        {
            var targetAngel = MathHelper.ToDegrees(Atan(target.Y, target.X));
            var delta = targetAngel - current;

            if (Math.Abs(delta) < TOLERANCE)
            {
                return current;
            }

            if (Math.Abs(delta) < rotationSpeed)
            {
                return targetAngel;
            }


            var dir = 1;
            if (current > targetAngel)
            {
                dir = -1;
            }

            var newAngle = Math.Abs(delta) <= 180 ? current + dir * rotationSpeed : current - dir * rotationSpeed;
            newAngle = newAngle < 0 ? newAngle + 360 : newAngle;
            newAngle %= 360;

            return newAngle;
        }

        /// <summary>
        /// Lines the line col.
        /// </summary>
        /// <param name="line1">The line1.</param>
        /// <param name="line2">The line2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool LineLineCol(Line line1, Line line2)
        {
            var s1 = line1.StartPoint;
            var e1 = line1.EndPoint;
            var s2 = line2.StartPoint;
            var e2 = line2.EndPoint;

            var isFirstPerpendicularToAxisX = Math.Abs(s1.X - e1.X) < TOLERANCE;
            var isSecondPerpendicularToAxisX = Math.Abs(s2.X - e2.X) < TOLERANCE;

            if (isFirstPerpendicularToAxisX && isSecondPerpendicularToAxisX)
            {
                return Math.Abs(s1.X - s2.X) < TOLERANCE && DoSameLineIntervalsNotPerpendicularToAxisYIntersect(s1, e1, s2, e2);
            }

            if (isFirstPerpendicularToAxisX)
            {
                return IsIntersectingWithPerpendicular(s1, e1, s2, e2);
            }

            if (isSecondPerpendicularToAxisX)
            {
                return IsIntersectingWithPerpendicular(s2, e2, s1, e1);
            }

            var (a1, b1) = CalcLine(s1, e1);
            var (a2, b2) = CalcLine(s2, e2);

            if (Math.Abs(a1 - a2) < TOLERANCE)
            {
                return Math.Abs(b1 - b2) < TOLERANCE && (Math.Abs(a1) < TOLERANCE
                           ? DoSameLineIntervalsPerpendicularToAxisYIntersect(s1, e1, s2, e2) // both intervals are on a line perpendicular to the axis Y
                           : DoSameLineIntervalsNotPerpendicularToAxisYIntersect(s1, e1, s2, e2));
            }

            var intersection = new Vector2(
                (float) Math.Round((b2 - b1) / (a1 - a2), ROUND_PRECISION),
                (float) Math.Round((b2 * a1 - a2 * b1) / (a1 - a2), ROUND_PRECISION));

            return
                IsBetween(s1.X, intersection.X, e1.X) &&
                IsBetween(s2.X, intersection.X, e2.X) &&
                IsBetween(s1.Y, intersection.Y, e1.Y) &&
                IsBetween(s2.Y, intersection.Y, e2.Y);
        }

        #region CalculateIntersectionResultFinal

        #endregion

        /// <summary>
        /// Does the same line intervals not perpendicular to axis y intersect.
        /// </summary>
        /// <param name="s1">The s1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="s2">The s2.</param>
        /// <param name="e2">The e2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool DoSameLineIntervalsNotPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) => 
            IsBetween(s2.Y, s1.Y, e2.Y) ||  // s1 is between s2 and e2
            IsBetween(s2.Y, e1.Y, e2.Y) ||  // e1 is between s2 and e2
            IsBetween(s1.Y, s2.Y, e1.Y) ||  // s2 is between s1 and e1
            IsBetween(s1.Y, e2.Y, e1.Y);    // e2 is between s1 and e1

        /// <summary>
        /// Does the same line intervals perpendicular to axis y intersect.
        /// </summary>
        /// <param name="s1">The s1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="s2">The s2.</param>
        /// <param name="e2">The e2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool DoSameLineIntervalsPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) =>
            IsBetween(s2.X, s1.X, e2.X) ||  // s1 is between s2 and e2
            IsBetween(s2.X, e1.X, e2.X) ||  // e1 is between s2 and e2
            IsBetween(s1.X, s2.X, e1.X) ||  // s2 is between s1 and e1
            IsBetween(s1.X, e2.X, e1.X);    // e2 is between s1 and e1

        /// <summary>
        /// Determines whether [is intersecting with perpendicular] [the specified s1].
        /// </summary>
        /// <param name="s1">The s1.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="s2">The s2.</param>
        /// <param name="e2">The e2.</param>
        /// <returns><c>true</c> if [is intersecting with perpendicular] [the specified s1]; otherwise, <c>false</c>.</returns>
        private static bool IsIntersectingWithPerpendicular(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var (a2, b2) = CalcLine(s2, e2);
            var y = (float) Math.Round(a2 * s1.X + b2, ROUND_PRECISION);
            return IsBetween(s2.X, s1.X, e2.X) && IsBetween(s1.Y, y, e1.Y) && IsBetween(s2.Y, y, e2.Y);
        }

        /// <summary>
        /// Determines whether the specified f1 is between.
        /// </summary>
        /// <param name="f1">The f1.</param>
        /// <param name="x">The x.</param>
        /// <param name="f2">The f2.</param>
        /// <returns><c>true</c> if the specified f1 is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(float f1, float x, float f2) => Math.Min(f1, f2) <= x && x <= Math.Max(f1, f2);

        /// <summary>
        /// Calculates the line.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.ValueTuple&lt;System.Double, System.Double&gt;.</returns>
        private static (double a, double b) CalcLine(Vector2 s, Vector2 e) =>
        (
            Math.Round((s.Y - e.Y) / (s.X - e.X), ROUND_PRECISION),
            Math.Round((s.X * e.Y - s.Y * e.X) / (s.X - e.X), ROUND_PRECISION));

        /// <summary>
        /// Boxes the box collision.
        /// </summary>
        /// <param name="box1">The box1.</param>
        /// <param name="box2">The box2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool BoxBoxCollision(RotatedRectangle box1, RotatedRectangle box2)
        {
            Line[] lines1 =
            {
                new Line(box1.TopRight, box1.TopLeft),
                new Line(box1.BottomRight, box1.BottomLeft),
                new Line(box1.TopRight, box1.BottomRight),
                new Line(box1.TopLeft, box1.BottomLeft),
            };

            Line[] lines2 =
            {
                new Line(box2.TopRight, box2.TopLeft),
                new Line(box2.BottomRight, box2.BottomLeft),
                new Line(box2.TopRight, box2.BottomRight),
                new Line(box2.TopLeft, box2.BottomLeft),
            };

            foreach (var line1 in lines1)
            foreach (var line2 in lines2)
            {
                if (LineLineCol(line1, line2))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Circles the point collision.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CirclePointCollision(Circle circle, Vector2 point) => (point - circle.Centre).LengthSquared() <= Math.Pow(circle.Radius, 2);
    }
}