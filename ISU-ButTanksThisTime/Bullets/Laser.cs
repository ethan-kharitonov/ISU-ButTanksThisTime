﻿// ***********************************************************************
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
    /// Class Laser.
    /// Implements the <see cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Bullets.Bullet" />
    internal class Laser : Bullet
    {
        private readonly float scaleFactor;
        private static readonly Texture2D bulletImg;

        public static readonly BulletInfo Info;
        private const int DAMAGE = 35;

        /// <summary>
        /// Initializes static members of the <see cref="Laser"/> class.
        /// </summary>
        static Laser()
        {
            bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Laser");
            Info = new BulletInfo(bulletImg, DAMAGE);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Laser"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="owner">The owner.</param>
        public Laser(Vector2 position, float rotation, float scaleFactor, Owner owner) : base(position, rotation, scaleFactor, owner)
        {
            this.scaleFactor = scaleFactor;

            var exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/LaserExplode");
            ExAnim = new Animation(exSprite, 3, 1, 3, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, scaleFactor, true);

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
        public override Bullet Clone(Vector2 pos, float rotation) => new Laser(pos, rotation, scaleFactor, BulletOwner);
        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected override Texture2D Img => bulletImg;
    }
}