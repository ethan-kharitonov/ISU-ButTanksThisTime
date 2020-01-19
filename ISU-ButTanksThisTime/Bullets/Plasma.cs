using Animation2D;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    internal class Plasma : Bullet
    {
        private Animation exAnim;
        private static readonly Texture2D bulletImg;

        public static readonly BulletInfo Info;
        private static int damage = 25;

        static Plasma()
        {
            bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Plasma");
            Info = new BulletInfo(bulletImg, damage);
        }

        public Plasma(Vector2 position, float rotation, Owner owner) : base(position, rotation, Tank.IMG_SCALE_FACTOR, owner)
        {
            img = bulletImg;

            var exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/LaserExplode");
            exAnim = new Animation(exSprite, 3, 1, 3, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, Tank.IMG_SCALE_FACTOR, true);

            Damage = damage;
        }

        protected override Animation ExAnim => exAnim;

        public override Bullet Clone(Vector2 pos, float rotation) => new Plasma(pos, rotation, bulletOwner);
    }
}