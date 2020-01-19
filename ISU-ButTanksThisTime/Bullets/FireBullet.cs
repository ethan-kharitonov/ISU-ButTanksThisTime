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
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Bullets
{
    /// <summary>
    /// Class FireBullet.
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class FireBullet : Bullet
    {
        private static readonly Texture2D bulletImg;

        public static readonly BulletInfo Info;
        private const int DAMAGE = 30;

        /// <summary>
        /// Initializes static members of the <see cref="FireBullet"/> class.
        /// </summary>
        static FireBullet()
        {
            bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/FireBullet");
            Info = new BulletInfo(bulletImg, DAMAGE);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireBullet"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="owner">The owner.</param>
        public FireBullet(Vector2 position, float rotation, Owner owner) : base(position, rotation, Tank.IMG_SCALE_FACTOR, owner)
        {
            var exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/FireBulletHit");
            ExAnim = new Animation(exSprite, 4, 1, 4, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, Tank.IMG_SCALE_FACTOR, true);
            Damage = DAMAGE;
        }

        /// <summary>
        /// Gets the ex anim.
        /// </summary>
        /// <value>The ex anim.</value>
        protected override Animation ExAnim { get; }

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>Bullet.</returns>
        public override Bullet Clone(Vector2 pos, float rotation) => new FireBullet(pos, rotation, BulletOwner);
        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected override Texture2D Img => bulletImg;
    }
}