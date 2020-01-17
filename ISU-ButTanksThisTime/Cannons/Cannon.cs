using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    abstract class Cannon
    {
        protected Texture2D img;

        private Vector2 pos;
        private readonly float disFromCentreBase;
        private float rotation = 0;

        //Shooting Variables
        private readonly Timer shootTimer;
        public bool active;
        protected Bullet bullet;
        private const int ROTATE_SPEED = 3;

        public Cannon(int fireRate, int damage, bool active, float disFromCentreBase)
        {
            shootTimer = new Timer(fireRate);
            this.disFromCentreBase = disFromCentreBase;
            this.active = active;
        }


        public virtual void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {

            pos = new Vector2((float)Math.Cos(MathHelper.ToRadians(baseRotation)), (float)-Math.Sin(MathHelper.ToRadians(baseRotation))) * -disFromCentreBase;
            pos += basePos;

            target -= pos;
            target *= new Vector2(1, -1);
            rotation = Tools.RotateTowardsVector(rotation, target, 3);
            
            if (shootTimer.IsTimeUp(Tools.gameTime) && active)
            {
                shootTimer.Reset();
                Bullet newBullet = bullet.Clone(pos, this.rotation);
                GameScene.AddBullet(newBullet);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, pos, null, Color.White, -MathHelper.ToRadians(rotation) + MathHelper.PiOver2, new Vector2(img.Width * 0.5f, img.Height * 0.75f), Tank.IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
        }
        public float Rotation { get => Rotation; private set => Rotation = value; }
        public Vector2 GetPosition() => pos;
        public float GetRotation() => rotation;

    }
}
