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
using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Class HealerCannon.
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class HealerCannon : Cannon
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};


        /// <summary>
        /// Initializes a new instance of the <see cref="HealerCannon"/> class.
        /// </summary>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public HealerCannon(Stage stage, Vector2 position, float rotation) : base(0, ROTATION_SPEED[(int) stage], false, position, rotation) => 
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Healer/H" + ((int) stage + 1));

        /// <summary>
        /// Gets the bullet.
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet => null;

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected override Texture2D Img { get; }
    }
}