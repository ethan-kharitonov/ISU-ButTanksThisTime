using System;
using Animation2D;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.LandMines;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_ButTanksThisTime.Tanks
{
    internal class Player : Tank
    {
        //Movment Variables
        private Vector2 velocity = new Vector2(0, 0);
        private float ACC_RATE = 1f;
        private float FRICTION = 0.4f;
        private int MAX_SPEED = 10;
        private bool isKeyPressed = false;
        private const int ROTATION_SPEED = 5;

        private Timer speedBoostTimer = new Timer(5000);

        //Cannon Variables
        private bool canControleShooting = true;

        public Player(Vector2 position) : base(position, Stage.Player, 0, 0, 0, 1000, 0)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1PP");
            cannon = new TierOneCannon(Owner.Player, Stage.Player, basePosition, baseRotation);
            basePosition = position;

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
        }

        public override bool Update(Vector2 NA)
        {
            var kb = Keyboard.GetState();

            if (speedBoostTimer.IsTimeUp(Tools.GameTime) && ACC_RATE != 1)
            {
                ACC_RATE = 1f;
                FRICTION = 0.4f;
                MAX_SPEED = 10;
            }

            MoveTank(kb);
            CannonUpdate(kb);
            bar.Update(basePosition, health);

            if (health <= 0)
            {
                Game1.state = State.LoseScreen;
            }

            Console.WriteLine(health);
            return false;
        }

        private void CannonUpdate(KeyboardState kb)
        {
            cannon.active = Mouse.GetState().LeftButton == ButtonState.Pressed && canControleShooting && GameScene.AreAnyBulletsLeft();

            cannon.Update(basePosition, baseRotation, Tools.TrueMousePos);
        }

        private void MoveTank(KeyboardState kb)
        {
            isKeyPressed = false;

            if (kb.IsKeyDown(Keys.D))
            {
                velocity.X += ACC_RATE;
                isKeyPressed = true;
            }

            if (kb.IsKeyDown(Keys.A))
            {
                velocity.X -= ACC_RATE;
                isKeyPressed = true;
            }

            if (kb.IsKeyDown(Keys.W))
            {
                velocity.Y -= ACC_RATE;
                isKeyPressed = true;
            }

            if (kb.IsKeyDown(Keys.S))
            {
                velocity.Y += ACC_RATE;
                isKeyPressed = true;
            }

            velocity.X = MathHelper.Clamp(velocity.X, -MAX_SPEED, MAX_SPEED);
            velocity.Y = MathHelper.Clamp(velocity.Y, -MAX_SPEED, MAX_SPEED);

            velocity.X = Tools.ApproachValue(velocity.X, 0, FRICTION);
            velocity.Y = Tools.ApproachValue(velocity.Y, 0, FRICTION);

            basePosition += velocity;

            basePosition.X = MathHelper.Clamp(basePosition.X, Tools.ArenaBounds.Left + (int) (baseImg.Width * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Right - (int) (baseImg.Width * IMG_SCALE_FACTOR / 2.0));
            basePosition.Y = MathHelper.Clamp(basePosition.Y, Tools.ArenaBounds.Top + (int) (baseImg.Height * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Bottom - (int) (baseImg.Height * IMG_SCALE_FACTOR / 2.0));

            if (isKeyPressed)
            {
                baseRotation = Tools.RotateTowardsVector(baseRotation, velocity * new Vector2(1, -1), ROTATION_SPEED) + 180;
                baseRotation += 180;
                baseRotation %= 360;
            }
        }

        public override void Collide(object collided)
        {
            switch (collided)
            {
                case Bullet _:
                    var bullet = collided as Bullet;
                    health -= bullet.Damage;
                    break;
                case BomberEnemie _:
                    health -= 25;
                    break;
                case RedMine _:
                    health = 0;
                    break;
            }

            if (health > startingHealth)
            {
                health = startingHealth;
            }
        }

        public Vector2 GetBasePosition() => basePosition;

        public Vector2 GetCannonPosition() => cannon.GetPosition();

        public float GetCannonRotation() => cannon.GetRotation();

        public float GetScaleFactor() => IMG_SCALE_FACTOR;

        public void CollideWithObject(Rectangle obstical)
        {
            var obsticalBox = new RotatedRectangle(obstical, 0, Vector2.Zero);
            var rotBox = GetRotatedRectangle();
            if (Tools.BoxBoxCollision(obsticalBox, rotBox) == null)
            {
                return;
            }

            isKeyPressed = false;

            var top = (int) Math.Min(Math.Min(rotBox.TopLeft.Y, rotBox.TopRight.Y), Math.Min(rotBox.BotomLeft.Y, rotBox.BotomRight.Y));
            var bottom = (int) Math.Max(Math.Max(rotBox.TopLeft.Y, rotBox.TopRight.Y), Math.Max(rotBox.BotomLeft.Y, rotBox.BotomRight.Y));
            var left = (int) Math.Min(Math.Min(rotBox.TopLeft.X, rotBox.TopRight.X), Math.Min(rotBox.BotomLeft.X, rotBox.BotomRight.X));
            var right = (int) Math.Max(Math.Max(rotBox.TopLeft.X, rotBox.TopRight.X), Math.Max(rotBox.BotomLeft.X, rotBox.BotomRight.X));

            var processX = true;
            var processY = true;
            if ((basePosition.X < obstical.Left || basePosition.X > obstical.Right) &&
                (basePosition.Y < obstical.Top || basePosition.Y > obstical.Bottom))
            {
                processX = Tools.Rnd.Next(0, 10000) < 5000;
                processY = !processX;
            }

            var trueRectangle = new Rectangle(left, top, right - left, bottom - top);

            if (basePosition.X < obstical.Left && processX)
            {
                basePosition.X = obstical.Left - trueRectangle.Width * 0.5f;
                velocity.X = 0;
            }
            else if (basePosition.X > obstical.Right && processX)
            {
                basePosition.X = obstical.Right + trueRectangle.Width * 0.5f;
                velocity.X = 0;
            }

            if (basePosition.Y < obstical.Top && processY)
            {
                basePosition.Y = obstical.Top - trueRectangle.Height * 0.5f;
                velocity.Y = 0;
            }
            else if (basePosition.Y > obstical.Bottom && processY)
            {
                basePosition.Y = obstical.Bottom + trueRectangle.Height * 0.5f;
                velocity.Y = 0;
            }
        }

        public override TankType GetTankType() => TankType.Player;

        public override Tank Clone(Vector2 position, float rotation, Stage stage) => throw new NotImplementedException();

        public void StepOutOfShop()
        {
            velocity = Vector2.Zero;
            basePosition = Tools.ArenaBounds.Location.ToVector2() + new Vector2(150, 150);
        }

        public void TakeCannon(Cannon newCannon)
        {
            cannon = newCannon;
            cannon.Update(basePosition, baseRotation, Tools.TrueMousePos);
        }

        public void SpeedUp()
        {
            speedBoostTimer.Reset();
            ACC_RATE = 3f;
            FRICTION = 1.5f;
            MAX_SPEED = 20;
        }
    }
}