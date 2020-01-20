// Author        : Ethan Kharitonov
// File Name     : Bullets\MedKit.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the Med Kit type.
using System;
using Animation2D;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Implements the Med Kit type.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class MedKit : Bullet
    {
        private const int DAMAGE = -300;

        private static readonly float scaleFactor = 0.5f;
        private static readonly Texture2D medKitImg;

        /// <summary>
        /// Initializes static members of the <see cref="MedKit"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static MedKit() => medKitImg = Tools.Content.Load<Texture2D>("Images/Sprites/Items/HP_Bonus");

        /// <summary>
        /// Initializes a new instance of the <see cref="MedKit"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        public MedKit(Vector2 position) : base(position, 0, scaleFactor, Owner.Enemy) => Damage = DAMAGE;

        /// <summary>
        /// Updates the state of this Med Kit object.
        /// </summary>
        /// <returns><c>true</c> if the object is dead, i.e. the kit was taken by the player, <c>false</c> otherwise.</returns>
        public override bool Update() => IsDead;

        /// <summary>
        /// Draws the specified Med Kit object.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(medKitImg, Position, null, Color.White, 0, Vector2.Zero, scaleFactor, SpriteEffects.None, 1f);
        }

        /// <summary>
        /// Gets the bounding box of this Med Kit object.
        /// </summary>
        public override RotatedRectangle GetRotatedRectangle() => new RotatedRectangle(new Rectangle((int) Position.X, (int) Position.Y, (int) (medKitImg.Width * 0.5f * scaleFactor), (int) (medKitImg.Height * 0.5f * scaleFactor)), 0, Vector2.Zero);

        /// <summary>
        /// Called to indicate that this MedKit object was picked up by the player.
        /// </summary>
        public override void Collide() => IsDead = true;

        /// <summary>
        /// Implements the abstract <see cref="Bullet.ExAnim"/> property.
        /// </summary>
        /// <value>Always <c>null</c>.</value>
        protected override Animation ExAnim => null;

        /// <summary>
        /// Not supported.
        /// </summary>
        public override Bullet Clone(Vector2 pos, float rotation) => throw new NotSupportedException();

        /// <summary>
        /// Overrides the abstract <see cref="Bullet.Img"/> property.
        /// </summary>
        /// <value>The med kit image.</value>
        protected override Texture2D Img => medKitImg;
    }
}