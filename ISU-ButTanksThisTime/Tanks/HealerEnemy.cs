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
using System;
using System.Collections.Generic;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// Class HealerEnemy.
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class HealerEnemy : Tank
    {
        private readonly Texture2D healArea;

        private static readonly int[] HEAL_RADIUS_OPTIONS = {250, 350, 500};
        private static readonly int[] HEAL_AMOUNT_OPTIONS = {1, 5, 10};
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        private readonly int healRadius;
        public readonly int HealAmount;

        private readonly List<Vector2> path;
        private int targetPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealerEnemy"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="path">The path.</param>
        public HealerEnemy(Vector2 position, float rotation, Stage stage, List<Vector2> path) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/Healer/HP" + ((int) stage + 1));

            Cannon = new HealerCannon(stage, BasePosition, BaseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            healArea = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/Light_02");

            this.path = path;

            healRadius = HEAL_RADIUS_OPTIONS[(int) stage];
            HealAmount = HEAL_AMOUNT_OPTIONS[(int) stage];
        }

        /// <summary>
        /// Updates the specified na.
        /// </summary>
        /// <param name="na">The na.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Update(Vector2 na)
        {
            var distanceSquared = (path[targetPoint] - BasePosition).LengthSquared();
            if (distanceSquared < Math.Pow(Speed, 2))
            {
                BasePosition = path[targetPoint];
                targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
            }

            return base.Update(path[targetPoint]);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Health > 0)
            {
                var healBox = new Rectangle((int) BasePosition.X, (int) BasePosition.Y, 2 * healRadius, 2 * healRadius);
                spriteBatch.Draw(healArea, healBox, null, Color.White, 0, new Vector2(healArea.Width / 2f, healArea.Height / 2f), SpriteEffects.None, 0);
            }

            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>Tank.</returns>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new HealerEnemy(position, rotation, stage, path);

        /// <summary>
        /// Gets the type of the tank.
        /// </summary>
        /// <returns>TankType.</returns>
        public override TankType GetTankType() => TankType.Healer;

        /// <summary>
        /// Heals the area.
        /// </summary>
        /// <returns>Circle.</returns>
        public Circle HealArea() => new Circle(BasePosition, healRadius);
    }
}