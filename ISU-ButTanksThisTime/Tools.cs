// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary>
// The purpose of this file is to provide the tools used in calculating collisions and rotations as well as expose certain well-known
// objects for all other objects to use.
// </summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using ISU_ButTanksThisTime.Shapes;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Denotes whether a bullet is owned by the player or the enemy.
    /// </summary>
    /// <remarks>
    /// Used to figure out if a bullet causes damage to a game object.
    /// </remarks>
    internal enum Owner
    {
        /// <summary>
        /// Denotes an enemy object
        /// </summary>
        Enemy = 0,
        /// <summary>
        /// Denotes the player
        /// </summary>
        Player = 1
    }

    /// <summary>
    /// Denotes the capabilities of a tank.
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
    /// Provides functions to compute collisions and rotations and exposes some well-known objects for all to use.
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
        /// Given the current value, the target value and the speed returns the next value from the current on the way to the target.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The target value.</param>
        /// <param name="speed">The speed.</param>
        /// <returns>The next value at the given speed.</returns>
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
        /// A slightly modified version of the standard <see cref="Math.Atan2(double, double)"/> function,
        /// which makes sure the result is always in the range [0..2*pi)
        /// </summary>
        private static float Atan(float y, float x)
        {
            var angle = (float) Math.Atan2(y, x);
            return angle < 0 ? angle + MathHelper.TwoPi : angle;
        }

        /// <summary>
        /// Given an angle, a target point and a rotation speed, calculates the next value for the angle on its way to the target point.
        /// </summary>
        /// <param name="current">The current angle value in degrees.</param>
        /// <param name="target">The target point.</param>
        /// <param name="rotationSpeed">The rotation speed in degrees.</param>
        /// <returns>The next angle value at the given rotation speed.</returns>
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
        /// Checks whether the given two intervals intersect.
        /// </summary>
        /// <param name="line1">The first interval.</param>
        /// <param name="line2">The second interval.</param>
        /// <returns><c>true</c> if intersect, <c>false</c> otherwise.</returns>
        private static bool LineLineCol(Line line1, Line line2)
        {
            var s1 = line1.StartPoint;
            var e1 = line1.EndPoint;
            var s2 = line2.StartPoint;
            var e2 = line2.EndPoint;

            // Indicates whether the first line is perpendicular to the X axis
            var isFirstPerpendicularToAxisX = Math.Abs(s1.X - e1.X) < TOLERANCE;
            // Indicates whether the second line is perpendicular to the X axis
            var isSecondPerpendicularToAxisX = Math.Abs(s2.X - e2.X) < TOLERANCE;

            if (isFirstPerpendicularToAxisX && isSecondPerpendicularToAxisX)
            {
                // if both are perpendicular to the axis X, then they intersect only if they are 
                // on the same line and one of interval ends belongs to the other interval.
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

            // Line equation is y = ax + b, we need to calculate (a) and (b) for both lines in order
            // to compute if both lines intersect.
            var (a1, b1) = CalcLine(s1, e1);
            var (a2, b2) = CalcLine(s2, e2);

            if (Math.Abs(a1 - a2) < TOLERANCE)
            {
                // The lines are parallel to each other, they intersect only if they are on the same line and
                // one of interval ends belongs to the other interval.
                return Math.Abs(b1 - b2) < TOLERANCE && (Math.Abs(a1) < TOLERANCE
                    ? DoSameLineIntervalsPerpendicularToAxisYIntersect(s1, e1, s2, e2)   // both intervals are on a line perpendicular to the axis Y
                    : DoSameLineIntervalsNotPerpendicularToAxisYIntersect(s1, e1, s2, e2)); // both intervals are on a line that is not perpendicular to any axis
            }

            // Use the formula for line intersection to compute the intersection point
            var intersection = new Vector2(
                (float) Math.Round((b2 - b1) / (a1 - a2), ROUND_PRECISION),
                (float) Math.Round((b2 * a1 - a2 * b1) / (a1 - a2), ROUND_PRECISION));

            // Check that the intersection point belongs to each interval.
            return
                IsBetween(s1.X, intersection.X, e1.X) &&
                IsBetween(s2.X, intersection.X, e2.X) &&
                IsBetween(s1.Y, intersection.Y, e1.Y) &&
                IsBetween(s2.Y, intersection.Y, e2.Y);
        }

        /// <summary>
        /// Check if the two intervals intersect when it is known they are on the same line, that is not perpendicular to axis Y.
        /// </summary>
        /// <param name="s1">The starting point of the first interval.</param>
        /// <param name="e1">The ending point of the first interval.</param>
        /// <param name="s2">The starting point of the second interval.</param>
        /// <param name="e2">The ending point of the second interval.</param>
        /// <returns>true if intersect, false otherwise.</returns>
        private static bool DoSameLineIntervalsNotPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) =>
            IsBetween(s2.Y, s1.Y, e2.Y) || // s1 lies on the second interval
            IsBetween(s2.Y, e1.Y, e2.Y) || // e1 lies on the second interval
            IsBetween(s1.Y, s2.Y, e1.Y) || // s2 lies on the first interval
            IsBetween(s1.Y, e2.Y, e1.Y);   // e2 lies on the first interval

        /// <summary>
        /// Check if the two intervals intersect when it is known they are on the same line, that is perpendicular to axis Y.
        /// </summary>
        /// <param name="s1">The starting point of the first interval.</param>
        /// <param name="e1">The ending point of the first interval.</param>
        /// <param name="s2">The starting point of the second interval.</param>
        /// <param name="e2">The ending point of the second interval.</param>
        /// <returns>true if intersect, false otherwise.</returns>
        private static bool DoSameLineIntervalsPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) =>
            IsBetween(s2.X, s1.X, e2.X) || // s1 lies on the second interval
            IsBetween(s2.X, e1.X, e2.X) || // e1 lies on the second interval
            IsBetween(s1.X, s2.X, e1.X) || // s2 lies on the first interval
            IsBetween(s1.X, e2.X, e1.X);   // e2 lies on the first interval

        /// <summary>
        /// Checks if the intervals intersect when the first one is perpendicular to the axis X, but the other is not.
        /// </summary>
        /// <param name="s1">The starting point of the first interval, which is perpendicular to the axis X.</param>
        /// <param name="e1">The ending point of the first interval, which is perpendicular to the axis X.</param>
        /// <param name="s2">The starting point of the second interval, which is not perpendicular to the axis X.</param>
        /// <param name="e2">The ending point of the second interval, which is not perpendicular to the axis X.</param>
        /// <returns>true if intersect, false otherwise.</returns>
        private static bool IsIntersectingWithPerpendicular(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var (a2, b2) = CalcLine(s2, e2);
            var y = (float) Math.Round(a2 * s1.X + b2, ROUND_PRECISION);
            return IsBetween(s2.X, s1.X, e2.X) && IsBetween(s1.Y, y, e1.Y) && IsBetween(s2.Y, y, e2.Y);
        }

        /// <summary>
        /// Checks if the given floating number is between the other two floating numbers.
        /// </summary>
        /// <param name="f1">The first number.</param>
        /// <param name="x">The number to check.</param>
        /// <param name="f2">The second number.</param>
        /// <returns>true if between, false otherwise.</returns>
        public static bool IsBetween(float f1, float x, float f2) => Math.Min(f1, f2) <= x && x <= Math.Max(f1, f2);

        /// <summary>
        /// Calculates (a) and (b) of y = ax + b given two points.
        /// </summary>
        /// <param name="s">The first point.</param>
        /// <param name="e">The second point.</param>
        /// <returns>The computed values of (a) and (b).</returns>
        private static (double a, double b) CalcLine(Vector2 s, Vector2 e) => (
            Math.Round((s.Y - e.Y) / (s.X - e.X), ROUND_PRECISION),
            Math.Round((s.X * e.Y - s.Y * e.X) / (s.X - e.X), ROUND_PRECISION));

        /// <summary>
        /// Indicates whether the given two boxes intersect.
        /// </summary>
        /// <param name="box1">The first box.</param>
        /// <param name="box2">The second box.</param>
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

            return (
                from line1 in lines1 
                from line2 in lines2 
                where LineLineCol(line1, line2) 
                select line1
                ).Any();
        }

        /// <summary>
        /// Indicates whether the given point is within the given circle.
        /// </summary>
        /// <param name="circle">A circle.</param>
        /// <param name="point">A point.</param>
        /// <returns><c>true</c> if the point is inside the circle, <c>false</c> otherwise.</returns>
        public static bool CirclePointCollision(Circle circle, Vector2 point) => (point - circle.Centre).LengthSquared() <= Math.Pow(circle.Radius, 2);
    }
}