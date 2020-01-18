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
    class TierTwoEnemie : Tank
    {

        private Vector2 target;

        private static readonly int[] SPEED = { 2, 3, 6 };
        private static readonly int[] ROTATION_SPEED = { 2, 3, 4 };
        private static readonly int[] HEALTH = { 75, 100, 200 };

        private readonly int viewRange = 300;

        public TierTwoEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, SPEED[(int)stage], ROTATION_SPEED[(int)stage], HEALTH[(int)stage], rotation)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierTwo/T2P" + ((int)stage + 1));

            cannon = new MineDroperCannon(Owner.Enemie, stage, basePosition, baseRotation);
            cannon.active = false;

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

            target = SetTarget();
        }

        public override bool Update(Vector2 playerPos)
        {
            if(basePosition == target)
            {
                GameScene.AddLandMine(new RedMine(basePosition - Dimensions/2f));
                target = SetTarget();
            }
            if((basePosition - playerPos).LengthSquared() <= Math.Pow(viewRange, 2))
            {
                cannon.active = true;
                return base.Update(target, playerPos);
            }
            cannon.active = false;
            return base.Update(target);
        }

        private Vector2 SetTarget()
        {
            return new Vector2(Tools.Rnd.Next(Tools.ArenaBounds.Left, Tools.ArenaBounds.Right), Tools.Rnd.Next(Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom));
        }

        public override TankType GetTankType() => TankType.MineDroper;

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new TierTwoEnemie(position, rotation, stage);
        }
    }
}
