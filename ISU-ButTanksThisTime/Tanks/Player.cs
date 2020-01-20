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
using Microsoft.Xna.Framework.Input;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// the tank controlled by the player
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
        private const int ENEMY_COLLIDE_DAMAGE = 25;

        //how long the player gets increased speed
        private readonly Timer speedBoostTimer = new Timer(5000);

        //stores if the player can shoot
        private readonly bool canControlShooting = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="position">The position to instanciate the player at</param>
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
        /// updates the player position, cannon, etc
        /// </summary>
        /// <returns>Always <c>false</c></returns>
        public override bool Update(Vector2 _)
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
                TankGame.State = GameState.LoseScreen;
            }

            return false;
        }

        /// <summary>
        /// updates the cannon
        /// </summary>
        /// <seealso cref="Cannon.Update"/>
        private void CannonUpdate()
        {
            //attempts to shoot cannon if player has bullets is pressing left click and can cantrolle shooting
            Cannon.Active = Mouse.GetState().LeftButton == ButtonState.Pressed && canControlShooting && GameScene.AreAnyBulletsLeft();

            //updates cannon with mouse position
            Cannon.Update(BasePosition, BaseRotation, Tools.TrueMousePos);
        }

        /// <summary>
        /// Holds the code to move the tank
        /// </summary>
        /// <param name="kb">the keyboard state</param>
        private void MoveTank(KeyboardState kb)
        {
            //sets key pressed ot false by default
            isKeyPressed = false;

            //increase the appropraite velocity based on the key pressed and set set key pressed to true
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

            //holds velocity between min and max velocity
            velocity.X = MathHelper.Clamp(velocity.X, -maxSpeed, maxSpeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxSpeed, maxSpeed);

            //applies friction
            velocity.X = Tools.ApproachValue(velocity.X, 0, friction);
            velocity.Y = Tools.ApproachValue(velocity.Y, 0, friction);

            //apply velocity to position
            BasePosition += velocity;

            //hold position on the map
            BasePosition.X = MathHelper.Clamp(BasePosition.X, Tools.ArenaBounds.Left + (int) (BaseImg.Width * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Right - (int) (BaseImg.Width * IMG_SCALE_FACTOR / 2.0));
            BasePosition.Y = MathHelper.Clamp(BasePosition.Y, Tools.ArenaBounds.Top + (int) (BaseImg.Height * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Bottom - (int) (BaseImg.Height * IMG_SCALE_FACTOR / 2.0));

            //if key is pressed rotate tank
            if (isKeyPressed)
            {
                //rotate tank towards the vector of velocity
                BaseRotation = Tools.RotateTowardsVector(BaseRotation, velocity * new Vector2(1, -1), ROTATION_SPEED) + 180;
                BaseRotation += 180;
                BaseRotation %= 360;
            }
        }

        /// <summary>
        /// preforms appropriate reaction based on object collided with
        /// </summary>
        /// <param name="collided">the object this collided with</param>
        public override void Collide(object collided)
        {
            //chooses appropriate reactions
            switch (collided)
            {
                case Bullet bullet:
                    //applies bullet damage
                    Health -= bullet.Damage;
                    break;
                case BomberEnemy _:
                    //applies collision damage
                    Health -= ENEMY_COLLIDE_DAMAGE;
                    break;
                case RedMine _:
                    //kills player
                    Health = 0;
                    break;
            }

            //holds health uder max health
            if (Health > StartingHealth)
            {
                //sets health to starting health
                Health = StartingHealth;
            }
        }

        /// <summary>
        /// Gets the type of the tank.
        /// </summary>
        /// <returns>the tyoe of the tank</returns>
        public override TankType GetTankType() => TankType.Player;

        /// <summary>
        /// nothing
        /// </summary>
        /// <returns>null</returns>
        /// <seealso cref="Tank.Clone(Vector2, float, Stage)"/>
        public override Tank Clone(Vector2 position, float rotation, Stage stage) => null;

        /// <summary>
        /// Steps the out of shop (moves to the coordinates beside shop). called when player in shop
        /// </summary>
        public void StepOutOfShop()
        {
            //moves to the coordinates beside shop and zeros velocity
            velocity = Vector2.Zero;
            BasePosition = Tools.ArenaBounds.Location.ToVector2() + new Vector2(150, 150);
        }

        /// <summary>
        /// assigns new cannon to player cannon
        /// </summary>
        /// <param name="newCannon">The new cannon.</param>
        public void TakeCannon(Cannon newCannon)
        {
            //assigns given cannon to player cannon (updates it right away so it doesnt draw in the worng spot)
            Cannon = newCannon;
            Cannon.Update(BasePosition, BaseRotation, Tools.TrueMousePos);
        }

        /// <summary>
        /// applies the speed boost
        /// </summary>
        public void SpeedUp()
        {
            //starts speed boost timer
            speedBoostTimer.Reset();

            //changes speed variables so the player can go faster
            accRate = 3;
            friction = 1.5f;
            maxSpeed = 20;
        }
    }
}