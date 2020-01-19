using System;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.LandMines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    internal class TierTwoEnemie : Tank
    {
        private Vector2 target;

        private static readonly int[] SPEED = {2, 3, 6};
        private static readonly int[] ROTATION_SPEED = {2, 3, 4};
        private static readonly int[] HEALTH = {75, 100, 200};

        private readonly int viewRange = 300;

        public TierTwoEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            BaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierTwo/T2P" + ((int) stage + 1));

            Cannon = new MineDroperCannon(Owner.Enemie, stage, BasePosition, BaseRotation);
            Cannon.Active = false;

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            ExplosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, BasePosition, 0.3f, true);

            target = SetTarget();
        }

        public override bool Update(Vector2 playerPos)
        {
            if (BasePosition == target)
            {
                GameScene.AddLandMine(new RedMine(BasePosition - Dimensions / 2f));
                target = SetTarget();
            }

            if ((BasePosition - playerPos).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                Cannon.Active = true;
                return base.Update(target, playerPos);
            }

            Cannon.Active = false;
            return base.Update(target);
        }

        private Vector2 SetTarget() => new Vector2(Tools.Rnd.Next(Tools.ArenaBounds.Left, Tools.ArenaBounds.Right), Tools.Rnd.Next(Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom));

        public override TankType GetTankType() => TankType.MineDroper;

        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new TierTwoEnemie(position, rotation, stage);
    }
}