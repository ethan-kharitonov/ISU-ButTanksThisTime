﻿using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class MeduimBullet : Bullet
    {
        private readonly float scaleFactor;

        private readonly Animation exAnim;

        public MeduimBullet(Vector2 position, float rotation, float scaleFactor, Owner bulletOwner) : base(position, rotation, scaleFactor, bulletOwner)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
            this.scaleFactor = scaleFactor;

            Texture2D exSprite = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/BulletExplode");
            exAnim = new Animation(exSprite, 3, 1, 3, 0, 0, Animation.ANIMATE_ONCE, 2, Vector2.Zero, scaleFactor, true);
        }

        protected override Animation ExAnim => exAnim;

        public override Bullet Clone(Vector2 pos, float rotation)
        {
            return new MeduimBullet(pos, rotation, scaleFactor, bulletOwner);
        }
    }
}
