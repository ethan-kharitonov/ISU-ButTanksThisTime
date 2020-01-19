using System;
using System.Collections.Generic;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    internal class TierOneEnemie : Tank
    {
        private static readonly int[] VIEW_RANGE_OPTIONS = {300, 450, 650};
        private static readonly int[] ATTACK_RANGE_OPTIONS = {250, 400, 550};
        private static readonly int[] SPEED = {4, 5, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {150, 200, 300};

        private readonly int playerAttackRange;
        private readonly int viewRange;


        private readonly List<Vector2> path;
        private int targetPoint = 1;

        public TierOneEnemie(Vector2 position, List<Vector2> path, Stage stage, float rotation) : base(position, stage, VIEW_RANGE_OPTIONS[(int) stage], SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1P" + ((int) stage + 1));

            Cannon = new TierOneCannon(Owner.Enemie, stage, BasePosition, BaseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            this.path = path;

            playerAttackRange = ATTACK_RANGE_OPTIONS[(int) stage];
            viewRange = VIEW_RANGE_OPTIONS[(int) stage];
        }

        public override bool Update(Vector2 playerPos)
        {
            if ((playerPos - BasePosition).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                Cannon.Active = true;
                AttackRange = playerAttackRange;
                return base.Update(playerPos);
            }
            else
            {
                Cannon.Active = false;
                AttackRange = 0;
                var distanceSquared = (path[targetPoint] - BasePosition).LengthSquared();
                if (distanceSquared < Math.Pow(Speed, 2))
                {
                    BasePosition = path[targetPoint];
                    targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
                }

                return base.Update(path[targetPoint]);
            }
        }

        public override TankType GetTankType() => TankType.BasicPath;

        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierOneEnemie(position, path, stage, rotation);
    }
}