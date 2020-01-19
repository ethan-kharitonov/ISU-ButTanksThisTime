using Animation2D;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    internal class MedKit : Bullet
    {
        protected override Animation ExAnim => null;

        private static readonly float scaleFactor = 0.5f;
        private static readonly Texture2D medKitImg;

        static MedKit() => medKitImg = Tools.Content.Load<Texture2D>("Images/Sprites/Items/HP_Bonus");

        public MedKit(Vector2 position) : base(position, 0, scaleFactor, Owner.Enemie) => Damage = -300;

        public override bool Update() => IsDead;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(medKitImg, Position, null, Color.White, 0, Vector2.Zero, scaleFactor, SpriteEffects.None, 1f);
        }

        public override RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(new Rectangle((int) Position.X, (int) Position.Y, (int) (medKitImg.Width * 0.5f * scaleFactor), (int) (medKitImg.Height * 0.5f * scaleFactor)), 0, Vector2.Zero);

        public override void Collide()
        {
            IsDead = true;
        }

        public override Bullet Clone(Vector2 pos, float rotation) => null;
        protected override Texture2D Img => medKitImg;
    }
}