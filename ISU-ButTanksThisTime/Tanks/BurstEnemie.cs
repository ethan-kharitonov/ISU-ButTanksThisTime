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
    class BurstEnemie : Tank
    {
        private const int ATTACK_RANGE = 400;

        public BurstEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, ATTACK_RANGE, rotation)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierThree/T3P" + ((int)stage + 1));

            cannon = new BurstCannon(Owner.Enemie, stage, basePosition, baseRotation);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

        }

        public override TankType GetTankType() => TankType.Burst;

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new BurstEnemie(position, rotation, stage);
        }
    }
}
