// Author        : Ethan Kharitonov
// File Name     : Cannons\TierOneCannon.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the TierOneCannon class.
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// The cannon used by the tier one enemy
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class TierOneCannon : Cannon
    {
        //The cannon info for this type
        public static readonly CannonInfo Info;

        //the possible time between rotations and rotation speed for this type of cannon
        private static readonly int[] fireRate = {1000, 800, 700, 300};
        private static readonly int[] rotationSpeed = {3, 4, 5, 4};

        //sets cannon active to false
        private const bool ACTIVE = false;

        /// <summary>
        /// Initializes static members of the <see cref="TierOneCannon"/> class.
        /// </summary>
        static TierOneCannon()
        {
            //populates the cannon info
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierOne/T1P4");
            Info = new CannonInfo(500, 3, cannonImg, MediumBullet.Info, new TierOneCannon(Owner.Player, Stage.Player, default, 0));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TierOneCannon"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        public TierOneCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) stage], rotationSpeed[(int) stage], ACTIVE, position, rotation)
        {
            //Implements the cannon image and bullet
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierOne/T1P" + ((int) stage + 1));
            Bullet = new MediumBullet(Vector2.Zero, 0, Tank.IMG_SCALE_FACTOR, owner);
        }


        /// <summary>
        ///  Implements the <see cref="Cannon.Bullet"/> property
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet { get; }

        /// <summary>
        /// Implements the <see cref="Cannon.Img"/> property
        /// </summary>
        /// <value>The TierOneCannon image</value>
        protected override Texture2D Img { get; }
    }
}