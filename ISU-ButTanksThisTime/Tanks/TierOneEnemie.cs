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
    class TierOneEnemie : Tank
    {
        private const float VIEW_RANGE = 450;
        private const float ATTACK_RANGE = 250;
        private readonly List<Vector2> path = new List<Vector2>();
        private int targetPoint = 1;

        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;

        public TierOneEnemie(Vector2 position, List<Vector2> path, Stage stage, float rotation) : base(position, stage, ATTACK_RANGE, rotation)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1P" + ((int)stage + 1));

            cannon = new TierOneCannon(Owner.Enemie, stage, basePosition, baseRotation);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
         
            this.path = path;
        }

        public override bool Update(Vector2 playerPos)
        {
            if ((playerPos - basePosition).LengthSquared() <= Math.Pow(VIEW_RANGE, 2))
            {
                cannon.active = true;
                attackRange = ATTACK_RANGE;
                return base.Update(playerPos);
            }
            else
            {
                cannon.active = false;
                attackRange = 0;
                float distanceSquared = (path[targetPoint] - basePosition).LengthSquared();
                if (distanceSquared < Math.Pow(speed, 2))
                {
                    basePosition = path[targetPoint];
                    targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
                }
                return base.Update(path[targetPoint]);
            }
        }

        public override TankType GetTankType() => TankType.BasicPath;

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new TierOneEnemie(position, path, stage, rotation);
        }
    }
}
