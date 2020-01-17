using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class Plasma : Bullet
    {
        public Plasma(Vector2 position, float rotation, Owner owner) : base(position, rotation, Tank.IMG_SCALE_FACTOR, owner)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Plasma");
        }

        public override Bullet Clone(Vector2 pos, float rotation)
        {
            return new Plasma(pos, rotation, bulletOwner);
        }
    }
}
