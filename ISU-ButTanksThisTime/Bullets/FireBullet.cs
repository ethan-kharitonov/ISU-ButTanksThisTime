// Author        : Ethan Kharitonov
// File Name     : Bullets\FireBullet.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the Fire Bullet type.
using Animation2D;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Implements the Fire Bullet type.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class FireBullet : Bullet
    {
        /// <summary>
        /// The fire bullet type information.
        /// </summary>
        public static readonly BulletTypeInfo Info;

        //the damage this bullet gives off when it hits
        private const int DAMAGE = 30;

        /// <summary>
        /// Initializes static members of the <see cref="FireBullet"/> class.
        /// </summary>
        /// <remarks>
        /// Using the static constructor guarantees that the static state is initialized right before the class is used. Without it, the static state
        /// could be initialized too early, e.g. when the content has not been loaded yet.
        /// </remarks>
        static FireBullet()
        {
            //Populates the bullet info
            var bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/FireBullet");
            Info = new BulletTypeInfo(bulletImg, DAMAGE);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireBullet"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="owner">The bullet owner.</param>
        /// <seealso cref="Owner"/>
        public FireBullet(Vector2 position, float rotation, Owner owner) : base(position, rotation, Tank.IMG_SCALE_FACTOR, owner)
        {
            //Implements bullet explosion animation and damage
            var exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/FireBulletHit");
            ExAnim = new Animation(exSprite, 4, 1, 4, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, Tank.IMG_SCALE_FACTOR, true);
            Damage = DAMAGE;
        }

        /// <summary>
        /// Implements the abstract <see cref="Bullet.ExAnim"/> property.
        /// </summary>
        protected override Animation ExAnim { get; }

        /// <summary>
        /// Clones the current fire bullet object to the specified position and rotation.
        /// <para>
        /// Overrides the abstract <see cref="Bullet.Clone"/> method.
        /// </para>
        /// </summary>
        public override Bullet Clone(Vector2 pos, float rotation) => new FireBullet(pos, rotation, BulletOwner);

        /// <summary>
        /// Overrides the abstract <see cref="Bullet.Img"/> property.
        /// </summary>
        /// <value>The fire bullet image.</value>
        protected override Texture2D Img => Info.Img;
    }
}