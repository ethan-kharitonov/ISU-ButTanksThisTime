// Author        : Ethan Kharitonov
// File Name     : Bullets\Laser.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the Laser type of a bullet.
using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Implements the Laser type of a bullet.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class Laser : Bullet
    {
        //the factor by wich the bullet image is scaled
        private readonly float scaleFactor;

        /// <summary>
        /// The laser type information.
        /// </summary>
        public static readonly BulletTypeInfo Info;

        //the damage this bullet gives off when it hits
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
            //populate the bullet info
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
            //saves the given scale factor
            this.scaleFactor = scaleFactor;

            //Implements the bullet explosion animation and damage
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