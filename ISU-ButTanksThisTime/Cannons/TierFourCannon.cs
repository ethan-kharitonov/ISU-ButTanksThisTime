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
    /// The cannon used by the tier four enemy (rotates and shoots)
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class TierFourCannon : Cannon
    {
        //the possible stats of this type of cannon
        private static readonly int[] fireRate = {0, 75};
        private static readonly int[] rotationSpeed = {100, 1000};
        private static readonly int[] timeBtwRotations = {1000, 150};

        //stores time between rotations and the rotation incerments in degrees
        private readonly Timer rotateTimer;
        private const int ROTATION_INC = 45;

        public static readonly CannonInfo Info;

        /// <summary>
        /// Initializes static members of the <see cref="TierFourCannon"/> class.
        /// </summary>
        static TierFourCannon()
        {
            //populates the cannon info for this type of cannon
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P4");
            Info = new CannonInfo(75, 1000, cannonImg, Plasma.Info, new TierFourCannon(Owner.Player, Stage.Player, default, 0));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TierFourCannon"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        public TierFourCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) owner], rotationSpeed[(int) owner], true, position, rotation)
        {
            //Implements the cannon image and bullet
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P" + ((int) stage + 1));
            Bullet = new Plasma(Vector2.Zero, 0, owner);

            //sets the time between rotations based on the owner and resets it waits before first rotation
            rotateTimer = new Timer(timeBtwRotations[(int) owner]);
            rotateTimer.Reset();
            
            //set active to false
            Active = false;
        }

        /// <summary>
        /// calculates position, rotates and shoots
        /// </summary>
        /// <param name="basePos">The tank position.</param>
        /// <param name="baseRotation">The tank rotation.</param>
        public override void Update(Vector2 basePos, float baseRotation, Vector2 _)
        {
            //calculate the position of the cannon
            Pos = CalcPos(basePos, baseRotation);

            //if time between rotations is up rotate and shoot
            if (rotateTimer.IsTimeUp(Tools.GameTime))
            {
                //rotate
                Rotation += ROTATION_INC;

                //shoot
                var newBullet = Bullet.Clone(Pos, Rotation);
                GameScene.AddBullet(newBullet);

                //reset time between rotations
                rotateTimer.Reset();
            }
        }

        /// <summary>
        ///  Implements the <see cref="Cannon.Bullet"/> property
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet { get; }

        /// <summary>
        /// Implements the <see cref="Cannon.Img"/> property
        /// </summary>
        /// <value>The TierFourCannon image</value>
        protected override Texture2D Img { get; }
    }
}