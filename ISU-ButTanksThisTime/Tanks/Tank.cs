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

        //Dying Variable
        protected Animation ExplosionAnimation;

        //Health Variables
        protected int Health;
        protected HealthBar Bar;
        protected bool Killed = true;
        protected readonly int StartingHealth;

        private readonly Stage stage;

        public Tank(Vector2 position, Stage stage, float attackRange, float speed, int rotationSpeed, int health, float rotation = 0)
        {
            BaseRotation = rotation;
            BasePosition = position;
            this.stage = stage;
            AttackRange = attackRange;
            this.rotationSpeed = rotationSpeed;
            StartingHealth = health;
            Health = health;
            Speed = speed;

            Bar = new HealthBar(health, position);
        }

        public virtual bool Update(Vector2 target)
        {
            if (Health > StartingHealth)
            {
                Health = StartingHealth;
            }

            var distance = target - BasePosition;
            if ((int) distance.Length() >= AttackRange)
            {
                if (distance.Length() < Speed)
                {
                    BasePosition = target;
                }
                else
                {
                    BasePosition += distance / distance.Length() * Speed;
                }
            }

            BaseRotation = Tools.RotateTowardsVector(BaseRotation, (target - BasePosition) * new Vector2(1, -1), rotationSpeed);

            Cannon.Update(BasePosition, BaseRotation, target);

            if (Health <= 0)
            {
                ExplosionAnimation.Update(Tools.GameTime);
            }
            else
            {
                var explosionPos = new Vector2(BasePosition.X - ExplosionAnimation.destRec.Width / 2F, BasePosition.Y - ExplosionAnimation.destRec.Height / 2F);
                ExplosionAnimation.destRec = new Rectangle((int) explosionPos.X, (int) explosionPos.Y, ExplosionAnimation.destRec.Width, ExplosionAnimation.destRec.Height);
            }

            Bar.Update(BasePosition, Health);

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
            if (Health > StartingHealth)
            {
                Health = StartingHealth;
            }

            var distance = target - BasePosition;
            if ((int) distance.Length() >= AttackRange)
            {
                if (distance.Length() < Speed)
                {
                    BasePosition = target;
                }
                else
                {
                    BasePosition += distance / distance.Length() * Speed;
                }
            }

            BaseRotation = Tools.RotateTowardsVector(BaseRotation, (target - BasePosition) * new Vector2(1, -1), rotationSpeed);

            Cannon.Update(BasePosition, BaseRotation, cannonTarget);

            if (Health <= 0)
            {
                ExplosionAnimation.Update(Tools.GameTime);
            }
            else
            {
                var explosionPos = new Vector2(BasePosition.X - ExplosionAnimation.destRec.Width / 2F, BasePosition.Y - ExplosionAnimation.destRec.Height / 2F);
                ExplosionAnimation.destRec = new Rectangle((int) explosionPos.X, (int) explosionPos.Y, ExplosionAnimation.destRec.Width, ExplosionAnimation.destRec.Height);
            }

            Bar.Update(BasePosition, Health);

            return !ExplosionAnimation.isAnimating && Health <= 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Health <= 0)
            {
                ExplosionAnimation.Draw(spriteBatch, Color.White, SpriteEffects.None);
            }
            else
            {
                spriteBatch.Draw(BaseImg, BasePosition, null, Color.White, -MathHelper.ToRadians(BaseRotation) + MathHelper.PiOver2, new Vector2(BaseImg.Width / 2F, BaseImg.Height / 2F), IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
                Cannon.Draw(spriteBatch);
                Bar.Draw(spriteBatch);
            }
        }

        public RotatedRectangle GetRotatedRectangle()
        {
            var box = new Rectangle((int) BasePosition.X, (int) BasePosition.Y, (int) (BaseImg.Width * IMG_SCALE_FACTOR), (int) (BaseImg.Height * IMG_SCALE_FACTOR));
            return new RotatedRectangle(box, MathHelper.ToRadians(BaseRotation) + MathHelper.PiOver2, new Vector2(box.Width * 0.5f, box.Height * 0.5f));
        }

        public Vector2 GetPos() => BasePosition;

        public virtual void Collide(object collided)
        {
            switch (collided)
            {
                case Bullet _:
                    var bullet = collided as Bullet;
                    Health -= bullet.Damage;
                    break;
                case Tank _:
                    if (!(collided is Player))
                    {
                        Health = 0;
                    }
                    break;
                case LandMine _:
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
            int chance = Tools.Rnd.Next(0, 210);
            switch (chance)
            {
                case int n when n < 20:
                    GameScene.AddItem(new RelocateItem(BasePosition));
                    break;
                case int n when n < 40:
                    GameScene.AddItem(new SpeedBoostItem(BasePosition));
                    break;
                case int n when n < 100:
                    GameScene.AddItem(new CoinItem(BasePosition));
                    break;
                case int n when n < 130:
                    GameScene.AddItem(new CoinPileItem(BasePosition));
                    break;
                case int n when n < 170:
                    GameScene.AddItem(new Ammo(BasePosition));
                    break;
                case int n when n < 210:
                    GameScene.AddBullet(new MedKit(BasePosition));
                    break;
            }
        }

        public void Heal(int healAmount)
        {
            Health += healAmount;
        }

        public int GetHealth() => Health;
    }
}
