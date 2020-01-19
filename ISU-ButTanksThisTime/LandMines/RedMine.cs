// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.LandMines
{
    /// <summary>
    /// Class RedMine.
    /// Implements the <see cref="ISU_ButTanksThisTime.LandMines.LandMine" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.LandMines.LandMine" />
    internal class RedMine : LandMine
    {
        private const float IMG_SCALE_FACTOR = 0.4f;
        private const int RADIUS = 37;


        /// <summary>
        /// Initializes a new instance of the <see cref="RedMine"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public RedMine(Vector2 position) : base(RADIUS, 300)
        {
            var idleSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineIdle");
            var triggeredSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineTRigered");
            var explodeSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineExplode");

            Animations[0] = new Animation(idleSprite, 10, 1, 10, 0, 0, Animation.ANIMATE_FOREVER, 3, position, IMG_SCALE_FACTOR, true);
            Animations[1] = new Animation(triggeredSprite, 4, 1, 4, 0, 0, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
            Animations[2] = new Animation(explodeSprite, 9, 1, 9, 0, 0, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
        }
    }
}