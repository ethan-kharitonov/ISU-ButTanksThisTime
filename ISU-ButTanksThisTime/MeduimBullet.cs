using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class MeduimBullet : Bullet
    {
        public MeduimBullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner) : base(position, rotation, scaleFactor, bulletOwner)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
        }
    }
}
