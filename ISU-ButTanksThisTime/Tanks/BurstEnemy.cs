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
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// Class BurstEnemy.
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class BurstEnemy : Tank
    {
        private static readonly int[] ATTACK_RANGE = {250, 400, 550};
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        /// <summary>
        /// Initializes a new instance of the <see cref="BurstEnemy"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        public BurstEnemy(Vector2 position, float rotation, Stage stage) : base(position, stage, ATTACK_RANGE[(int) stage], SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierThree/T3P" + ((int) stage + 1));

            Cannon = new BurstCannon(Owner.Enemy, stage, BasePosition, BaseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);
        }

        /// <summary>
        /// Gets the type of the tank.
        /// </summary>
        /// <returns>TankType.</returns>
        public override TankType GetTankType() => TankType.Burst;

        /// <summary>
        /// Updates the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Update(Vector2 target)
        {
            if ((BasePosition - target).Length() <= AttackRange)
            {
                Cannon.Active = true;
            }

            return base.Update(target);
        }

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>Tank.</returns>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new BurstEnemy(position, rotation, stage);
    }
}