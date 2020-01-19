using System;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal abstract class Cannon
    {
        protected Vector2 pos;
        private static readonly float disFromCentreBase = 35 * Tank.IMG_SCALE_FACTOR;
        protected float rotation;

        //Shooting Variables
        private readonly Timer shootTimer;
        public bool active;
        private readonly int ROTATE_SPEED;

        public Cannon(int fireRate, int rotationSpeed, bool active, Vector2 position, float rotation)
        {
            shootTimer = new Timer(fireRate);
            ROTATE_SPEED = rotationSpeed;
            this.active = active;
            pos = CalcPos(position, rotation);
            this.rotation = rotation;
        }

        public virtual void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {
            pos = CalcPos(basePos, baseRotation);

            target -= pos;
            target *= new Vector2(1, -1);
            rotation = Tools.RotateTowardsVector(rotation, target, ROTATE_SPEED);
            if (shootTimer.IsTimeUp(Tools.GameTime) && active && !GameScene.GameIsFrozen())
            {
                shootTimer.Reset();
                var newBullet = Bullet.Clone(pos, rotation);
                GameScene.AddBullet(newBullet);
                if (newBullet.bulletOwner == Owner.Player)
                {
                    GameScene.RemoveBullet();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, pos, null, Color.White, -MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2(Img.Width * 0.5f, Img.Height * 0.75f), Tank.IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
        }

        protected abstract Bullet Bullet { get; }
        public abstract Texture2D Img { get; }

        public Vector2 GetPosition() => pos;

        public float GetRotation() => rotation;

        protected Vector2 CalcPos(Vector2 basePosition, float baseRotation)
        {
            var position = new Vector2((float) Math.Cos(MathHelper.ToRadians(baseRotation)), (float) -Math.Sin(MathHelper.ToRadians(baseRotation))) * -disFromCentreBase;
            position += basePosition;

            return position;
        }
    }
}