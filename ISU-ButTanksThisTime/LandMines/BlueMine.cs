// Author        : Ethan Kharitonov
// File Name     : LandMines\BlueMine.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the Blue Mine type.
using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.LandMines
{
    /// <summary>
    /// Implements the Blue Mine type.
    /// <para>
    /// Implements the <see cref="ISU_ButTanksThisTime.LandMines.LandMine" />
    /// </para>
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.LandMines.LandMine" />
    internal class BlueMine : LandMine
    {
        //the factor by which the image is scaled by
        private const float IMG_SCALE_FACTOR = 0.4f;
        private const int RADIUS = 19;
        private const int EX_RADIUS = 250;

        //the index for each animation
        private const int IDLE_ANIM_INDEX = 0;
        private const int TRIGGERED_ANIM_INDEX = 1;
        private const int EXPLODE_ANIM_INDEX = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueMine"/> class.
        /// </summary>
        /// <param name="position">The initial position.</param>
        public BlueMine(Vector2 position) : base(RADIUS, EX_RADIUS)
        {
            //Implements the animations of the land mine
            var idleSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Idle");
            var triggeredSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Triggered");
            var explodeSprite = Tools.Content.Load<Texture2D>("Images/Sprites/LandMines/bomb2Explode");


            Animations[IDLE_ANIM_INDEX] = new Animation(idleSprite, 10, 1, 10, 1, 1, Animation.ANIMATE_FOREVER, 2, position, IMG_SCALE_FACTOR, true);
            Animations[TRIGGERED_ANIM_INDEX] = new Animation(triggeredSprite, 6, 1, 6, 1, 1, Animation.ANIMATE_ONCE, 2, position, IMG_SCALE_FACTOR, true);
            Animations[EXPLODE_ANIM_INDEX] = new Animation(explodeSprite, 9, 1, 9, 1, 1, Animation.ANIMATE_ONCE, 2, position, IMG_SCALE_FACTOR, true);
            
            //adjast centre of the land mine
            foreach(Animation anim in Animations)
            {
                anim.destRec.X -= anim.destRec.Width / 2;
                anim.destRec.Y -= anim.destRec.Height / 2;
            }

        }
    }
}