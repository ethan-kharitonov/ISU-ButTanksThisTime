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
    class TierOneEnemie : Tank
    {
        private const float VIEW_RANGE = 450;
        private const float ATTACK_RANGE = 250;
        private readonly List<Vector2> path = new List<Vector2>();
        private int targetPoint = 1;

        private const float IMG_SCALE_FACTOR = 0.25f;
        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;

        private readonly Stage stage;
        private readonly Texture2D[] stages = new Texture2D[4];

        public TierOneEnemie(Vector2 position, List<Vector2> path, Stage stage, float rotation) : base(position, IMG_SCALE_FACTOR, rotation)
        {
            for (int i = 0; i < stages.Length - 1; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1P" + (i + 1));
            }
            stages[stages.Length - 1] = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1PP");

            this.stage = stage;
            this.path = path;

            baseImg = stages[(int)stage];
            cannon = new TierOneCannon(CANNON_DIS_FROM_CENTRE, IMG_SCALE_FACTOR, Owner.Enemie, stage);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);
        }

        public override bool Update(Vector2 playerPos, float targetDistance = 0)
        {
            if ((playerPos - basePosition).LengthSquared() <= Math.Pow(VIEW_RANGE, 2))
            {
                cannon.active = true;
                return base.Update(playerPos, ATTACK_RANGE);
            }
            else
            {
                cannon.active = false;
                float distanceSquared = (path[targetPoint] - basePosition).LengthSquared();
                if (distanceSquared < Math.Pow(speed, 2))
                {
                    basePosition = path[targetPoint];
                    targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
                }
                return base.Update(path[targetPoint]);
            }
        }

        public override Stage GetStage() => stage;
    }
}
