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
        public BomberEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, rotation)
        {

            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/BomberEnemie/BP" + ((int)stage + 1));

            cannon = new BomberEnemieCannon(stage, basePosition, baseRotation);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
        }

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new BomberEnemie(position, rotation, stage);
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
                case LandMine _:
                    health = 0;
                    break;
            }
        }

        public override TankType GetTankType() => TankType.Bomber;
    }
}

