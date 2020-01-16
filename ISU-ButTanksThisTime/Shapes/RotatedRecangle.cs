using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    public class RotatedRectangle
    {
        public readonly Vector2 TopLeft;
        public readonly Vector2 TopRight;
        public readonly Vector2 BotomLeft;
        public readonly Vector2 BotomRight;

        public readonly Vector2 Centre;
        public readonly int Width;
        public readonly int Height;
        public readonly double raduis;

        public RotatedRectangle(Rectangle box, float rotation, Vector2 origin)
        {
            Width = box.Width;
            Height = box.Height;
            raduis = (float)Math.Sqrt(Math.Pow(Height / 2.0, 2) + Math.Pow(Width / 2.0, 2));

            box.X -= (int)origin.X;
            box.Y -= (int)origin.Y;
            Centre = box.Center.ToVector2();


            double originalAngle = Math.Atan2(-box.Height / 2.0, box.Width / 2.0);
            double distance = new Vector2(box.Height / 2f, box.Width / 2f).Length();

            TopRight = CalcVertex(originalAngle, rotation, distance, box.Center.ToVector2());
            TopLeft = CalcVertex(MathHelper.ToRadians(180) - originalAngle, rotation, distance, box.Center.ToVector2());
            BotomRight = CalcVertex(-originalAngle, rotation, distance, box.Center.ToVector2());
            BotomLeft = CalcVertex(MathHelper.ToRadians(180) + originalAngle, rotation, distance, box.Center.ToVector2());
        }

        private Vector2 CalcVertex(double originalAngle, double rotation, double distFromCentre, Vector2 centre)
        {
            Vector2 vertex = new Vector2((float)Math.Cos(originalAngle + rotation), (float)-Math.Sin(originalAngle + rotation));
            vertex *= (float)distFromCentre;
            vertex += centre;

            return vertex;
        }

    }
}
