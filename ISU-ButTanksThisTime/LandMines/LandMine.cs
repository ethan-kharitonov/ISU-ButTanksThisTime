using Animation2D;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.LandMines
{
    internal abstract class LandMine
    {
        protected Animation[] animations = new Animation[3];
        private int currentAnim = 0;
        private bool active = false;
        private int raduis;
        private float explosionRaduis;

        public LandMine(int raduis, float explosionRaduis, int damage)
        {
            this.raduis = raduis;
            this.explosionRaduis = explosionRaduis;
        }

        public bool Update()
        {
            animations[currentAnim].Update(Tools.GameTime);
            if (currentAnim == 1 && !animations[currentAnim].isAnimating)
            {
                currentAnim = 2;
                active = true;
            }

            return currentAnim == 2 && !animations[currentAnim].isAnimating;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animations[currentAnim].Draw(spriteBatch, Color.White, SpriteEffects.None);
        }

        public void Collide()
        {
            currentAnim = currentAnim == 0 ? 1 : currentAnim;
        }

        public RotatedRectangle GetBox()
        {
            var fullBox = animations[currentAnim].destRec;
            var box = new Rectangle(fullBox.Center.X - raduis / 2, fullBox.Center.Y - raduis / 2, raduis, raduis);
            return new RotatedRectangle(box, 0, Vector2.Zero);
        }

        public Circle GetExplosionArea()
        {
            var centre = animations[currentAnim].destRec.Center.ToVector2();
            return new Circle(centre, explosionRaduis);
        }

        public bool IsActive() => active;
    }
}