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
using Animation2D;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.LandMines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// Class BomberEnemy.
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class BomberEnemy : Tank
    {
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        /// <summary>
        /// Initializes a new instance of the <see cref="BomberEnemy"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        public BomberEnemy(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/BomberEnemy/BP" + ((int) stage + 1));

            Cannon = new BomberEnemyCannon(stage, BasePosition, BaseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);
        }

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>Tank.</returns>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new BomberEnemy(position, rotation, stage);

        /// <summary>
        /// Collides the specified collided.
        /// </summary>
        /// <param name="collided">The collided.</param>
        public override void Collide(object collided)
        {
            switch (collided)
            {
                case Bullet bullet:
                    if (bullet.BulletOwner == Owner.Player)
                    {
                        Health -= 25;
                    }

                    break;
                case Player _:
                    Health = 0;
                    Killed = false;
                    break;
                case LandMine _:
                    Health = 0;
                    break;
            }
        }

        /// <summary>
        /// Gets the type of the tank.
        /// </summary>
        /// <returns>TankType.</returns>
        public override TankType GetTankType() => TankType.Bomber;
    }
}