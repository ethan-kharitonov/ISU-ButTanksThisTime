using System;
using System.Collections.Generic;
using Animation2D;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Tanks
{
    internal class HealerEnemy : Tank
    {
        private readonly Texture2D healArea;

        private static readonly int[] HEAL_RADUIS_OPTIONS = {250, 350, 500};
        private static readonly int[] HEAL_AMOUNT_OPTIONS = {1, 5, 10};
        private static readonly int[] SPEED = {3, 4, 6};
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};
        private static readonly int[] HEALTH = {100, 150, 200};

        private readonly int healRaduis;
        public readonly int HealAmount;

        private List<Vector2> path = new List<Vector2>();
        private int targetPoint = 0;

        public HealerEnemy(Vector2 position, float rotation, Stage stage, List<Vector2> path) : base(position, stage, 0, SPEED[(int) stage], ROTATION_SPEED[(int) stage], HEALTH[(int) stage], rotation)
        {
            baseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/Healer/HP" + ((int) stage + 1));

            cannon = new HealerCannon(stage, basePosition, baseRotation);

            var explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

            healArea = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/Light_02");

            this.path = path;

            healRaduis = HEAL_RADUIS_OPTIONS[(int) stage];
            HealAmount = HEAL_AMOUNT_OPTIONS[(int) stage];
        }

        public override bool Update(Vector2 NA)
        {
            var distanceSquared = (path[targetPoint] - basePosition).LengthSquared();
            if (distanceSquared < Math.Pow(speed, 2))
            {
                basePosition = path[targetPoint];
                targetPoint = targetPoint == path.Count - 1 ? 0 : targetPoint + 1;
            }

            return base.Update(path[targetPoint]);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (health > 0)
            {
                var healBox = new Rectangle((int) basePosition.X, (int) basePosition.Y, 2 * healRaduis, 2 * healRaduis);
                spriteBatch.Draw(healArea, healBox, null, Color.White, 0, new Vector2(healArea.Width / 2f, healArea.Height / 2f), SpriteEffects.None, 0);
            }

            base.Draw(spriteBatch);
        }

        public override Tank Clone(Vector2 position, float rotation, Stage stage) => new HealerEnemy(position, rotation, stage, path);

        public override TankType GetTankType() => TankType.Healer;

        public Circle HealArea() => new Circle(basePosition, healRaduis);
    }
}