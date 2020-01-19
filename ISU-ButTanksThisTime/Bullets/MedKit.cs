// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-19-2020
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

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Class MedKit.
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class MedKit : Bullet
    {
        /// <summary>
        /// Gets the ex anim.
        /// </summary>
        /// <value>The ex anim.</value>
        protected override Animation ExAnim => null;

        private static readonly float scaleFactor = 0.5f;
        private static readonly Texture2D medKitImg;

        /// <summary>
        /// Initializes static members of the <see cref="MedKit"/> class.
        /// </summary>
        static MedKit() => medKitImg = Tools.Content.Load<Texture2D>("Images/Sprites/Items/HP_Bonus");

        /// <summary>
        /// Initializes a new instance of the <see cref="MedKit"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public MedKit(Vector2 position) : base(position, 0, scaleFactor, Owner.Enemy) => Damage = -300;

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Update() => IsDead;

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(medKitImg, Position, null, Color.White, 0, Vector2.Zero, scaleFactor, SpriteEffects.None, 1f);
        }

        /// <summary>
        /// Gets the rotated rectangle.
        /// </summary>
        /// <returns>RotatedRectangle.</returns>
        public override RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(new Rectangle((int) Position.X, (int) Position.Y, (int) (medKitImg.Width * 0.5f * scaleFactor), (int) (medKitImg.Height * 0.5f * scaleFactor)), 0, Vector2.Zero);

        /// <summary>
        /// Collides this instance.
        /// </summary>
        public override void Collide()
        {
            IsDead = true;
        }

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>Bullet.</returns>
        public override Bullet Clone(Vector2 pos, float rotation) => null;
        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected override Texture2D Img => medKitImg;
    }
}