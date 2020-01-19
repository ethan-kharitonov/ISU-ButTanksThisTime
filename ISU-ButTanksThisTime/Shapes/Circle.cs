using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime.Shapes
{
    public class Circle
    {
        public Vector2 Centre;
        public float Raduis;

        public Circle(Vector2 centre, float raduis)
        {
            Centre = centre;
            Raduis = raduis;
        }
    }
}