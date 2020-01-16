using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ISU_ButTanksThisTime
{
    internal enum Owner
    {
        Enemie = 0,
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
        public static Rectangle screen;
        public static Random rnd = new Random();
        public const int ROUND_PERCISION = 3;
        public static Rectangle ArenaBounds;
        public static GameTime gameTime;
        public static Texture2D RedSquare;
        public static ContentManager Content;
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

        public static float Atan(float y, float x)
        {
            y = y == -0 ? 0 : y;
            float angle = (float)Math.Atan2(y, x);

            angle = angle < 0 ? angle + MathHelper.TwoPi : angle;

            return angle;
        }

        public static float RotateTowardsVectorTest(float current, Vector2 target, float rotationSpeed)
        {
            float targetAngel = MathHelper.ToDegrees(Tools.Atan(target.Y, target.X));
            float delta = targetAngel - current;

            if (delta == 0)
            {
                return current;
            }
            else if (Math.Abs(delta) < rotationSpeed)
            {
                return targetAngel;
            }


            int dir = 1;
            if (current > targetAngel)
            {
                dir = -1;
            }
            float newAngle = Math.Abs(delta) <= 180 ? current + dir * rotationSpeed : current - dir * rotationSpeed;
            newAngle = newAngle < 0 ? newAngle + 360 : newAngle;
            newAngle %= 360;

            return newAngle;
        }

        private static bool LineLineCol(Line line1, Line line2)
        {
            Vector2 s1 = line1.StartPoint;
            Vector2 e1 = line1.EndPoint;
            Vector2 s2 = line2.StartPoint;
            Vector2 e2 = line2.EndPoint;

            bool isFirstPerpendicularToAxisX = s1.X == e1.X;
            bool isSecondPerpendicularToAxisX = s2.X == e2.X;

            if (isFirstPerpendicularToAxisX && isSecondPerpendicularToAxisX)
            {
                return s1.X == s2.X && DoSameLineIntervalsNotPerpendicularToAxisYIntersect(s1, e1, s2, e2);
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

            if (a1 == a2)
            {
                return b1 == b2 && (a1 == 0
                    ? DoSameLineIntervalsPerpendicularToAxisYIntersect(s1, e1, s2, e2)   // both intervals are on a line perpendicular to the axis Y
                    : DoSameLineIntervalsNotPerpendicularToAxisYIntersect(s1, e1, s2, e2)); // both intervals are on a line that is not perpendicular to any axis
            }

            Vector2 intersection = new Vector2(
                (float)Math.Round((b2 - b1) / (a1 - a2), Tools.ROUND_PERCISION),
                (float)Math.Round((b2 * a1 - a2 * b1) / (a1 - a2), Tools.ROUND_PERCISION));

            return
                IsBetween(s1.X, intersection.X, e1.X) &&
                IsBetween(s2.X, intersection.X, e2.X) &&
                IsBetween(s1.Y, intersection.Y, e1.Y) &&
                IsBetween(s2.Y, intersection.Y, e2.Y);
        }

        private static bool DoSameLineIntervalsNotPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) =>
            IsBetween(s2.Y, s1.Y, e2.Y) || // s1 lies on the second interval
            IsBetween(s2.Y, e1.Y, e2.Y) || // e1 lies on the second interval
            IsBetween(s1.Y, s2.Y, e1.Y) || // s2 lies on the first interval
            IsBetween(s1.Y, e2.Y, e1.Y);   // e2 lies on the first interval

        private static bool DoSameLineIntervalsPerpendicularToAxisYIntersect(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) =>
            IsBetween(s2.X, s1.X, e2.X) || // s1 lies on the second interval
            IsBetween(s2.X, e1.X, e2.X) || // e1 lies on the second interval
            IsBetween(s1.X, s2.X, e1.X) || // s2 lies on the first interval
            IsBetween(s1.X, e2.X, e1.X);   // e2 lies on the first interval

        private static bool IsIntersectingWithPerpendicular(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2)
        {
            var (a2, b2) = CalcLine(s2, e2);
            var y = (float)Math.Round(a2 * s1.X + b2, Tools.ROUND_PERCISION);
            return IsBetween(s2.X, s1.X, e2.X) && IsBetween(s1.Y, y, e1.Y) && IsBetween(s2.Y, y, e2.Y);
        }

        public static bool IsBetween(float f1, float x, float f2) => Math.Min(f1, f2) <= x && x <= Math.Max(f1, f2);

        private static (double a, double b) CalcLine(Vector2 s, Vector2 e) => (
            Math.Round((s.Y - e.Y) / (s.X - e.X), Tools.ROUND_PERCISION),
            Math.Round((s.X * e.Y - s.Y * e.X) / (s.X - e.X), Tools.ROUND_PERCISION));

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

            foreach (Line line1 in lines1)
            {
                foreach (Line line2 in lines2)
                {
                    if (LineLineCol(line1, line2))
                    {
                        return true;
                    }
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

            foreach (Line lineB in lines)
            {
                if (LineLineCol(lineB, line))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
