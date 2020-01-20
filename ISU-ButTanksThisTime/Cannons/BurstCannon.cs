// Author        : Ethan Kharitonov
// File Name     : Cannons\BurstCannon.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the BurstCannon class.
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
        //stores the info of the BurstCannon
        public static readonly CannonInfo Info;

        //The possible stats of any BurstCannon
        private static readonly int[] fireRate = {150, 100, 80, 60};
        private static readonly int[] rotationSpeed = {4, 1000};
        private static readonly int[] burstRate = {2000, 2500, 1500, 1000, 1000};
        private static readonly int[] burstLength = {300, 500, 700, 800, 1000};

        //stores the time between bursts and the length of those bursts
        private readonly Timer timeBetweenBursts;
        private readonly Timer burstDuration;

        //indicates wether this cannon is in a burst state
        private bool inBurst;


        /// <summary>
        /// Initializes static members of the <see cref="BurstCannon"/> class.
        /// </summary>
        static BurstCannon()
        {
            //loads the info for this type of cannon
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierThree/T3P4");
            Info = new CannonInfo(60, 1000, cannonImg, Laser.Info, new BurstCannon(Owner.Player, Stage.Player, default, 0), 1000, 1000);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BurstCannon"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        public BurstCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) stage], rotationSpeed[(int) owner], true, position, rotation)
        {
            //Implements the cannon image
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierThree/T3P" + ((int) stage + 1));

            //Implements the cannon bullet
            Bullet = new Laser(Vector2.Zero, 0, Tank.IMG_SCALE_FACTOR, owner);

            //set the time between burst based on the stage
            timeBetweenBursts = new Timer(burstRate[(int) stage]);
            burstDuration = new Timer(burstLength[(int) stage]);
        }

        /// <summary>
        /// decides if the cannon shoud shoot then calls regular update
        /// </summary>
        /// <param name="basePos">The tank position.</param>
        /// <param name="baseRotation">The tank rotation.</param>
        /// <param name="target">The target.</param>
        /// <seealso cref="Cannon.Update(Vector2, float, Vector2)"/>
        public override void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {
            //start timer for shooting period
            if (Active && !inBurst)
            {
                //reset burst duration timer and set go into burst state
                burstDuration.Reset();
                inBurst = true;
            }

            //if burst duration time is not up set cannon active to true (shoot)
            if (!burstDuration.IsTimeUp(Tools.GameTime))
            {
                Active = true;
            }

            //if shooting is done but still inBurst start timer for reloading
            if (inBurst && burstDuration.IsTimeUp(Tools.GameTime))
            {
                //set cannon active to false
                Active = false;
                
                //if done time between bursts is done leave burst state and reset timer
                if (timeBetweenBursts.IsTimeUp(Tools.GameTime))
                {
                    //leave burst state and reset time between bursts timer
                    inBurst = false;
                    timeBetweenBursts.Reset();
                }
            }

            //call teh rest of the update function
            base.Update(basePos, baseRotation, target);
        }

        /// <summary>
        ///  Implements the <see cref="Cannon.Bullet"/> property
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet { get; }

        /// <summary>
        /// Implements the <see cref="Cannon.Img"/> property
        /// </summary>
        /// <value>The BurstCannon image</value>
        protected override Texture2D Img { get; }
    }
}