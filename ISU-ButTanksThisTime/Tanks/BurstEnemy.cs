// Author        : Ethan Kharitonov
// File Name     : Tanks\BurstEnemy.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Defines the BurstEnemy class.
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// an enemy tank whihc shoots in bursts
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class BurstEnemy : Tank
    {
        //the possible stats of this enemy based on its stage
        private static readonly int[] ATTACK_RANGE = {250, 400, 550};
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        /// <summary>
        /// Initializes a new instance of the <see cref="BurstEnemy"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="stage">The stage (determines the satats of this tank)</param>
        /// <see cref="Tank"/>
        public BurstEnemy(Vector2 position, float rotation, Stage stage) : base(position, stage, ATTACK_RANGE[(int) stage], SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            //implements tank image
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierThree/T3P" + ((int) stage + 1));

            //impleaments tank cannon
            Cannon = new BurstCannon(Owner.Enemy, stage, BasePosition, BaseRotation);

            //implemeants tank explosion animation
            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);
        }

        /// <summary>
        /// Implements the method <see cref="Tank.GetTankType"/>
        /// </summary>
        public override TankType GetTankType() => TankType.Burst;

        /// <summary>
        /// updates the enemy
        /// </summary>
        /// <remarks>
        /// only turns on cannon when near player
        /// </remarks>
        /// <seealso cref="Tank.Update(Vector2)"/>
        public override bool Update(Vector2 target)
        {
            //if player is near turn cannon on, else turn it off
            if ((BasePosition - target).Length() <= AttackRange)
            {
                Cannon.Active = true;
            }
            else
            {
                return false;
            }

            //calls the reset of the tank update
            return base.Update(target);
        }

        /// <summary>
        /// creates an instance of burst enemy
        /// </summary>
        /// <seealso cref="Tank.Clone(Vector2, float, Stage)"/>
        /// <see cref="BurstCannon"/>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new BurstEnemy(position, rotation, stage);
    }
}