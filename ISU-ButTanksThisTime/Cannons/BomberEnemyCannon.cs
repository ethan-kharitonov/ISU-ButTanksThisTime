// Author        : Ethan Kharitonov
// File Name     : Cannons\BomberEnemyCannon.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the BomberEnemyCannon class.
using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Class BomberEnemyCannon.
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class BomberEnemyCannon : Cannon
    {
        //the time between shots (not applicable at this case)
        private const int FIRE_RATE = 0;

        //stores whether this cannon shoots
        private const bool ACTIVE = false;

        //stores the possible rotation speeds of this cannon
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private static readonly int[] rotationSpeed = {5, 6, 7};

        /// <summary>
        /// Initializes a new instance of the <see cref="BomberEnemyCannon"/> class.
        /// </summary>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        public BomberEnemyCannon(Stage stage, Vector2 position, float rotation) : base(FIRE_RATE, rotationSpeed[(int) stage], ACTIVE, position, rotation) => 
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Inactive/I" + ((int) stage + 1)); //implements the image of the cannon

        /// <summary>
        /// Implements the <see cref="Cannon.Bullet"/> property
        /// </summary>
        /// <value>Always <c>null</c></value>
        protected override Bullet Bullet => null;
        
        /// <summary>
        /// Implements the <see cref="Cannon.Img"/> property
        /// </summary>
        /// <value>The bomberEnemyCannon image</value>
        protected override Texture2D Img { get; }
    }
}