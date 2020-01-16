using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
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
