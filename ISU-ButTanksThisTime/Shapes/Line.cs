using Microsoft.Xna.Framework;
namespace ISU_ButTanksThisTime
{
    public class Line
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
        public Line(Vector2 startPoint, Vector2 endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public override string ToString() => $"{{{StartPoint.X}, {StartPoint.Y}}},{{{EndPoint.X}, {EndPoint.Y}}}";
    }
}
