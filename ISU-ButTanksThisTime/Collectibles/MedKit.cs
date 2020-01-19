using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    class MedKit : Bullet
    {
        protected override Animation ExAnim => null;

        private static float SCALE_FACTOR = 0.5f;

        public MedKit(Vector2 position) : base(position, 0, SCALE_FACTOR, Owner.Enemie)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/HP_Bonus");
            Damage = -100;
        }

        public override bool Update()
        {
            return IsDead;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, position, null, Color.White, 0, Vector2.Zero, SCALE_FACTOR, SpriteEffects.None, 1f);

        }

        public override RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(new Rectangle((int)(position.X), (int)(position.Y), (int)(img.Width * 0.5f * SCALE_FACTOR), (int)(img.Height * 0.5f * SCALE_FACTOR)), 0, Vector2.Zero);

        public override void Collide()
        {
            IsDead = true;
        }

        public override Bullet Clone(Vector2 pos, float rotation)
        {
            return null;
        }
    }
}
