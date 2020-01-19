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
using System;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.LandMines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// An enemy tank which drops mines at random spots on the map
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class TierTwoEnemy : Tank
    {
        //stores the current target point of this tank
        private Vector2 target;

        //the possible stats of this enemy
        private static readonly int[] SPEED = { 2, 3, 6 };
        private static readonly int[] ROTATION_SPEED = { 2, 3, 4 };
        private static readonly int[] HEALTH = { 75, 100, 200 };
        private static readonly int[] VIEW_RANGE_OPTIONS = { 300, 350, 450 };

        //the distance at which this tank can see the player
        private readonly int viewRange;


        /// <summary>
        /// Initializes a new instance of the <see cref="TierTwoEnemy"/> class.
        /// </summary>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The intial rotation.</param>
        /// <param name="stage">The stage.</param>
        public TierTwoEnemy(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, SPEED[(int)stage], ROTATION_SPEED[(int)stage], HEALTH[(int)stage], rotation)
        {
            //implmeants tank img
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierTwo/T2P" + ((int)stage + 1));

            //implements the tank cannon initaly set to false
            Cannon = new MineDropperCannon(Owner.Enemy, stage, BasePosition, BaseRotation)
            {
                Active = false
            };

            //implements the tank explosion animation
            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            //sets target to a random point on the map
            target = SetTarget();

            //chooses the view range based on this tanks stage
            viewRange = VIEW_RANGE_OPTIONS[(int)stage];
        }

        /// <summary>
        /// basic update with adition of droping land mines when reaching target and deciding wether to shot at player
        /// </summary>
        /// <param name="playerPos">The player position.</param>
        ///<seealso cref="Tank.Update(Vector2)"/>
        public override bool Update(Vector2 playerPos)
        {
            //drop landmine if reached target
            if (BasePosition == target)
            {
                //drop landmine
                GameScene.AddLandMine(new RedMine(BasePosition - Dimensions / 2f));

                //calculate new target positon
                target = SetTarget();
            }

            //if this tank can see player shoot and follow him
            if ((BasePosition - playerPos).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                //turn on cannon and call Tank update with player as target
                Cannon.Active = true;
                return Update(target, playerPos);
            }

            //turn cannon off and call Tank update with the random point(target) as the target
            Cannon.Active = false;
            return base.Update(target);
        }

        /// <summary>
        /// Calculates a random point on the mao
        /// </summary>
        /// <returns>the random point</returns>
        private Vector2 SetTarget() => new Vector2(Tools.Rnd.Next(Tools.ArenaBounds.Left, Tools.ArenaBounds.Right), Tools.Rnd.Next(Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom));

        // <summary>
        /// gets the type of this tank
        /// </summary>
        /// <returns>the tank type</returns>
        /// <seealso cref="Tank.GetTankType"/>
        public override TankType GetTankType() => TankType.MineDropper;

        //// <summary>
        /// creates an instance of TierTwoEnemy
        /// </summary>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>the new TierTwoEnemy</returns>
        /// <seealso cref="Tank.Clone(Vector2, float, Stage)"/>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierTwoEnemy(position, rotation, stage);
    }
}