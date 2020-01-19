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
using ISU_ButTanksThisTime.Collectibles;
using ISU_ButTanksThisTime.LandMines;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    /// <summary>
    /// Enum TankType
    /// </summary>
    public enum TankType
    {
        BasicPath = 0,
        Bomber = 1,
        RotateShooter = 2,
        Burst = 3,
        MineDropper = 4,
        Healer = 5,
        Player = 6
    }

    /// <summary>
    /// Class Tank.
    /// </summary>
    internal abstract class Tank
    {
        //Base Variables
        protected Texture2D BaseImg;
        protected Vector2 BasePosition;
        protected float BaseRotation;
        public const float IMG_SCALE_FACTOR = 0.25f;

        //Cannon Variables
        protected Cannon Cannon;

        //Movement Variables
        protected readonly float Speed;
        private readonly int rotationSpeed;
        protected float AttackRange;

        //Health Variables
        protected int Health;
        protected readonly HealthBar Bar;
        protected readonly int StartingHealth;
        protected Animation ExplosionAnimation;

        //indicates if the this tank has been killed by the player
        protected bool KilledByPlayer = true;
        
        //the stage of this tank
        private readonly Stage stage;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tank"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="attackRange">The attack range.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="rotationSpeed">The rotation speed.</param>
        /// <param name="health">The health.</param>
        /// <param name="rotation">The rotation.</param>
        protected Tank(Vector2 position, Stage stage, float attackRange, float speed, int rotationSpeed, int health, float rotation = 0)
        {
            //assign tank details
            BaseRotation = rotation;
            BasePosition = position;
            this.stage = stage;
            AttackRange = attackRange;
            this.rotationSpeed = rotationSpeed;
            StartingHealth = health;
            Health = health;
            Speed = speed;

            //create health bar
            Bar = new HealthBar(health, position);
        }

        /// <summary>
        /// moves the tank towards teh target
        /// </summary>
        /// <param name="target">what the tank moves towards</param>
        /// <returns>true if this tank is dead and done exploding, false otherwise</returns>
        public virtual bool Update(Vector2 target)
        {
            //call cannon update
            Cannon.Update(BasePosition, BaseRotation, target);
            return UpdateBaseAndHealthBar(target);
        }

        /// <summary>
        /// Updates the base and health bar.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool UpdateBaseAndHealthBar(Vector2 target)
        {
            //keep health under max health
            if (Health > StartingHealth)
            {
                Health = StartingHealth;
            }

            //check if target is in range to attack and this tank is not dead
            var distance = target - BasePosition;
            if ((int)distance.Length() >= AttackRange && Health > 0)
            {
                //if this object will overstep target this update and adjust the step
                if (distance.Length() < Speed)
                {
                    //equal this position ot target position
                    BasePosition = target;
                }
                else
                {
                    //move towards target by speed
                    BasePosition += distance / distance.Length() * Speed;
                }
            }

            //rotate towards target
            BaseRotation = Tools.RotateTowardsVector(BaseRotation, (target - BasePosition) * new Vector2(1, -1), rotationSpeed);


            //either play the explosion animation or update its position to match with the tank depending on if this tank is dead
            if (Health <= 0)
            {
                //update explosion animation
                ExplosionAnimation.Update(Tools.GameTime);
            }
            else
            {
                //update explosion animation position to match with the tank
                var explosionPos = new Vector2(BasePosition.X - ExplosionAnimation.destRec.Width / 2F, BasePosition.Y - ExplosionAnimation.destRec.Height / 2F);
                ExplosionAnimation.destRec = new Rectangle((int)explosionPos.X, (int)explosionPos.Y, ExplosionAnimation.destRec.Width, ExplosionAnimation.destRec.Height);
            }

            //Update the health bar
            Bar.Update(BasePosition, Health);

            //return true if dead and explosion done animating
            if (!ExplosionAnimation.isAnimating && Health <= 0)
            {
                if (KilledByPlayer)
                {
                    DropItem();
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="cannonTarget">The cannon target.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool Update(Vector2 target, Vector2 cannonTarget)
        {
            //update cannon and call UpdateBaseAndHealthBar
            Cannon.Update(BasePosition, BaseRotation, cannonTarget);
            return UpdateBaseAndHealthBar(target);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //either draw the explosion animation or the tank and cannon depending on if tank is dead
            if (Health <= 0)
            {
                //draw explosion animation
                ExplosionAnimation.Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
            else
            {
                //draw tank base, cannon and health bar
                spriteBatch.Draw(BaseImg, BasePosition, null, Color.White, -MathHelper.ToRadians(BaseRotation) + MathHelper.PiOver2, new Vector2(BaseImg.Width / 2F, BaseImg.Height / 2F), IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
                Cannon.Draw(spriteBatch);
                Bar.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Gets the rotated rectangle.
        /// </summary>
        /// <returns>RotatedRectangle.</returns>
        public RotatedRectangle GetRotatedRectangle()
        {
            //calculate the regular rectangle of the tank and construct a rotated rectangle
            var box = new Rectangle((int) BasePosition.X, (int) BasePosition.Y, (int) (BaseImg.Width * IMG_SCALE_FACTOR), (int) (BaseImg.Height * IMG_SCALE_FACTOR));
            return new RotatedRectangle(box, MathHelper.ToRadians(BaseRotation) + MathHelper.PiOver2, new Vector2(box.Width * 0.5f, box.Height * 0.5f));
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns>Vector2.</returns>
        public Vector2 GetPos() => BasePosition;

        /// <summary>
        /// Collides the specified collided.
        /// </summary>
        /// <param name="collided">The collided.</param>
        public virtual void Collide(object collided)
        {
            //choose a reaction based on the object collided with
            switch (collided)
            {
                case Bullet bullet:
                    //apply the bullets damage
                    Health -= bullet.Damage;
                    break;
                case Tank _:
                    //if tank is not player kill self
                    if (!(collided is Player))
                    {
                        //set health to zero
                        Health = 0;
                    }

                    break;
                case LandMine _:
                    //set health to zero
                    Health = 0;
                    break;
            }
        }

        /// <summary>
        /// Gets the stage of this tank
        /// </summary>
        /// <returns>the stage of this tank</returns>
        public Stage GetStage() => stage;

        /// <summary>
        /// Gets the rotation of this tank
        /// </summary>
        /// <returns>the rotation of the base of this tank</returns>
        public float GetRotation() => BaseRotation;

        /// <summary>
        /// returns the width and height of this tank
        /// </summary>
        /// <value>the width and height of this tank</value>
        protected Vector2 Dimensions => new Vector2(BaseImg.Width * IMG_SCALE_FACTOR, BaseImg.Height * IMG_SCALE_FACTOR);


        /// <summary>
        /// gets the type of this tank
        /// </summary>
        /// <returns>the type of this tank</returns>
        /// <seealso cref="TankType"/>
        public abstract TankType GetTankType();

        /// <summary>
        /// creates and instance of this tank
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="stage">The stage.</param>
        /// <returns>the new instance</returns>
        /// <remarks>
        /// acts as a constructor when havign an instance but not knowing the type
        /// </remarks>
        public abstract Tank Clone(Vector2 position, float rotation, Stage stage);

        /// <summary>
        /// chooses with loot to drop
        /// </summary>
        private void DropItem()
        {
            //randomly chase a number to decide which item to drop
            var chance = Tools.Rnd.Next(0, 210);
            switch (chance)
            {
                case int n when n < 20:
                    //drop relocate/freeze game item
                    GameScene.AddItem(new RelocateItem(BasePosition));
                    break;
                case int n when n < 40:
                    //drop speed boost item
                    GameScene.AddItem(new SpeedBoostItem(BasePosition));
                    break;
                case int n when n < 100:
                    //drop coin item
                    GameScene.AddItem(new CoinItem(BasePosition));
                    break;
                case int n when n < 130:
                    //drop coin pile item
                    GameScene.AddItem(new CoinPileItem(BasePosition));
                    break;
                case int n when n < 170:
                    //drop ammo item
                    GameScene.AddItem(new Ammo(BasePosition));
                    break;
                case int n when n < 210:
                    //drop med kit
                    GameScene.AddBullet(new MedKit(BasePosition));
                    break;
            }
        }

        /// <summary>
        /// adds the given heal amount to this tanks health
        /// </summary>
        /// <param name="healAmount">the amount of health to be added</param>
        public void Heal(int healAmount) => Health += healAmount;

        /// <summary>
        /// Gets the health of this tank
        /// </summary>
        /// <returns>the health of the player</returns>
        public int GetHealth() => Health;
    }
}