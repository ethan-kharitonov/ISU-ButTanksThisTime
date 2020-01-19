// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Animation2D;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.LandMines
{
    /// <summary>
    /// Class LandMine.
    /// </summary>
    internal abstract class LandMine
    {
        protected readonly Animation[] Animations = new Animation[3];
        private int currentAnim;
        private bool active;
        private readonly int radius;
        private readonly float explosionRadius;

        /// <summary>
        /// Initializes a new instance of the <see cref="LandMine"/> class.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="explosionRadius">The explosion radius.</param>
        protected LandMine(int radius, float explosionRadius)
        {
            this.radius = radius;
            this.explosionRadius = explosionRadius;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Animations[currentAnim].Draw(spriteBatch, Color.White, SpriteEffects.None);
        }

        /// <summary>
        /// Collides this instance.
        /// </summary>
        public void Collide()
        {
            currentAnim = currentAnim == 0 ? 1 : currentAnim;
        }

        /// <summary>
        /// Gets the box.
        /// </summary>
        /// <returns>RotatedRectangle.</returns>
        public RotatedRectangle GetBox()
        {
            var fullBox = Animations[currentAnim].destRec;
            var box = new Rectangle(fullBox.Center.X - radius / 2, fullBox.Center.Y - radius / 2, radius, radius);
            return new RotatedRectangle(box, 0, Vector2.Zero);
        }

        /// <summary>
        /// Gets the explosion area.
        /// </summary>
        /// <returns>Circle.</returns>
        public Circle GetExplosionArea()
        {
            var centre = Animations[currentAnim].destRec.Center.ToVector2();
            return new Circle(centre, explosionRadius);
        }

        /// <summary>
        /// Determines whether this instance is active.
        /// </summary>
        /// <returns><c>true</c> if this instance is active; otherwise, <c>false</c>.</returns>
        public bool IsActive() => active;
    }
}