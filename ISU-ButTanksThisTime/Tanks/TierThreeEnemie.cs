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
    class TierThreeEnemie : Tank
    {
        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;

        private readonly Texture2D[] stages = new Texture2D[3];

        private const int ATTACK_RANGE = 400;

        private Timer burstTimer = new Timer(1000);


        public TierThreeEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, ATTACK_RANGE, rotation)
        {
            for (int i = 0; i < stages.Length; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierThree/T3P" + (i + 1));
            }

            baseImg = stages[(int)stage];
            cannon = new TierThreeCannon(CANNON_DIS_FROM_CENTRE, Owner.Enemie, stage);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

        }

        public override bool Update(Vector2 target)
        {
            if (burstTimer.IsTimeUp(Tools.gameTime))
            {
                burstTimer.Reset();
                cannon.active = !cannon.active;
            }

            if ((basePosition - target).Length() > ATTACK_RANGE)
            {
                cannon.active = false;
            }
            
            return base.Update(target);
        }

        public override TankType GetTankType() => TankType.Burst;

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new TierThreeEnemie(position, rotation, stage);
        }
    }
}
