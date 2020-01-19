using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ISU_ButTanksThisTime
{
    class TierOneEnemie : Tank
    {
        private static readonly int[] VIEW_RANGE_OPTIONS = {300, 450, 650 };
        private static readonly int[] ATTACK_RANGE_OPTIONS = { 250, 400 , 550};
        private static readonly int[] SPEED = { 4, 5, 6 };
        private static readonly int[] ROTATION_SPEED = { 5, 6, 7 };
        private static readonly int[] HEALTH = { 150, 200, 300 };

        private readonly int playerAttackRange;
        private readonly int viewRange;


        private readonly List<Vector2> path = new List<Vector2>();
        private int targetPoint = 1;

        public TierOneEnemie(Vector2 position, List<Vector2> path, Stage stage, float rotation) : base(position, stage, VIEW_RANGE_OPTIONS[(int)stage], SPEED[(int)stage], ROTATION_SPEED[(int)stage], HEALTH[(int)stage], rotation)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1P" + ((int)stage + 1));

            cannon = new TierOneCannon(Owner.Enemie, stage, basePosition, baseRotation);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
         
            this.path = path;

            playerAttackRange = ATTACK_RANGE_OPTIONS[(int)stage];
            viewRange = VIEW_RANGE_OPTIONS[(int)stage];

        }

        public override bool Update(Vector2 playerPos)
        {
            if ((playerPos - basePosition).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                cannon.active = true;
                attackRange = playerAttackRange;
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
