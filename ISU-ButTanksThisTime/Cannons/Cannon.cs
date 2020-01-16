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
        private readonly float scaleFactor;

        private Vector2 pos;
        private readonly float disFromCentreBase;
        private float rotation = 0;
        private float rotationSpeed;

        //Shooting Variables
        private readonly Timer shootTimer = new Timer(1000);
        private readonly Owner owner;
        public bool active;

        public Cannon(int fireRate, int damage, bool active, float disFromCentreBase, float scaleFactor, Owner owner)
        {
            this.scaleFactor = scaleFactor;
            this.disFromCentreBase = disFromCentreBase;
            this.owner = owner;
            this.active = active;
            this.rotationSpeed = rotationSpeed;
        }


        public void Update(Vector2 basePos, float baseRotation, float rotation)
        {
            pos = new Vector2((float)Math.Cos(baseRotation), (float)-Math.Sin(baseRotation)) * -disFromCentreBase;
            pos += basePos;

            this.rotation = baseRotation + rotation;

            if (shootTimer.IsTimeUp(Tools.gameTime) && active)
            {
                shootTimer.Reset();
                GameScene.AddBullet(new MeduimBullet(pos, this.rotation, scaleFactor, owner));
            }
           // Console.WriteLine(MathHelper.ToDegrees(rotation));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, pos, null, Color.White, -rotation + MathHelper.PiOver2, new Vector2(img.Width * 0.5f, img.Height * 0.75f), scaleFactor, SpriteEffects.None, 1f);
        }
        public float Rotation { get => Rotation; private set => Rotation = value; }
        public Vector2 GetPosition() => pos;
        public float GetRotation() => rotation;

    }
}
