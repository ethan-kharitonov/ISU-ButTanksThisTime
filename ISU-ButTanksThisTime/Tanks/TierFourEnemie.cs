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
    class TierFourEnemie : Tank
    {
        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;

        private readonly Texture2D[] stages = new Texture2D[3];

        public TierFourEnemie(Vector2 position, float rotation, Stage stage) : base(position, stage, rotation)
        {
            for (int i = 0; i < stages.Length - 1; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierFour/T4P" + (i + 1));
            }

            baseImg = stages[(int)stage];
            cannon = new TierFourCannon(CANNON_DIS_FROM_CENTRE, Owner.Enemie, Stage.Low);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
        }

    }
}
