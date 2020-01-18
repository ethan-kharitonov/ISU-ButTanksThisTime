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
    class Plasma : Bullet
    {
        private Animation exAnim;
        private readonly static Texture2D bulletImg;

        public static readonly BulletInfo Info;
        static Plasma()
        {
            bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Plasma");
            Info = new BulletInfo(bulletImg, 0);
        }
        public Plasma(Vector2 position, float rotation, Owner owner) : base(position, rotation, Tank.IMG_SCALE_FACTOR, owner)
        {
            img = bulletImg;

            Texture2D exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/LaserExplode");
            exAnim = new Animation(exSprite, 3, 1, 3, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, Tank.IMG_SCALE_FACTOR, true);
        }

        protected override Animation ExAnim => exAnim;

        public override Bullet Clone(Vector2 pos, float rotation)
        {
            return new Plasma(pos, rotation, bulletOwner);
        }
    }
}
