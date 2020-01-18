using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
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

    abstract class Tank
    {
        //Base Variables
        protected Texture2D baseImg;
        protected Vector2 basePosition;
        protected float baseRotation = 0;
        public const float IMG_SCALE_FACTOR = 0.25f;

        //Cannon Variables
        protected Cannon cannon;

        //Movment Variables
        protected float speed = 3;
        private readonly int ROTATION_SPEED = 5;
        protected float attackRange;

        //Dying Variable
        protected Animation explosionAnimation;

        //Health Variables
        protected int health;
        protected HealthBar bar;
        protected bool killed = true;

        private Stage stage;
        public Tank(Vector2 position, Stage stage, float attackRange, float speed, int rotationSpeed, int health, float rotation = 0)
        {
            baseRotation = rotation;
            basePosition = position;
            this.stage = stage;
            this.attackRange = attackRange;
            ROTATION_SPEED = rotationSpeed;
            this.health = health;
            this.speed = speed;

            bar = new HealthBar(health);
        }

        public virtual bool Update(Vector2 target)
        {
            Vector2 distance = target - basePosition;
            if((int)distance.Length() >= attackRange)
            {
                if(distance.Length() < speed)
                {
                    basePosition = target;
                }
                else
                {
                    basePosition += distance / distance.Length() * speed;
                }
            }

            baseRotation = Tools.RotateTowardsVector(baseRotation, (target - basePosition) * new Vector2(1, -1), ROTATION_SPEED);

            cannon.Update(basePosition, baseRotation, target);

            if (health <= 0)
            {
                explosionAnimation.Update(Tools.GameTime);
            }
            else
            {
                Vector2 explosionPos = new Vector2(basePosition.X - explosionAnimation.destRec.Width / 2, basePosition.Y - explosionAnimation.destRec.Height / 2);
                explosionAnimation.destRec = new Rectangle((int)explosionPos.X, (int)explosionPos.Y, explosionAnimation.destRec.Width, explosionAnimation.destRec.Height);
            }

            bar.Update(basePosition, health);

            if(!explosionAnimation.isAnimating && health <= 0)
            {
                if (killed)
                {
                    DropItem();
                }
                return true;
            }

            return false;
        }

        protected virtual bool Update(Vector2 target, Vector2 cannonTarget)
        {
            Vector2 distance = target - basePosition;
            if ((int)distance.Length() >= attackRange)
            {
                if (distance.Length() < speed)
                {
                    basePosition = target;
                }
                else
                {
                    basePosition += distance / distance.Length() * speed;
                }
            }

            baseRotation = Tools.RotateTowardsVector(baseRotation, (target - basePosition) * new Vector2(1, -1), ROTATION_SPEED);

            cannon.Update(basePosition, baseRotation, cannonTarget);

            if (health <= 0)
            {
                explosionAnimation.Update(Tools.GameTime);
            }
            else
            {
                Vector2 explosionPos = new Vector2(basePosition.X - explosionAnimation.destRec.Width / 2, basePosition.Y - explosionAnimation.destRec.Height / 2);
                explosionAnimation.destRec = new Rectangle((int)explosionPos.X, (int)explosionPos.Y, explosionAnimation.destRec.Width, explosionAnimation.destRec.Height);
            }

            bar.Update(basePosition, health);

            return !explosionAnimation.isAnimating && health <= 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (health <= 0)
            {
                explosionAnimation.Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
            else
            {
                spriteBatch.Draw(baseImg, basePosition, null, Color.White, -MathHelper.ToRadians(baseRotation) + MathHelper.PiOver2, new Vector2(baseImg.Width / 2, baseImg.Height / 2), IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
                cannon.Draw(spriteBatch);
                bar.Draw(spriteBatch);
            }


            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().TopLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().TopRight, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().BotomLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().BotomRight, Color.White);

        }

        public RotatedRectangle GetRotatedRectangle()
        {
            Rectangle box = new Rectangle((int)basePosition.X, (int)basePosition.Y, (int)(baseImg.Width * IMG_SCALE_FACTOR), (int)(baseImg.Height * IMG_SCALE_FACTOR));
            return new RotatedRectangle(box, MathHelper.ToRadians(baseRotation) + MathHelper.PiOver2, new Vector2(box.Width * 0.5f, box.Height * 0.5f));
        }

        public Vector2 GetPos() => basePosition;

        public virtual void Collide(object collided)
        {
            switch (collided)
            {
                case Bullet _:
                    Bullet bullet = collided as Bullet;
                    if (bullet.bulletOwner == Owner.Player)
                    {
                        health -= 25;
                    }
                    break;
                case Tank _:
                    if(!(collided is Player))
                    {
                        health = 0;
                    }
                    break;
                case LandMine _:
                    health = 0;
                    break;
            }
        }

        public Stage GetStage() => stage;

        public float GetRotation() => baseRotation;

        public Vector2 Dimensions => new Vector2(baseImg.Width * IMG_SCALE_FACTOR, baseImg.Height * IMG_SCALE_FACTOR);

        public Vector2 GetOrigin() => new Vector2(baseImg.Width * 0.5f * IMG_SCALE_FACTOR, baseImg.Height * 0.5f * IMG_SCALE_FACTOR);


        public abstract TankType GetTankType();

        public abstract Tank Clone(Vector2 position, float rotation, Stage stage);

        protected void DropItem()
        {
            int chance = Tools.Rnd.Next(76, 100);
            switch (chance)
            {
                case int n when n < 25:
                    GameScene.AddItem(new RelocateItem(basePosition));
                    break;
                case int n when n < 50:
                    GameScene.AddItem(new SpeedBoostItem(basePosition));
                    break;
                case int n when n < 75:
                    GameScene.AddItem(new CoinItem(basePosition));
                    break;
                case int n when n < 100:
                    GameScene.AddItem(new CoinPileItem(basePosition));
                    break;
            }
        }
    }
}
