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
        //the time between shots (not aplicable at this case)
        private const int FIRE_RATE = 0;

        //stores wether this cannon shoots
        private const bool ACTIVE = false;

        //stores the possible rotation speeds of this cannon
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};

        /// <summary>
        /// Initializes a new instance of the <see cref="BomberEnemyCannon"/> class.
        /// </summary>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        public BomberEnemyCannon(Stage stage, Vector2 position, float rotation) : base(FIRE_RATE, ROTATION_SPEED[(int) stage], ACTIVE, position, rotation) => 
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