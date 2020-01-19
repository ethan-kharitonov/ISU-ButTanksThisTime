using ISU_ButTanksThisTime.Shapes;

namespace ISU_ButTanksThisTime
{
    public struct LineIntersectionResult
    {
        public readonly Line Line1;
        public readonly Line Line2;
        public readonly PointOrInterval Intersection;

        public LineIntersectionResult(Line line1, Line line2, PointOrInterval intersection)
        {
            Line1 = line1;
            Line2 = line2;
            Intersection = intersection;
        }
    }
}