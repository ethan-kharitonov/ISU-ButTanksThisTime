using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime
{
    public struct PointOrInterval
    {
        public readonly bool IsInterval;
        public readonly Vector2 Point1;
        public readonly Vector2 Point2;

        public PointOrInterval(Vector2 pt)
        {
            IsInterval = false;
            Point1 = pt;
            Point2 = pt;
        }

        public PointOrInterval(Vector2 pt1, Vector2 pt2)
        {
            IsInterval = pt1 != pt2;
            Point1 = pt1;
            Point2 = pt2;
        }
    }
}