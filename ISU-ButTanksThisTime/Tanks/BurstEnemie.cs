using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    internal class BurstEnemie : Tank
    {
        private static readonly int[] ATTACK_RANGE = {250, 400, 550};
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        public BurstEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, ATTACK_RANGE[(int) stage], SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierThree/T3P" + ((int) stage + 1));

            Cannon = new BurstCannon(Owner.Enemie, stage, BasePosition, BaseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);
        }

        public override TankType GetTankType() => TankType.Burst;

        public override bool Update(Vector2 target)
        {
            if ((BasePosition - target).Length() <= AttackRange)
            {
                Cannon.Active = true;
            }

            return base.Update(target);
        }

        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new BurstEnemie(position, rotation, stage);
    }
}