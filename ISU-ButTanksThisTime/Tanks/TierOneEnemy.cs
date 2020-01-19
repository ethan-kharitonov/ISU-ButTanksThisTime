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
    /// Class TierOneEnemy.
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class TierOneEnemy : Tank
    {
        private static readonly int[] VIEW_RANGE_OPTIONS = {300, 450, 650};
        private static readonly int[] ATTACK_RANGE_OPTIONS = {250, 400, 550};
        private static readonly int[] SPEED = {4, 5, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {150, 200, 300};

        private readonly int playerAttackRange;
        private readonly int viewRange;


        private readonly List<Vector2> path;
        private int targetPoint = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="TierOneEnemy"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="rotation">The rotation.</param>
        public TierOneEnemy(Vector2 position, List<Vector2> path, Stage stage, float rotation) : base(position, stage, VIEW_RANGE_OPTIONS[(int) stage], SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1P" + ((int) stage + 1));

            Cannon = new TierOneCannon(Owner.Enemy, stage, BasePosition, BaseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            this.path = path;

            playerAttackRange = ATTACK_RANGE_OPTIONS[(int) stage];
            viewRange = VIEW_RANGE_OPTIONS[(int) stage];
        }

        /// <summary>
        /// Updates the specified player position.
        /// </summary>
        /// <param name="playerPos">The player position.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Update(Vector2 playerPos)
        {
            if ((playerPos - BasePosition).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                Cannon.Active = true;
                AttackRange = playerAttackRange;
                return base.Update(playerPos);
            }
            else
            {
                Cannon.Active = false;
                AttackRange = 0;
                var distanceSquared = (path[targetPoint] - BasePosition).LengthSquared();
                if (distanceSquared < Math.Pow(Speed, 2))
                {
                    BasePosition = path[targetPoint];
                    targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
                }

                return base.Update(path[targetPoint]);
            }
        }

        /// <summary>
        /// Gets the type of the tank.
        /// </summary>
        /// <returns>TankType.</returns>
        public override TankType GetTankType() => TankType.BasicPath;

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>Tank.</returns>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierOneEnemy(position, path, stage, rotation);
    }
}