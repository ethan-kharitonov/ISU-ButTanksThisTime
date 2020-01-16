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
    abstract class Tank
    {
        //Base Variables
        protected Texture2D baseImg;
        protected Vector2 basePosition;
        protected float baseRotation = 0;
        private readonly float imgScaleFactor;

        //Cannon Variables
        protected Cannon cannon;

        //Movment Variables
        protected float speed = 3;
        public static readonly float ROTATION_SPEED = 5;

        //Dying Variable
        protected Animation explosionAnimation;

        //Health Variables
        protected int health = 100;
        public Tank(Vector2 position, float imgScaleFactor, float rotation = 0)
        {
            baseRotation = rotation;
            this.imgScaleFactor = imgScaleFactor;
            basePosition = position;
            

        }

        public virtual bool Update(Vector2 target, float targetDistance = 0)
        {
            Vector2 distance = target - basePosition;
            if((int)distance.Length() >= targetDistance)
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

            baseRotation = Tools.RotateTowardsVectorTest(baseRotation, (target - basePosition) * new Vector2(1, -1), ROTATION_SPEED);

            cannon.Update(basePosition, baseRotation, 0);

            if (health <= 0)
            {
                explosionAnimation.Update(Tools.gameTime);
            }
            else
            {
                Vector2 explosionPos = new Vector2(basePosition.X - explosionAnimation.destRec.Width / 2, basePosition.Y - explosionAnimation.destRec.Height / 2);
                explosionAnimation.destRec = new Rectangle((int)explosionPos.X, (int)explosionPos.Y, explosionAnimation.destRec.Width, explosionAnimation.destRec.Height);
            }

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
                spriteBatch.Draw(baseImg, basePosition, null, Color.White, -MathHelper.ToRadians(baseRotation) + MathHelper.PiOver2, new Vector2(baseImg.Width / 2, baseImg.Height / 2), imgScaleFactor, SpriteEffects.None, 1f);
                //cannon.Draw(spriteBatch);
            }


        }

        public RotatedRectangle GetRotatedRectangle()
        {
            Rectangle box = new Rectangle((int)basePosition.X, (int)basePosition.Y, (int)(baseImg.Width * imgScaleFactor), (int)(baseImg.Height * imgScaleFactor));
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

        public abstract Stage GetStage();

        public float GetRotation() => baseRotation;

        public Vector2 GetOrigin() => new Vector2(baseImg.Width * 0.5f * imgScaleFactor, baseImg.Height * 0.5f * imgScaleFactor);
    
    }
}
