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
        public static readonly int Raduis = 37;


        public RedMine(Vector2 position) : base(Raduis, 300, 100)
        {
            Texture2D idleSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineIdle");
            Texture2D triggeredSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineTRigered");
            Texture2D explodeSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineExplode");

            animations[0] = new Animation(idleSprite, 10, 1, 10, 0, 0, Animation.ANIMATE_FOREVER, 3, position, IMG_SCALE_FACTOR, true);
            animations[1] = new Animation(triggeredSprite, 4, 1, 4, 0, 0, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
            animations[2] = new Animation(explodeSprite, 9, 1, 9, 0, 0, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
            
        }
    }
}
