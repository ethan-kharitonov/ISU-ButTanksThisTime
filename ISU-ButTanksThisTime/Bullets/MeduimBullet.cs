using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    internal class MeduimBullet : Bullet
    {
        private readonly float scaleFactor;
        private readonly Animation exAnim;
        private static readonly Texture2D bulletImg;

        public static readonly BulletInfo Info;
        private static int damage = 40;

        static MeduimBullet()
        {
            bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
            Info = new BulletInfo(bulletImg, damage);
        }

        public MeduimBullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner) : base(position, rotation, scaleFactor, bulletOwner)
        {
            this.scaleFactor = scaleFactor;

            var exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/BulletExplode");
            exAnim = new Animation(exSprite, 3, 1, 3, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, scaleFactor, true);

            Damage = damage;
        }

        protected override Animation ExAnim => exAnim;

        public override Bullet Clone(Vector2 pos, float rotation) => new MeduimBullet(pos, rotation, scaleFactor, bulletOwner);
        protected override Texture2D Img => bulletImg;
    }
}