using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class FireBullet : Bullet
    {
        private Animation exAnim;

        public FireBullet(Vector2 position, float rotation, Owner owner) : base(position, rotation, Tank.IMG_SCALE_FACTOR, owner)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/FireBullet");

            Texture2D exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/FireBulletHit");
            exAnim = new Animation(exSprite, 4, 1, 4, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, Tank.IMG_SCALE_FACTOR, true);
        }

        protected override Animation ExAnim => exAnim;

        public override Bullet Clone(Vector2 pos, float rotation)
        {
            return new FireBullet(pos, rotation, bulletOwner);
        }
    }
}
