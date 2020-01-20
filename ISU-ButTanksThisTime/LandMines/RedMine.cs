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
    /// The Red Mine type.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.LandMines.LandMine" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.LandMines.LandMine" />
    internal class RedMine : LandMine
    {
        // The properties of this kind of land mines.
        private const float IMG_SCALE_FACTOR = 0.4f;

        //store the raduis of the land mine and the raduis of the area effected by its explosion
        private const int RADIUS = 37;
        private const int EX_RADIUS = 300;

        //the index for each animation
        private const int IDLE_ANIM_INDEX = 0;
        private const int TRIGGERED_ANIM_INDEX = 1;
        private const int EXPLODE_ANIM_INDEX = 2;


        /// <summary>
        /// Initializes a new instance of the <see cref="RedMine"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public RedMine(Vector2 position) : base(RADIUS, EX_RADIUS)
        {
            //Implements the animations of the land mine
            var idleSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineIdle");
            var triggeredSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineTRigered");
            var explodeSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/RedMineExplode");

            Animations[IDLE_ANIM_INDEX] = new Animation(idleSprite, 10, 1, 10, 0, 0, Animation.ANIMATE_FOREVER, 3, position, IMG_SCALE_FACTOR, true);
            Animations[TRIGGERED_ANIM_INDEX] = new Animation(triggeredSprite, 4, 1, 4, 0, 0, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
            Animations[EXPLODE_ANIM_INDEX] = new Animation(explodeSprite, 9, 1, 9, 0, 0, Animation.ANIMATE_ONCE, 3, position, IMG_SCALE_FACTOR, true);
        }
    }
}