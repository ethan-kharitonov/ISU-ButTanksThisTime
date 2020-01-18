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

        public TierTwoEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, 0, rotation)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierTwo/T2P" + ((int)stage + 1));

            cannon = new TierOneCannon(Owner.Enemie, stage, basePosition, baseRotation);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

            target = SetTarget();
        }

        public override bool Update(Vector2 NA)
        {
            if(basePosition == target)
            {
                GameScene.AddLandMine(new RedMine(basePosition - Dimensions/2f));
                target = SetTarget();
            }
            return base.Update(target);
        }

        private Vector2 SetTarget()
        {
            return new Vector2(Tools.rnd.Next(Tools.ArenaBounds.Left, Tools.ArenaBounds.Right), Tools.rnd.Next(Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom));
        }

        public override TankType GetTankType() => TankType.MineDroper;

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new TierTwoEnemie(position, rotation, stage);
        }
    }
}
