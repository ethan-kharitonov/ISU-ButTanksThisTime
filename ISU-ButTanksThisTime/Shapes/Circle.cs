using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime.Shapes
{
    public class Circle
    {
        public Vector2 Centre;
        public readonly float Radius;

        public Circle(Vector2 centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }
    }
}