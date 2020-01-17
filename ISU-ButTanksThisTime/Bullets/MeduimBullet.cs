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
        private readonly float scaleFactor;
        public MeduimBullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner) : base(position, rotation, scaleFactor, bulletOwner)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
            this.scaleFactor = scaleFactor;
        }

        public override Bullet Clone(Vector2 pos, float rotation)
        {
            return new MeduimBullet(pos, rotation, scaleFactor, bulletOwner);
        }
    }
}
