// Author        : Ethan Kharitonov
// File Name     : Cannons\HealerCannon.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the cannon the healer enemy uses.
using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Implements the cannon the healer enemy uses.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class HealerCannon : Cannon
    {
        //The possible rotation speeds of this type of cannon
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private static readonly int[] rotationSpeed = {5, 6, 7};

        /// <summary>
        /// Initializes a new instance of the <see cref="HealerCannon"/> class.
        /// </summary>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        public HealerCannon(Stage stage, Vector2 position, float rotation) : base(0, rotationSpeed[(int) stage], false, position, rotation) => 
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Healer/H" + ((int) stage + 1)); //implements the image of the cannon

        /// <summary>
        ///  Implements the <see cref="Cannon.Bullet"/> property
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet => null;

        /// <summary>
        /// Implements the <see cref="Cannon.Img"/> property
        /// </summary>
        /// <value>The HealerCannon image</value>
        protected override Texture2D Img { get; }
    }
}