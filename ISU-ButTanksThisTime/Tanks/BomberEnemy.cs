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
    /// an enemy tank whihc does not shoot but charges towards the player
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class BomberEnemy : Tank
    {
        //the possible stats of this enemy based on its stage
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        /// <summary>
        /// Initializes a new instance of the <see cref="BomberEnemy"/> class.
        /// </summary>
        /// <param name="position">The initial position</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="stage">the level of the tank (determines its stats)</param>
        public BomberEnemy(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            //implements the tank image
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/BomberEnemy/BP" + ((int) stage + 1));

            //implements the tank cannon
            Cannon = new BomberEnemyCannon(stage, BasePosition, BaseRotation);

            //implements the tank explosion animation
            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);
        }

        /// <summary>
        /// constructs a an instance of this object
        /// </summary>
        /// <param name="position">The initial position</param>
        /// <param name="rotation">The initial rotation.</param>
        /// <param name="stage">the level of the tank (determines its stats)</param>
        /// <returns>the new constructed instance</returns>
        /// <see cref="BomberEnemy(Vector2, float, Stage)"/>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new BomberEnemy(position, rotation, stage);

        /// <summary>
        /// Implements the method <see cref="Tank.Collide(object)"/>
        /// </summary>
        /// <remarks>
        /// only changes the collision with player
        /// </remarks>
        public override void Collide(object collided)
        {
            switch (collided)
            {
                case Bullet bullet:
                    if (bullet.BulletOwner == Owner.Player)
                    {
                        Health -= bullet.Damage;
                    }

                    break;
                case Player _:
                    //kills enemy but not by the player
                    Health = 0;
                    KilledByPlayer = false;
                    break;
                case LandMine _:
                    Health = 0;
                    break;
            }
        }

        /// <summary>
        /// returns the type of the tank
        /// </summary>
        /// <returns>the type of the tank</returns>
        /// <see cref="Tank.GetTankType"/>
        public override TankType GetTankType() => TankType.Bomber;
    }
}