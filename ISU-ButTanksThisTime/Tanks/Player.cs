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
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.LandMines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// Class Player.
    /// Implements the <see cref="ISU_ButTanksThisTime.Tanks.Tank" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Tanks.Tank" />
    internal class Player : Tank
    {
        //Movement Variables
        private Vector2 velocity = new Vector2(0, 0);
        private int accRate = 1;
        private float friction = 0.4f;
        private int maxSpeed = 10;
        private bool isKeyPressed;
        private const int ROTATION_SPEED = 5;

        //how long the player gets increased speed
        private readonly Timer speedBoostTimer = new Timer(5000);

        //stores if the player can shoot
        private readonly bool canControlShooting = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Player(Vector2 position) : base(position, Stage.Player, 0, 0, 0, 1000)
        {
            //implement tank variables
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1PP");
            Cannon = new TierOneCannon(Owner.Player, Stage.Player, BasePosition, BaseRotation);
            BasePosition = position;

            //load and implement tank explosion animation
            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);
        }

        /// <summary>
        /// Updates the specified na.
        /// </summary>
        /// <param name="na">The na.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Update(Vector2 na)
        {
            //store the state of the keyboard
            var kb = Keyboard.GetState();
            
            //reset  speed variables if speed boost is over and the variables have not been changed
            if (speedBoostTimer.IsTimeUp(Tools.GameTime) && accRate != 1)
            {
                //reset speed variables
                accRate = 1;
                friction = 0.4f;
                maxSpeed = 10;
            }
            //call MoveTank and CannonUpdate functions
            MoveTank(kb);
            CannonUpdate();
            
            //update health bar
            Bar.Update(BasePosition, Health);

            //if player is dead go to loose screen
            if (Health <= 0)
            {
                Game1.State = State.LoseScreen;
            }

            Console.WriteLine(Health);
            return false;
        }

        /// <summary>
        /// Cannons the update.
        /// </summary>
        private void CannonUpdate()
        {
            Cannon.Active = Mouse.GetState().LeftButton == ButtonState.Pressed && canControlShooting && GameScene.AreAnyBulletsLeft();

            Cannon.Update(BasePosition, BaseRotation, Tools.TrueMousePos);
        }

        /// <summary>
        /// Moves the tank.
        /// </summary>
        /// <param name="kb">The kb.</param>
        private void MoveTank(KeyboardState kb)
        {
            isKeyPressed = false;

            if (kb.IsKeyDown(Keys.D))
            {
                velocity.X += accRate;
                isKeyPressed = true;
            }

            if (kb.IsKeyDown(Keys.A))
            {
                velocity.X -= accRate;
                isKeyPressed = true;
            }

            if (kb.IsKeyDown(Keys.W))
            {
                velocity.Y -= accRate;
                isKeyPressed = true;
            }

            if (kb.IsKeyDown(Keys.S))
            {
                velocity.Y += accRate;
                isKeyPressed = true;
            }

            velocity.X = MathHelper.Clamp(velocity.X, -maxSpeed, maxSpeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxSpeed, maxSpeed);

            velocity.X = Tools.ApproachValue(velocity.X, 0, friction);
            velocity.Y = Tools.ApproachValue(velocity.Y, 0, friction);

            BasePosition += velocity;

            BasePosition.X = MathHelper.Clamp(BasePosition.X, Tools.ArenaBounds.Left + (int) (BaseImg.Width * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Right - (int) (BaseImg.Width * IMG_SCALE_FACTOR / 2.0));
            BasePosition.Y = MathHelper.Clamp(BasePosition.Y, Tools.ArenaBounds.Top + (int) (BaseImg.Height * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Bottom - (int) (BaseImg.Height * IMG_SCALE_FACTOR / 2.0));

            if (isKeyPressed)
            {
                BaseRotation = Tools.RotateTowardsVector(BaseRotation, velocity * new Vector2(1, -1), ROTATION_SPEED) + 180;
                BaseRotation += 180;
                BaseRotation %= 360;
            }
        }

        /// <summary>
        /// Collides the specified collided.
        /// </summary>
        /// <param name="collided">The collided.</param>
        public override void Collide(object collided)
        {
            switch (collided)
            {
                case Bullet bullet:
                    Health -= bullet.Damage;
                    break;
                case BomberEnemy _:
                    Health -= 25;
                    break;
                case RedMine _:
                    Health = 0;
                    break;
            }

            if (Health > StartingHealth)
            {
                Health = StartingHealth;
            }
        }

        /// <summary>
        /// Gets the type of the tank.
        /// </summary>
        /// <returns>TankType.</returns>
        public override TankType GetTankType() => TankType.Player;

        /// <summary>
        /// Clones the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>Tank.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => throw new NotImplementedException();

        /// <summary>
        /// Steps the out of shop.
        /// </summary>
        public void StepOutOfShop()
        {
            velocity = Vector2.Zero;
            BasePosition = Tools.ArenaBounds.Location.ToVector2() + new Vector2(150, 150);
        }

        /// <summary>
        /// Takes the cannon.
        /// </summary>
        /// <param name="newCannon">The new cannon.</param>
        public void TakeCannon(Cannon newCannon)
        {
            Cannon = newCannon;
            Cannon.Update(BasePosition, BaseRotation, Tools.TrueMousePos);
        }

        /// <summary>
        /// Speeds up.
        /// </summary>
        public void SpeedUp()
        {
            speedBoostTimer.Reset();
            accRate = 3;
            friction = 1.5f;
            maxSpeed = 20;
        }
    }
}