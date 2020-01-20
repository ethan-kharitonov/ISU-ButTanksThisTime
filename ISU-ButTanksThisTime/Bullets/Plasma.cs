// Author        : Ethan Kharitonov
// File Name     : Bullets\Plasma.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the Plasma class.
using Animation2D;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// The Plasma type.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class Plasma : Bullet
    {
        /// <summary>
        /// The plasma type information.
        /// </summary>
        public static readonly BulletTypeInfo Info;

        //the damage this bullet gives off when it hits
        private const int DAMAGE = 25;

        /// <summary>
        /// Initializes static members of the <see cref="Plasma"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static Plasma()
        {
            //Populate the bullet info
            var bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Plasma");
            Info = new BulletTypeInfo(bulletImg, DAMAGE);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plasma"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="owner">The owner.</param>
        /// <seealso cref="Owner"/>
        public Plasma(Vector2 position, float rotation, Owner owner) : base(position, rotation, Tank.IMG_SCALE_FACTOR, owner)
        {
            //Implements the bullet explosion animation and damage
            var exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/LaserExplode");
            ExAnim = new Animation(exSprite, 3, 1, 3, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, Tank.IMG_SCALE_FACTOR, true);
            Damage = DAMAGE;
        }

        /// <summary>
        /// Implements the abstract <see cref="Bullet.ExAnim"/> property.
        /// </summary>
        protected override Animation ExAnim { get; }

        /// <summary>
        /// Clones the current plasma object to the specified position.
        /// <para>
        /// Overrides the abstract <see cref="Bullet.Clone"/> method.
        /// </para>
        /// </summary>
        public override Bullet Clone(Vector2 pos, float rotation) => new Plasma(pos, rotation, BulletOwner);

        /// <summary>
        /// Overrides the abstract <see cref="Bullet.Img"/> property.
        /// </summary>
        /// <value>The plasma image.</value>
        protected override Texture2D Img => Info.Img;
    }
}