using System;
using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime.Shapes
{
    public class RotatedRectangle
    {
        public readonly Vector2 TopLeft;
        public readonly Vector2 TopRight;
        public readonly Vector2 BottomLeft;
        public readonly Vector2 BottomRight;

        public RotatedRectangle(Rectangle box, float rotation, Vector2 origin)
        {
            box.X -= (int) origin.X;
            box.Y -= (int) origin.Y;
            box.Center.ToVector2();

            var originalAngle = Math.Atan2(-box.Height / 2.0, box.Width / 2.0);
            double distance = new Vector2(box.Height / 2f, box.Width / 2f).Length();

            TopRight = CalcVertex(originalAngle, rotation, distance, box.Center.ToVector2());
            TopLeft = CalcVertex(MathHelper.ToRadians(180) - originalAngle, rotation, distance, box.Center.ToVector2());
            BottomRight = CalcVertex(-originalAngle, rotation, distance, box.Center.ToVector2());
            BottomLeft = CalcVertex(MathHelper.ToRadians(180) + originalAngle, rotation, distance, box.Center.ToVector2());
        }

        private Vector2 CalcVertex(double originalAngle, double rotation, double distFromCentre, Vector2 centre)
        {
            var vertex = new Vector2((float) Math.Cos(originalAngle + rotation), (float) -Math.Sin(originalAngle + rotation));
            vertex *= (float) distFromCentre;
            vertex += centre;

            return vertex;
        }
    }
}