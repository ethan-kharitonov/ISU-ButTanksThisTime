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
    /// Implements the core functionality of a land mine object.
    /// </summary>
    internal abstract class LandMine
    {

        //the index for the triggered and explode animation
        private const int TRIGGERED_ANIM = 1;
        private const int EXPLODE_ANIM = 2;

        //store the idle, triggered and exploding animation and the current animation
        protected readonly Animation[] Animations = new Animation[3];
        private int currentAnim;

        //store the raduis of the land mine and the raduis of the area effected by its explosion
        private readonly int radius;
        private readonly float explosionRadius;

        /// <summary>
        /// Initializes a new instance of the <see cref="LandMine"/> class.
        /// </summary>
        /// <param name="radius">The land mine radius.</param>
        /// <param name="explosionRadius">The explosion radius.</param>
        protected LandMine(int radius, float explosionRadius)
        {
            //save raduis and explosion raduis to local variables
            this.radius = radius;
            this.explosionRadius = explosionRadius;
        }

        /// <summary>
        /// Updates the state of this land mine object.
        /// </summary>
        /// <returns><c>true</c> if the land mine should be removed, i.e. if it was activated and finished animating, <c>false</c> otherwise.</returns>
        public bool Update()
        {
            //update the current animation
            Animations[currentAnim].Update(Tools.GameTime);

            // if triggered is done animating set 
            if (currentAnim == TRIGGERED_ANIM && !Animations[currentAnim].isAnimating)
            {
                //set current animation to explode animation and set active to true
                currentAnim = EXPLODE_ANIM;
                IsActive = true;
            }

            //returs true if done animating its last stage
            return currentAnim == EXPLODE_ANIM && !Animations[currentAnim].isAnimating;
        }

        /// <summary>
        /// Draws this land mine.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //draw the current animation
            Animations[currentAnim].Draw(spriteBatch, Color.White, SpriteEffects.None);
        }

        /// <summary>
        /// Starts the animations upon collision with the player.
        /// </summary>
        public void Collide()
        {
            //if the land mine was nit previously animating set it to triggered
            currentAnim = currentAnim == 0 ? 1 : currentAnim;
        }

        /// <summary>
        /// Gets the bounding box corresponding to the current animation.
        /// </summary>
        /// <returns>RotatedRectangle.</returns>
        public RotatedRectangle GetBox()
        {
            var fullBox = Animations[currentAnim].destRec;
            var box = new Rectangle(fullBox.Center.X - radius / 2, fullBox.Center.Y - radius / 2, radius, radius);
            return new RotatedRectangle(box, 0, Vector2.Zero);
        }

        /// <summary>
        /// Gets the circle encompassing the explosion area.
        /// </summary>
        public Circle GetExplosionArea()
        {
            var centre = Animations[currentAnim].destRec.Center.ToVector2();
            return new Circle(centre, explosionRadius);
        }

        /// <summary>
        /// Indicates whether this land mine is active.
        /// </summary>
        public bool IsActive { get; private set; }
    }
}