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
    /// an enemy tank wich follows a route and heals enemies in its area
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class HealerEnemy : Tank
    {
        //the picture that fills the healing area aroudn this enemy
        private readonly Texture2D healArea;

        //the possible stats of this enemy
        private static readonly int[] HEAL_RADIUS_OPTIONS = {250, 350, 500};
        private static readonly int[] HEAL_AMOUNT_OPTIONS = {1, 5, 10};
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        //stores the radius of the healing area around the player
        private readonly int healRadius;

        //stores the amount of health added ot enemies in the heal area per second
        public readonly int HealAmount;

        //stores the path and the current point on the path which the enemy follows
        private readonly List<Vector2> path;
        private int targetPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealerEnemy"/> class.
        /// </summary>
        /// <param name="position">The intial position.</param>
        /// <param name="rotation">The intial rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="path">The path.</param>
        /// <seealso cref="Tank"/>
        public HealerEnemy(Vector2 position, float rotation, Stage stage, List<Vector2> path) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            //implmeants tank img
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/Healer/HP" + ((int) stage + 1));

            //implements the tank cannon
            Cannon = new HealerCannon(stage, BasePosition, BaseRotation);

            //implements the tank explosion animation
            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            //loads the healArea img
            healArea = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/Light_02");

            //saves the given path to member variable
            this.path = path;

            //chooses the heal radius and heal amount of this enemy based on its stage
            healRadius = HEAL_RADIUS_OPTIONS[(int) stage];
            HealAmount = HEAL_AMOUNT_OPTIONS[(int) stage];
        }

        /// <summary>
        /// calls the Tank update with the correct path point
        /// </summary>
        /// <param name="NA">Only added because this overrides Tank.Update(Vector2 ----)</param>
        /// <seealso cref="Tank.Update(Vector2)"/>
        public override bool Update(Vector2 NA)
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
        /// Draws this tank
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <remarks>
        /// this is a basic Tank draw with the adition of the heal circle
        /// </remarks>
        /// <see cref="Tank.Draw(SpriteBatch)"/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //if still alive draws the healt circle
            if (Health > 0)
            {
                //draws the heal circle
                var healBox = new Rectangle((int) BasePosition.X, (int) BasePosition.Y, 2 * healRadius, 2 * healRadius);
                spriteBatch.Draw(healArea, healBox, null, Color.White, 0, new Vector2(healArea.Width / 2f, healArea.Height / 2f), SpriteEffects.None, 0);
            }

            //draws the rest of the tank
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// creates an instance of healerEnemy
        /// </summary>
        /// <param name="position">The inital position.</param>
        /// <param name="rotation">The inital rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>the new healerEnemy</returns>
        /// <seealso cref="Tank.Clone(Vector2, float, Stage)"/>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new HealerEnemy(position, rotation, stage, path);

        /// <summary>
        /// gets the type of this tank
        /// </summary>
        /// <returns>the tank type</returns>
        /// <seealso cref="Tank.GetTankType"/>
        public override TankType GetTankType() => TankType.Healer;

        /// <summary>
        /// returns a circle that covers teh healing ares
        /// </summary>
        /// <returns>the healing area circle</returns>
        public Circle HealArea() => new Circle(BasePosition, healRadius);
    }
}