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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// The Laser type.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class Laser : Bullet
    {
        private readonly float scaleFactor;

        /// <summary>
        /// The laser type information.
        /// </summary>
        public static readonly BulletTypeInfo Info;
        private const int DAMAGE = 35;

        /// <summary>
        /// Initializes static members of the <see cref="Laser"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static Laser()
        {
            var bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Laser");
            Info = new BulletTypeInfo(bulletImg, DAMAGE);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Laser"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="owner">The laser owner.</param>
        /// <seealso cref="Owner"/>
        public Laser(Vector2 position, float rotation, float scaleFactor, Owner owner) : base(position, rotation, scaleFactor, owner)
        {
            this.scaleFactor = scaleFactor;

            var exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/LaserExplode");
            ExAnim = new Animation(exSprite, 3, 1, 3, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, scaleFactor, true);

            Damage = DAMAGE;
        }

        /// <summary>
        /// Implements the abstract <see cref="Bullet.ExAnim"/> property.
        /// </summary>
        protected override Animation ExAnim { get; }

        /// <summary>
        /// Clones the current laser object to the specified position.
        /// <para>
        /// Overrides the abstract <see cref="Bullet.Clone"/> method.
        /// </para>
        /// </summary>
        public override Bullet Clone(Vector2 pos, float rotation) => new Laser(pos, rotation, scaleFactor, BulletOwner);

        /// <summary>
        /// Overrides the abstract <see cref="Bullet.Img"/> property.
        /// </summary>
        /// <value>The laser image.</value>
        protected override Texture2D Img => Info.Img;
    }
}