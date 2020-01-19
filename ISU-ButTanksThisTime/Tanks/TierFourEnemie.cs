using System;
using System.Collections.Generic;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    internal class TierFourEnemie : Tank
    {
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 200, 300};

        private readonly List<Vector2> path;
        private int targetPoint;

        public TierFourEnemie(Vector2 position, float rotation, Stage stage, List<Vector2> path) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierFour/T4P" + ((int) stage + 1));

            Cannon = new TierFourCannon(Owner.Enemie, stage, BasePosition, BaseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            this.path = path;
        }

        public override bool Update(Vector2 na)
        {
            var distanceSquared = (path[targetPoint] - BasePosition).LengthSquared();
            if (distanceSquared < Math.Pow(Speed, 2))
            {
                BasePosition = path[targetPoint];
                targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
            }

            return base.Update(path[targetPoint]);
        }

        public override TankType GetTankType() => TankType.RotateShooter;

        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierFourEnemie(position, rotation, stage, path);
    }
}