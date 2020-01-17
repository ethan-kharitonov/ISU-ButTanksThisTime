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
    class HealerEnemy : Tank
    {
        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;

        private readonly Texture2D[] stages = new Texture2D[3];
        private readonly Texture2D healArea;

        private const int HEAL_RADUIS = 200;

        private List<Vector2> path = new List<Vector2>();
        private int targetPoint = 0;
        public HealerEnemy(Vector2 position, float rotation, Stage stage, List<Vector2> path) : base(position, stage, 0, rotation)
        {
            for (int i = 0; i < stages.Length; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/Healer/HP" + (i + 1));
            }

            baseImg = stages[(int)stage];
            cannon = new HealerCannon(CANNON_DIS_FROM_CENTRE, stage);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

            healArea = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/Light_02");

            this.path = path;
        }

        public override bool Update(Vector2 NA)
        {
            float distanceSquared = (path[targetPoint] - basePosition).LengthSquared();
            if (distanceSquared < Math.Pow(speed, 2))
            {
                basePosition = path[targetPoint];
                targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
            }
            return base.Update(path[targetPoint]);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(health > 0)
            {
                Rectangle healBox = new Rectangle((int)basePosition.X, (int)basePosition.Y, 2 * HEAL_RADUIS, 2 * HEAL_RADUIS);
                spriteBatch.Draw(healArea, healBox, null, Color.White, 0, new Vector2(healArea.Width / 2f, healArea.Height / 2f), SpriteEffects.None, 0);
            }
            base.Draw(spriteBatch);
        }

        public override Tank Clone(Vector2 position, float rotation, Stage stage)
        {
            return new HealerEnemy(position, rotation, stage, path);
        }

        public override TankType GetTankType()
        {
            return TankType.Healer;
        }
    }
}
