using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class BulletInfo
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
