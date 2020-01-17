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
    class BlueMine : LandMine
    {
        private const float IMG_SCALE_FACTOR = 0.4f;
        public static readonly int Raduis = 38;

        public BlueMine(Vector2 position) : base(Raduis, 100, 0)
        {
            Texture2D idleSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Idle");
            Texture2D triggeredSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Triggered");
            Texture2D explodeSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Explode");

            animations[0] = new Animation(idleSprite, 10, 1, 10, 1, 1, Animation.ANIMATE_FOREVER, 2, position, IMG_SCALE_FACTOR, true);
            animations[1] = new Animation(triggeredSprite, 6, 1, 6, 1, 1, Animation.ANIMATE_ONCE, 2, position, IMG_SCALE_FACTOR, true);
            animations[2] = new Animation(explodeSprite, 9, 1, 9, 1, 1, Animation.ANIMATE_ONCE, 2, position, IMG_SCALE_FACTOR, true);

        }
    }
}
