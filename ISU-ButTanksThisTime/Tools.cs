using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        private static bool DoSameLineIntervalsNotPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) => 
            IsBetween(s2.Y, s1.Y, e2.Y) ||  // s1 is between s2 and e2
            IsBetween(s2.Y, e1.Y, e2.Y) ||  // e1 is between s2 and e2
            IsBetween(s1.Y, s2.Y, e1.Y) ||  // s2 is between s1 and e1
            IsBetween(s1.Y, e2.Y, e1.Y);    // e2 is between s1 and e1

        private static bool DoSameLineIntervalsPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) =>
            IsBetween(s2.X, s1.X, e2.X) ||  // s1 is between s2 and e2
            IsBetween(s2.X, e1.X, e2.X) ||  // e1 is between s2 and e2
            IsBetween(s1.X, s2.X, e1.X) ||  // s2 is between s1 and e1
            IsBetween(s1.X, e2.X, e1.X);    // e2 is between s1 and e1

        private static bool IsIntersectingWithPerpendicular(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var (a2, b2) = CalcLine(s2, e2);
            var y = (float) Math.Round(a2 * s1.X + b2, ROUND_PRECISION);
            return IsBetween(s2.X, s1.X, e2.X) && IsBetween(s1.Y, y, e1.Y) && IsBetween(s2.Y, y, e2.Y);
        }

        public static bool IsBetween(float f1, float x, float f2) => Math.Min(f1, f2) <= x && x <= Math.Max(f1, f2);

        private static (double a, double b) CalcLine(Vector2 s, Vector2 e) =>
        (
            Math.Round((s.Y - e.Y) / (s.X - e.X), ROUND_PRECISION),
            Math.Round((s.X * e.Y - s.Y * e.X) / (s.X - e.X), ROUND_PRECISION));

        public static bool BoxBoxCollision(RotatedRectangle box1, RotatedRectangle box2)
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

        public static bool LineBoxCollision(Line line, RotatedRectangle box)
        {
            Line[] lines =
            {
                new Line(box.TopRight, box.TopLeft),
                new Line(box.BotomRight, box.BotomLeft),
                new Line(box.TopRight, box.BotomRight),
                new Line(box.TopLeft, box.BotomLeft),
            };

            foreach (var lineB in lines)
            {
                if (LineLineCol(lineB, line))
                {
                    return true;
                }
            }

            return false;
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