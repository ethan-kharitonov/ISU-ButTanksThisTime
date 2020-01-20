// Author        : Ethan Kharitonov
// File Name     : Tanks\TierFourEnemy.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the TierFourEnemy class.
using System;
using System.Collections.Generic;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// an enemy tank whihc follows a path and shoots in a circle
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class TierFourEnemy : Tank
    {
        //the possible stats of this enemy based on its stage
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 200, 300};

        //stores the path and the current point on the path which the enemy follows
        private readonly List<Vector2> path;
        private int targetPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="TierFourEnemy"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="path">The path.</param>
        /// <seealso cref="Tank"/>
        public TierFourEnemy(Vector2 position, float rotation, Stage stage, List<Vector2> path) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            //implmeants tank img
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierFour/T4P" + ((int) stage + 1));

            //implmeants the tank cannon
            Cannon = new TierFourCannon(Owner.Enemy, stage, BasePosition, BaseRotation);

            //implmeants the tank exxplosion animation
            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            //saves teh given path to a member variables
            this.path = path;
        }

        /// <summary>
        /// calls the Tank update with the correct path point
        /// </summary>
        /// <seealso cref="Tank.Update(Vector2)"/>
        public override bool Update(Vector2 _)
        {
            //checks if this tank is going to hit its target this step
            var distanceSquared = (path[targetPoint] - BasePosition).LengthSquared();
            if (distanceSquared < Math.Pow(Speed, 2))
            {
                //sets this tanks position to target
                BasePosition = path[targetPoint];

                //updates the target point onto the next  point in the path
                targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
            }

            //calls the reset of the tank update function
            return base.Update(path[targetPoint]);
        }

        /// <summary>
        /// gets the type of this tank
        /// </summary>
        /// <returns>the tank type</returns>
        /// <seealso cref="Tank.GetTankType"/>
        public override TankType GetTankType() => TankType.RotateShooter;

        /// <summary>
        /// creates an instance of TierFourEnemy
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>the new TierFourEnemy</returns>
        /// <seealso cref="Tank.Clone(Vector2, float, Stage)"/>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierFourEnemy(position, rotation, stage, path);
    }
}