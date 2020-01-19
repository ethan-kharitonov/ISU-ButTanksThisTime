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
    /// Class TierTwoEnemy.
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class TierTwoEnemy : Tank
    {
        private Vector2 target;

        private static readonly int[] SPEED = {2, 3, 6};
        private static readonly int[] ROTATION_SPEED = {2, 3, 4};
        private static readonly int[] HEALTH = {75, 100, 200};

        private readonly int viewRange = 300;

        /// <summary>
        /// Initializes a new instance of the <see cref="TierTwoEnemy"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        public TierTwoEnemy(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierTwo/T2P" + ((int) stage + 1));

            Cannon = new MineDropperCannon(Owner.Enemy, stage, BasePosition, BaseRotation)
            {
                Active = false
            };

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            target = SetTarget();
        }

        /// <summary>
        /// Updates the specified player position.
        /// </summary>
        /// <param name="playerPos">The player position.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Update(Vector2 playerPos)
        {
            if (BasePosition == target)
            {
                GameScene.AddLandMine(new RedMine(BasePosition - Dimensions / 2f));
                target = SetTarget();
            }

            if ((BasePosition - playerPos).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                Cannon.Active = true;
                return base.Update(target, playerPos);
            }

            Cannon.Active = false;
            return base.Update(target);
        }

        /// <summary>
        /// Sets the target.
        /// </summary>
        /// <returns>Vector2.</returns>
        private Vector2 SetTarget() => new Vector2(Tools.Rnd.Next(Tools.ArenaBounds.Left, Tools.ArenaBounds.Right), Tools.Rnd.Next(Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom));

        /// <summary>
        /// Gets the type of the tank.
        /// </summary>
        /// <returns>TankType.</returns>
        public override TankType GetTankType() => TankType.MineDropper;

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>Tank.</returns>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierTwoEnemy(position, rotation, stage);
    }
}