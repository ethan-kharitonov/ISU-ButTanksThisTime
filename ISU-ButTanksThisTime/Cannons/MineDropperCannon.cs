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
    /// The cannon used by the tier two enemy
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class MineDropperCannon : Cannon
    {
        //the possible time between shots and rotation speed for this type of cannon
        private static readonly int[] fireRate = {1000, 750, 500, 90};
        private static readonly int[] rotationSpeed = {3, 4, 5, 5};
        private const bool ACTIVE = false;

        //the information about this type of cannon
        public static readonly CannonInfo Info;

        /// <summary>
        /// Initializes static members of the <see cref="MineDropperCannon"/> class.
        /// </summary>
        static MineDropperCannon()
        {
            //populates the cannon info
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/MineDropper/M4");
            Info = new CannonInfo(500, 5, cannonImg, FireBullet.Info, new MineDropperCannon(Owner.Player, Stage.Player, default, 0));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MineDropperCannon"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        public MineDropperCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) stage], rotationSpeed[(int) stage], ACTIVE, position, rotation)
        {
            //Implements the cannon image and bullet
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/MineDropper/M" + ((int) stage + 1));
            Bullet = new FireBullet(Vector2.Zero, 0, owner);
        }

        /// <summary>
        ///  Implements the <see cref="Cannon.Bullet"/> property
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet { get; }

        /// <summary>
        /// Implements the <see cref="Cannon.Img"/> property
        /// </summary>
        /// <value>The MineDropperCannon image</value>
        protected override Texture2D Img { get; }
    }
}