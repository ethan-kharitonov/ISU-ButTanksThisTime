﻿using Animation2D;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.Collectibles;
using ISU_ButTanksThisTime.LandMines;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    public enum TankType
    {
        BasicPath = 0,
        Bomber = 1,
        RotateShooter = 2,
        Burst = 3,
        MineDroper = 4,
        Healer = 5,
        Player = 6
    }

    internal abstract class Tank
    {
        //Base Variables
        protected Texture2D BaseImg;
        protected Vector2 BasePosition;
        protected float BaseRotation;
        public const float IMG_SCALE_FACTOR = 0.25f;

        //Cannon Variables
        protected Cannon Cannon;

        //Movment Variables
        protected float Speed;
        private readonly int rotationSpeed;
        protected float AttackRange;

        //Health Variables
        protected int Health;
        protected HealthBar Bar;
        protected bool Killed = true;
        protected readonly int StartingHealth;
        protected Animation ExplosionAnimation;

        //the stage of this tank
        private readonly Stage stage;

        public Tank(Vector2 position, Stage stage, float attackRange, float speed, int rotationSpeed, int health, float rotation = 0)
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

        public virtual bool Update(Vector2 target)
        {
            //call cannon update
            Cannon.Update(BasePosition, BaseRotation, target);
            return UpdateBaseAndHealthBar(target);
        }

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
                //if this object will overstep target this update and adjast the step
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

            //return true if dead and explosion done animatig
            if (!ExplosionAnimation.isAnimating && Health <= 0)
            {
                if (Killed)
                {
                    DropItem();
                }

                return true;
            }
            return false;
        }

        protected virtual bool Update(Vector2 target, Vector2 cannonTarget)
        {
            //update cannon and call UpdateBaseAndHealthBar
            Cannon.Update(BasePosition, BaseRotation, cannonTarget);
            return UpdateBaseAndHealthBar(target);
        }

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

        public RotatedRectangle GetRotatedRectangle()
        {
            //calculate the regular rectangle of the tank and construct a rotated rectangle
            var box = new Rectangle((int) BasePosition.X, (int) BasePosition.Y, (int) (BaseImg.Width * IMG_SCALE_FACTOR), (int) (BaseImg.Height * IMG_SCALE_FACTOR));
            return new RotatedRectangle(box, MathHelper.ToRadians(BaseRotation) + MathHelper.PiOver2, new Vector2(box.Width * 0.5f, box.Height * 0.5f));
        }

        public Vector2 GetPos() => BasePosition;

        public virtual void Collide(object collided)
        {
            //choose a reaction based on the object collided with
            switch (collided)
            {
                case Bullet _:
                    //appla the bullets damage
                    var bullet = collided as Bullet;
                    Health -= bullet.Damage;
                    break;
                case Tank _:
                    //if tank is not player kill self
                    if (!(collided is Player))
                    {
                        //set heaklth to zero
                        Health = 0;
                    }

                    break;
                case LandMine _:
                    //set heaklth to zero
                    Health = 0;
                    break;
            }
        }

        public Stage GetStage() => stage;

        public float GetRotation() => BaseRotation;

        public Vector2 Dimensions => new Vector2(BaseImg.Width * IMG_SCALE_FACTOR, BaseImg.Height * IMG_SCALE_FACTOR);

        public Vector2 GetOrigin() => new Vector2(BaseImg.Width * 0.5f * IMG_SCALE_FACTOR, BaseImg.Height * 0.5f * IMG_SCALE_FACTOR);


        public abstract TankType GetTankType();

        public abstract Tank Clone(Vector2 position, float rotation, Stage stage);

        protected void DropItem()
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

        public void Heal(int healAmount) => Health += healAmount;

        public int GetHealth() => Health;
    }
}