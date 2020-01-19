using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ISU_ButTanksThisTime.Shapes;

namespace ISU_ButTanksThisTime
{
    internal enum Owner
    {
        Enemy = 0,
        Player = 1
    }

    internal enum Stage
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Player = 3
    }

    public static class Tools
    {
        public static Rectangle Screen;
        public static Random Rnd = new Random();
        public const int ROUND_PRECISION = 3;
        public static Rectangle ArenaBounds;

        public static GameTime GameTime;

        //public static Texture2D RedSquare;
        public static ContentManager Content;
        public static GraphicsDevice Graphics;
        public static Vector2 TrueMousePos;
        public static SpriteFont Font;
        public static Texture2D ButtonImg;
        public const float TOLERANCE = 0.00001F;

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

        private static float Atan(float y, float x)
        {
            var angle = (float) Math.Atan2(y, x);
            angle = angle < 0 ? angle + MathHelper.TwoPi : angle;
            return angle;
        }

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

        private static PointOrInterval? LineLineCol(Line line1, Line line2)
        {
            var s1 = line1.StartPoint;
            var e1 = line1.EndPoint;
            var s2 = line2.StartPoint;
            var e2 = line2.EndPoint;

            var isFirstPerpendicularToAxisX = Math.Abs(s1.X - e1.X) < TOLERANCE;
            var isSecondPerpendicularToAxisX = Math.Abs(s2.X - e2.X) < TOLERANCE;

            if (isFirstPerpendicularToAxisX && isSecondPerpendicularToAxisX)
            {
                return Math.Abs(s1.X - s2.X) < TOLERANCE
                    ? DoSameLineIntervalsNotPerpendicularToAxisYIntersect(s1, e1, s2, e2)
                    : null;
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
                return Math.Abs(b1 - b2) < TOLERANCE
                    ? Math.Abs(a1) < TOLERANCE
                        ? DoSameLineIntervalsPerpendicularToAxisYIntersect(s1, e1, s2, e2) // both intervals are on a line perpendicular to the axis Y
                        : DoSameLineIntervalsNotPerpendicularToAxisYIntersect(s1, e1, s2, e2)
                    : null;
            }

            var intersection = new Vector2(
                (float) Math.Round((b2 - b1) / (a1 - a2), ROUND_PRECISION),
                (float) Math.Round((b2 * a1 - a2 * b1) / (a1 - a2), ROUND_PRECISION));

            return
                IsBetween(s1.X, intersection.X, e1.X) &&
                IsBetween(s2.X, intersection.X, e2.X) &&
                IsBetween(s1.Y, intersection.Y, e1.Y) &&
                IsBetween(s2.Y, intersection.Y, e2.Y)
                    ? new PointOrInterval(new Vector2(intersection.X, intersection.Y))
                    : default(PointOrInterval?);
        }

        #region CalculateIntersectionResultFinal

        private static PointOrInterval? CalculateIntersectionResult(
            bool s1BelongsToInterval2, bool e1BelongsToInterval2,
            bool s2BelongsToInterval1, bool e2BelongsToInterval1,
            Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            if (s1BelongsToInterval2)
            {
                if (e1BelongsToInterval2)
                {
                    return new PointOrInterval(s1, e1);
                }

                if (s2BelongsToInterval1)
                {
                    return new PointOrInterval(s1, s2);
                }

                Debug.Assert(e2BelongsToInterval1);
                return new PointOrInterval(s1, e2);
            }

            return null;
        }

        private static PointOrInterval? CalculateIntersectionResultFinal(
            bool s1BelongsToInterval2, bool e1BelongsToInterval2,
            bool s2BelongsToInterval1, bool e2BelongsToInterval1,
            Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var res = CalculateIntersectionResult(
                s1BelongsToInterval2, e1BelongsToInterval2,
                s2BelongsToInterval1, e2BelongsToInterval1,
                s1, e1, s2, e2);
            if (res == null)
            {
                res = CalculateIntersectionResult(
                    e1BelongsToInterval2, s1BelongsToInterval2,
                    s2BelongsToInterval1, e2BelongsToInterval1,
                    e1, s1, s2, e2);
                if (res == null)
                {
                    res = CalculateIntersectionResult(
                        s2BelongsToInterval1, e2BelongsToInterval1,
                        s1BelongsToInterval2, e1BelongsToInterval2,
                        s2, e2, s1, e1);
                    if (res == null)
                    {
                        res = CalculateIntersectionResult(
                            e2BelongsToInterval1, s2BelongsToInterval1,
                            s1BelongsToInterval2, e1BelongsToInterval2,
                            e2, s2, s1, e1);
                    }
                }
            }

            return res;
        }

        #endregion

        private static PointOrInterval? DoSameLineIntervalsNotPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var s1BelongsToInterval2 = IsBetween(s2.Y, s1.Y, e2.Y);
            var e1BelongsToInterval2 = IsBetween(s2.Y, e1.Y, e2.Y);
            var s2BelongsToInterval1 = IsBetween(s1.Y, s2.Y, e1.Y);
            var e2BelongsToInterval1 = IsBetween(s1.Y, e2.Y, e1.Y);

            return CalculateIntersectionResultFinal(
                s1BelongsToInterval2, e1BelongsToInterval2,
                s2BelongsToInterval1, e2BelongsToInterval1,
                s1, e1, s2, e2);
        }

        private static PointOrInterval? DoSameLineIntervalsPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var s1BelongsToInterval2 = IsBetween(s2.X, s1.X, e2.X);
            var e1BelongsToInterval2 = IsBetween(s2.X, e1.X, e2.X);
            var s2BelongsToInterval1 = IsBetween(s1.X, s2.X, e1.X);
            var e2BelongsToInterval1 = IsBetween(s1.X, e2.X, e1.X);

            return CalculateIntersectionResultFinal(
                s1BelongsToInterval2, e1BelongsToInterval2,
                s2BelongsToInterval1, e2BelongsToInterval1,
                s1, e1, s2, e2);
        }

        private static PointOrInterval? IsIntersectingWithPerpendicular(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var (a2, b2) = CalcLine(s2, e2);
            var y = (float) Math.Round(a2 * s1.X + b2, ROUND_PRECISION);
            return IsBetween(s2.X, s1.X, e2.X) && IsBetween(s1.Y, y, e1.Y) && IsBetween(s2.Y, y, e2.Y)
                ? new PointOrInterval(new Vector2(s1.X, y))
                : default(PointOrInterval?);
        }

        public static bool IsBetween(float f1, float x, float f2) => Math.Min(f1, f2) <= x && x <= Math.Max(f1, f2);

        private static (double a, double b) CalcLine(Vector2 s, Vector2 e) =>
        (
            Math.Round((s.Y - e.Y) / (s.X - e.X), ROUND_PRECISION),
            Math.Round((s.X * e.Y - s.Y * e.X) / (s.X - e.X), ROUND_PRECISION));

        public static List<LineIntersectionResult> BoxBoxCollision(RotatedRectangle box1, RotatedRectangle box2)
        {
            Line[] lines1 =
            {
                new Line(box1.TopRight, box1.TopLeft),
                new Line(box1.BotomRight, box1.BotomLeft),
                new Line(box1.TopRight, box1.BotomRight),
                new Line(box1.TopLeft, box1.BotomLeft),
            };

            Line[] lines2 =
            {
                new Line(box2.TopRight, box2.TopLeft),
                new Line(box2.BotomRight, box2.BotomLeft),
                new Line(box2.TopRight, box2.BotomRight),
                new Line(box2.TopLeft, box2.BotomLeft),
            };

            List<LineIntersectionResult> res = null;
            foreach (var line1 in lines1)
            foreach (var line2 in lines2)
            {
                var pointOrInterval = LineLineCol(line1, line2);
                if (pointOrInterval != null)
                {
                    if (res == null)
                    {
                        res = new List<LineIntersectionResult>();
                    }

                    res.Add(new LineIntersectionResult(line1, line2, pointOrInterval.Value));
                }
            }

            return res;
        }

        public static List<LineIntersectionResult> LineBoxCollision(Line line, RotatedRectangle box)
        {
            Line[] lines =
            {
                new Line(box.TopRight, box.TopLeft),
                new Line(box.BotomRight, box.BotomLeft),
                new Line(box.TopRight, box.BotomRight),
                new Line(box.TopLeft, box.BotomLeft),
            };

            List<LineIntersectionResult> res = null;
            foreach (var lineB in lines)
            {
                var pointOrInterval = LineLineCol(lineB, line);
                if (pointOrInterval != null)
                {
                    if (res == null)
                    {
                        res = new List<LineIntersectionResult>();
                    }

                    res.Add(new LineIntersectionResult(lineB, line, pointOrInterval.Value));
                }
            }

            return res;
        }

        public static bool CirclePointCollision(Circle circle, Vector2 point)
        {
            if ((point - circle.Centre).LengthSquared() <= Math.Pow(circle.Raduis, 2))
            {
                return true;
            }

            return false;
        }
    }
}