﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class CannonInfo
    {
        public readonly int FireRate;
        public readonly int RotationSpeed;
        public readonly Texture2D Img;
        public readonly BulletInfo Bullet;
        public readonly int? BurstRate;
        public readonly int? BurstDuration;
        public CannonInfo(int fireRate, int rotationSpeed, Texture2D img, BulletInfo bullet, int? burstRate = null, int? burstDuration = null)
        {
            FireRate = fireRate;
            RotationSpeed = rotationSpeed;
            Img = img;
            Bullet = bullet;
            BurstRate = burstRate;
            BurstDuration = burstDuration;
        }
    }
}
