using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.LandMines
{
    internal class BlueMine : LandMine
    {
        private const float IMG_SCALE_FACTOR = 0.4f;
        private const int RADIUS = 38;
        private const int EX_RADIUS = 250;

        public BlueMine(Vector2 position) : base(RADIUS, EX_RADIUS)
        {
            var idleSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Idle");
            var triggeredSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Triggered");
            var explodeSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Explode");

            Animations[0] = new Animation(idleSprite, 10, 1, 10, 1, 1, Animation.ANIMATE_FOREVER, 2, position, IMG_SCALE_FACTOR, true);
            Animations[1] = new Animation(triggeredSprite, 6, 1, 6, 1, 1, Animation.ANIMATE_ONCE, 2, position, IMG_SCALE_FACTOR, true);
            Animations[2] = new Animation(explodeSprite, 9, 1, 9, 1, 1, Animation.ANIMATE_ONCE, 2, position, IMG_SCALE_FACTOR, true);
        }
    }
}