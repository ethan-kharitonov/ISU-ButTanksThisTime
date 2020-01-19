using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    class BurstEnemie : Tank
    {
        private static readonly int[] ATTACK_RANGE = { 250, 400, 550 };
        private static readonly int[] SPEED = { 3, 4, 6 };
        private static readonly int[] ROTATION_SPEED = { 5, 6, 7 };
        private static readonly int[] HEALTH = { 100, 150, 200 };

        public BurstEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, ATTACK_RANGE[(int)stage], SPEED[(int)stage], ROTATION_SPEED[(int)stage], HEALTH[(int)stage], rotation)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierThree/T3P" + ((int)stage + 1));

            cannon = new BurstCannon(Owner.Enemie, stage, basePosition, baseRotation);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

        }
        public override TankType GetTankType() => TankType.Burst;

        public override bool Update(Vector2 target)
        {
            if((basePosition - target).Length() <= attackRange)
            {
                cannon.active = true;
            }
            return base.Update(target);
        }

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new BurstEnemie(position, rotation, stage);
        }
    }
}
