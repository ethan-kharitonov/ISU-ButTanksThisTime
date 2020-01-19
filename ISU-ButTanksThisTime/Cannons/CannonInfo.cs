using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal class CannonInfo
    {
        public readonly int FireRate;
        public readonly int RotationSpeed;
        public readonly Texture2D Img;
        public readonly BulletInfo Bullet;
        public readonly Cannon cannon;
        public readonly int? BurstRate;
        public readonly int? BurstDuration;

        public CannonInfo(int fireRate, int rotationSpeed, Texture2D img, BulletInfo bullet,
            Cannon cannon,
            int? burstRate = null, int? burstDuration = null)
        {
            FireRate = fireRate;
            RotationSpeed = rotationSpeed;
            Img = img;
            Bullet = bullet;
            this.cannon = cannon;
            BurstRate = burstRate;
            BurstDuration = burstDuration;
        }
    }
}