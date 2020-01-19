using System;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal abstract class Cannon
    {
        protected Vector2 Pos;
        private static readonly float disFromCentreBase = 35 * Tank.IMG_SCALE_FACTOR;
        protected float Rotation;

        //Shooting Variables
        private readonly Timer shootTimer;
        public bool Active;
        private readonly int rotateSpeed;

        public Cannon(int fireRate, int rotationSpeed, bool active, Vector2 position, float rotation)
        {
            shootTimer = new Timer(fireRate);
            rotateSpeed = rotationSpeed;
            Active = active;
            Pos = CalcPos(position, rotation);
            Rotation = rotation;
        }

        public virtual void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {
            Pos = CalcPos(basePos, baseRotation);

            target -= Pos;
            target *= new Vector2(1, -1);
            Rotation = Tools.RotateTowardsVector(Rotation, target, rotateSpeed);
            if (shootTimer.IsTimeUp(Tools.GameTime) && Active && !GameScene.GameIsFrozen())
            {
                shootTimer.Reset();
                var newBullet = Bullet.Clone(Pos, Rotation);
                GameScene.AddBullet(newBullet);
                if (newBullet.BulletOwner == Owner.Player)
                {
                    GameScene.RemoveBullet();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Img, Pos, null, Color.White, -MathHelper.ToRadians(Rotation) + MathHelper.PiOver2, new Vector2(Img.Width * 0.5f, Img.Height * 0.75f), Tank.IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
        }

        protected abstract Bullet Bullet { get; }
        public abstract Texture2D Img { get; }

        public Vector2 GetPosition() => Pos;

        public float GetRotation() => Rotation;

        protected Vector2 CalcPos(Vector2 basePosition, float baseRotation)
        {
            var position = new Vector2((float) Math.Cos(MathHelper.ToRadians(baseRotation)), (float) -Math.Sin(MathHelper.ToRadians(baseRotation))) * -disFromCentreBase;
            position += basePosition;

            return position;
        }
    }
}