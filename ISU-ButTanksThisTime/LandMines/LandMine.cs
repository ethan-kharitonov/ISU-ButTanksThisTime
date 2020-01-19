using Animation2D;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.LandMines
{
    internal abstract class LandMine
    {
        protected readonly Animation[] Animations = new Animation[3];
        private int currentAnim;
        private bool active;
        private readonly int radius;
        private readonly float explosionRadius;

        protected LandMine(int radius, float explosionRadius)
        {
            this.radius = radius;
            this.explosionRadius = explosionRadius;
        }

        public bool Update()
        {
            Animations[currentAnim].Update(Tools.GameTime);
            if (currentAnim == 1 && !Animations[currentAnim].isAnimating)
            {
                currentAnim = 2;
                active = true;
            }

            return currentAnim == 2 && !Animations[currentAnim].isAnimating;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Animations[currentAnim].Draw(spriteBatch, Color.White, SpriteEffects.None);
        }

        public void Collide()
        {
            currentAnim = currentAnim == 0 ? 1 : currentAnim;
        }

        public RotatedRectangle GetBox()
        {
            var fullBox = Animations[currentAnim].destRec;
            var box = new Rectangle(fullBox.Center.X - radius / 2, fullBox.Center.Y - radius / 2, radius, radius);
            return new RotatedRectangle(box, 0, Vector2.Zero);
        }

        public Circle GetExplosionArea()
        {
            var centre = Animations[currentAnim].destRec.Center.ToVector2();
            return new Circle(centre, explosionRadius);
        }

        public bool IsActive() => active;
    }
}