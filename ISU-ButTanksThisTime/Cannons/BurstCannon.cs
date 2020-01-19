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
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Class BurstCannon.
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class BurstCannon : Cannon
    {
        private static readonly int[] fireRate = {150, 100, 80, 60};
        private static readonly int[] rotationSpeed = {4, 1000};
        private static readonly int[] burstRate = {2000, 2500, 1500, 1000, 1000};
        private static readonly int[] burstLength = {300, 500, 700, 800, 1000};

        private readonly Timer timeBetweenBursts;
        private readonly Timer burstDuration;

        private bool inBurst;

        public static readonly CannonInfo Info;

        /// <summary>
        /// Initializes static members of the <see cref="BurstCannon"/> class.
        /// </summary>
        static BurstCannon()
        {
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierThree/T3P4");
            Info = new CannonInfo(60, 1000, cannonImg, Laser.Info, new BurstCannon(Owner.Player, Stage.Player, default, 0), 1000, 1000);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BurstCannon"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public BurstCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) stage], rotationSpeed[(int) owner], true, position, rotation)
        {
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierThree/T3P" + ((int) stage + 1));

            Bullet = new Laser(Vector2.Zero, 0, Tank.IMG_SCALE_FACTOR, owner);

            timeBetweenBursts = new Timer(burstRate[(int) stage]);
            burstDuration = new Timer(burstLength[(int) stage]);
        }

        /// <summary>
        /// Updates the specified base position.
        /// </summary>
        /// <param name="basePos">The base position.</param>
        /// <param name="baseRotation">The base rotation.</param>
        /// <param name="target">The target.</param>
        public override void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {
            //start timer for shooting period
            if (Active && !inBurst)
            {
                burstDuration.Reset();
                inBurst = true;
            }

            if (!burstDuration.IsTimeUp(Tools.GameTime))
            {
                Active = true;
            }

            //if shooting is done but still inBurst start timer for reloading
            if (inBurst && burstDuration.IsTimeUp(Tools.GameTime))
            {
                Active = false;
                if (timeBetweenBursts.IsTimeUp(Tools.GameTime))
                {
                    inBurst = false;
                    timeBetweenBursts.Reset();
                }
            }

            base.Update(basePos, baseRotation, target);
        }

        /// <summary>
        /// Gets the bullet.
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet { get; }

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected override Texture2D Img { get; }
    }
}