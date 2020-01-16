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
    class BomberEnemie :Tank
    {
        private const float IMG_SCALE_FACTOR = 0.25f;
        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;
        public BomberEnemie(Vector2 position) : base(position, IMG_SCALE_FACTOR)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/Hull_04");
            cannon = new BomberEnemieCannon(CANNON_DIS_FROM_CENTRE, IMG_SCALE_FACTOR, Owner.Enemie);
            cannon.active = false;

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
        }
        public override void Collide(object collided)
        {
            switch (collided)
            {
                case Bullet _:
                    Bullet bullet = collided as Bullet;
                    if(bullet.bulletOwner == Owner.Player)
                    {
                        health -= 25;
                    }
                    break;
                case Player _:
                    health = 0;
                    break;
            }
        }

        public override Stage GetStage()
        {
            return Stage.Low;
        }
    }
}

