using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    internal class BulletInfo
    {
        public readonly Texture2D Img;
        public readonly int Damage;

        public BulletInfo(Texture2D img, int damage)
        {
            Img = img;
            Damage = damage;
        }
    }
}