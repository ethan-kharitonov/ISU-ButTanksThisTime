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
    class RedMine : LandMine
    {
        private const float IMG_SCALE_FACTOR = 0.4f;
        public static readonly int Width = 103;
        public static readonly int Height = 103;


        public RedMine(Vector2 position) : base(10, 300, 100)
        {
            Texture2D idleSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb1Idle");
            Texture2D triggeredSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb1Triggerd");
            Texture2D explodeSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb1Explode");

            explosionEffect = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/Light_01");

            animations[0] = new Animation(idleSprite, 10, 1, 10, 1, 1, Animation.ANIMATE_FOREVER, 3, position, IMG_SCALE_FACTOR, true);
            animations[1] = new Animation(triggeredSprite, 2, 2, 4, 1, 1, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
            animations[2] = new Animation(explodeSprite, 3, 3, 9, 1, 1, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
            
        }
    }
}
