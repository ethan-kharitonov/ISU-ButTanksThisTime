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
using System.Collections.Generic;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// basic enemy tank which follows a path and shoots at the playr if in the area
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class TierOneEnemy : Tank
    {
        //the possible stats of this enemy
        private static readonly int[] VIEW_RANGE_OPTIONS = {300, 450, 650};
        private static readonly int[] ATTACK_RANGE_OPTIONS = {250, 400, 550};
        private static readonly int[] SPEED = {4, 5, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {150, 200, 300};

        //stores the distance the player has to be from the tank for the tank to see it
        private readonly int viewRange;

        //stores the distance the player has to be from the tank for the tank to start shooting
        private readonly int playerAttackRange;

        //stores the path and the current point on the path which the enemy follows
        private readonly List<Vector2> path;
        private int targetPoint = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="TierOneEnemy"/> class.
        /// </summary>
        /// <param name="position">The inital position.</param>
        /// <param name="path">The path.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="rotation">The inital rotation.</param>
        /// <seealso cref="Tank"/>
        public TierOneEnemy(Vector2 position, List<Vector2> path, Stage stage, float rotation) : base(position, stage, VIEW_RANGE_OPTIONS[(int) stage], SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            //implmeants tank img
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1P" + ((int) stage + 1));
            
            //implements the tank cannon
            Cannon = new TierOneCannon(Owner.Enemy, stage, BasePosition, BaseRotation);

            //implements the tank explosion animation
            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            //saves the given path to member variable
            this.path = path;

            //chooses attack range and view range of this enemy based on its stage
            playerAttackRange = ATTACK_RANGE_OPTIONS[(int) stage];
            viewRange = VIEW_RANGE_OPTIONS[(int) stage];
        }

        /// <summary>
        /// decides if the target is player or the path then calls regular tank update.
        /// </summary>
        /// <param name="playerPos">The player position.</param>
        ///<seealso cref="Tank.Update(Vector2)"/>
        public override bool Update(Vector2 playerPos)
        {
            //checks if the tank can see the player
            if ((playerPos - BasePosition).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                //turns on the cannon and sets attack range to player attack range
                Cannon.Active = true;
                AttackRange = playerAttackRange;

                //cals the Tank update function with tha player as target
                return base.Update(playerPos);
            }
            else
            {
                //turns cannon off and sets attack range to 0
                Cannon.Active = false;
                AttackRange = 0;

                //checks if this tank is going to hit its target this step
                var distanceSquared = (path[targetPoint] - BasePosition).LengthSquared();
                if (distanceSquared < Math.Pow(Speed, 2))
                {
                    //sets this tanks position to target
                    BasePosition = path[targetPoint];

                    //updates the target point onto the next  point in the path
                    targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
                }

                //calls the Tank update with the current path point as the target
                return base.Update(path[targetPoint]);
            }
        }

        /// <summary>
        /// gets the type of this tank
        /// </summary>
        /// <returns>the tank type</returns>
        /// <seealso cref="Tank.GetTankType"/>
        public override TankType GetTankType() => TankType.BasicPath;

        /// <summary>
        /// creates an instance of healerEnemy
        /// </summary>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>the new healerEnemy</returns>
        /// <seealso cref="Tank.Clone(Vector2, float, Stage)"/>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierOneEnemy(position, path, stage, rotation);
    }
}