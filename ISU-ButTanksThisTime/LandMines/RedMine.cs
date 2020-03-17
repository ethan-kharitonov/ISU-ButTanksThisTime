// Author        : Ethan Kharitonov
// File Name     : LandMines\RedMine.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the Red Mine type.
using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.LandMines
{
    /// <summary>
    /// Implements the Red Mine type.
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
        private const int RADIUS =  18;
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
           
            //adjast centre of the land mine
            foreach (Animation anim in Animations)
            {
                anim.destRec.X -= anim.destRec.Width / 2;
                anim.destRec.Y -= anim.destRec.Height / 2;
            }
        }
    }
}