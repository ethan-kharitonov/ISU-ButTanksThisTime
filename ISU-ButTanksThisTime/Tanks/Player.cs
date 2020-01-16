using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class Player : Tank
    {
        //Image Variables
        private const float IMG_SCALE_FACTOR = 0.25f;

        //Movment Variables
        private Vector2 velocity = new Vector2(0, 0);
        private const float ACC_RATE = 0.5f;
        private const float FRICTION = 0.2f;
        private const int MAX_SPEED = 5;
        private KeyboardState kb;
        private bool isKeyPressed = false;

        //Cannon Variables
        private float cannonRotation = 0;
        private const int CANNON_ROTATION_SPEED = 2;
        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;

        public Player(Vector2 position) : base(position, IMG_SCALE_FACTOR)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1PP");
            cannon = new TierOneCannon(CANNON_DIS_FROM_CENTRE, IMG_SCALE_FACTOR, Owner.Player, Stage.Player);
            basePosition = position;

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().TopLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().TopRight, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().BotomLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, GetRotatedRectangle().BotomRight, Color.White);
        }

        public override bool Update(Vector2 target, float targetDistance = 0)
        {
            kb = Keyboard.GetState();
            MoveTank();

            //Cannon Update
            if (kb.IsKeyDown(Keys.Right))
            {
                cannonRotation -= MathHelper.ToRadians(CANNON_ROTATION_SPEED);
            }

            if (kb.IsKeyDown(Keys.Left))
            {
                cannonRotation += MathHelper.ToRadians(CANNON_ROTATION_SPEED);
            }

            cannonRotation %= MathHelper.TwoPi;
            cannonRotation %= -MathHelper.TwoPi;

            cannon.active = kb.IsKeyDown(Keys.Up);
            cannon.Update(basePosition, baseRotation, cannonRotation);

            return false;
        }
        private void MoveTank()
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

            basePosition.X = MathHelper.Clamp(basePosition.X, Tools.ArenaBounds.Left + (int)(baseImg.Width * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Right - (int)(baseImg.Width * IMG_SCALE_FACTOR / 2.0));
            basePosition.Y = MathHelper.Clamp(basePosition.Y, Tools.ArenaBounds.Top + (int)(baseImg.Height * IMG_SCALE_FACTOR / 2.0), Tools.ArenaBounds.Bottom - (int)(baseImg.Height * IMG_SCALE_FACTOR / 2.0));

            if (isKeyPressed)
            {
                baseRotation = Tools.RotateTowardsVectorTest(baseRotation, velocity * new Vector2(1, -1), ROTATION_SPEED) + 180;
                baseRotation += 180;
                baseRotation %= 360;
            }
        }
        public override void Collide(object collided)
        {
            
        }
        public Vector2 GetBasePosition() => basePosition;
        public Vector2 GetCannonPosition() => cannon.GetPosition();
        public float GetCannonRotation() => cannon.GetRotation();
        public float GetScaleFactor() => IMG_SCALE_FACTOR;
        public void CollideWithObject(Rectangle obstical)
        {
            RotatedRectangle obsticalBox = new RotatedRectangle(obstical, 0, Vector2.Zero);
            Console.WriteLine(obstical.Location);
            RotatedRectangle rotBox = GetRotatedRectangle();
            if (!Tools.BoxBoxCollision(obsticalBox, rotBox))
            {
                return;
            }

            isKeyPressed = false;

            int top = (int)Math.Min(Math.Min(rotBox.TopLeft.Y, rotBox.TopRight.Y),Math.Min(rotBox.BotomLeft.Y, rotBox.BotomRight.Y));
            int bottom = (int)Math.Max(Math.Max(rotBox.TopLeft.Y, rotBox.TopRight.Y), Math.Max(rotBox.BotomLeft.Y, rotBox.BotomRight.Y));
            int left = (int)Math.Min(Math.Min(rotBox.TopLeft.X, rotBox.TopRight.X), Math.Min(rotBox.BotomLeft.X, rotBox.BotomRight.X));
            int right = (int)Math.Max(Math.Max(rotBox.TopLeft.X, rotBox.TopRight.X), Math.Max(rotBox.BotomLeft.X, rotBox.BotomRight.X));

            Rectangle trueRectangle = new Rectangle(left, top, right - left, bottom - top);

            if (basePosition.X < obstical.Left)
            {
                basePosition.X = obstical.Left - trueRectangle.Width * 0.5f;
                velocity.X = 0;
            }
            else if (basePosition.X > obstical.Right)
            {
                basePosition.X = obstical.Right + trueRectangle.Width * 0.5f;
                velocity.X = 0;
            }

            if (basePosition.Y < obstical.Top)
            {
                basePosition.Y = obstical.Top - trueRectangle.Height * 0.5f;
                velocity.Y = 0;
            }
            else if (basePosition.Y > obstical.Bottom)
            {
                basePosition.Y = obstical.Bottom + trueRectangle.Height * 0.5f;
                velocity.Y = 0;
            }
        }
        public override Stage GetStage()
        {
            return Stage.Player;
        }
    }
}
