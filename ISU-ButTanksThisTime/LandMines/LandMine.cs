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
    abstract class LandMine
    {
        protected Animation[] animations = new Animation[3];
        protected Texture2D explosionEffect;
        private int currentAnim = 0;
        private bool active = false;
        private float raduis;
        private float explosionRaduis;
        public LandMine(int raduis, int damage, float explosionRaduis)
        {
            this.raduis = raduis;
            this.explosionRaduis = explosionRaduis;
        }
        public bool Update()
        {
            animations[currentAnim].Update(Tools.gameTime);
            if(currentAnim == 1 && !animations[currentAnim].isAnimating)
            {
                currentAnim = 2;
                active = true;
            }

            return currentAnim == 2 && !animations[currentAnim].isAnimating;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animations[currentAnim].Draw(spriteBatch, Color.White, SpriteEffects.None);
            /*Rectangle box2 = new Rectangle(animations[currentAnim].destRec.Center, new Vector2(raduis, raduis).ToPoint());
            spriteBatch.Draw(explosionEffect, box2, null, Color.Red, 0, new Vector2(explosionEffect.Width * 0.5f, explosionEffect.Height * 0.5f), SpriteEffects.None, 1);
*/
            if (currentAnim == 2)
            {
                Rectangle box = new Rectangle(animations[currentAnim].destRec.Center, new Vector2(explosionRaduis, explosionRaduis).ToPoint());
                spriteBatch.Draw(explosionEffect, box, null, Color.White, 0, new Vector2(explosionEffect.Width * 0.5f, explosionEffect.Height * 0.5f), SpriteEffects.None, 1);
            }
        }

        public void Collide()
        {
            currentAnim = currentAnim == 0 ? 1 : currentAnim;
        }

        public RotatedRectangle Box => new RotatedRectangle(animations[currentAnim].destRec, 0, Vector2.Zero);

        public Circle GetExplosionArea()
        {
            Vector2 centre = animations[currentAnim].destRec.Center.ToVector2();
            return new Circle(centre, explosionRaduis);
        }
        public bool IsActive() => active;
    }
}
