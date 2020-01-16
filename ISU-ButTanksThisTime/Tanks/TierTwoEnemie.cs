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
    class TierTwoEnemie : Tank
    {
        private const float IMG_SCALE_FACTOR = 0.25f;
        private const float CANNON_DIS_FROM_CENTRE = 35 * IMG_SCALE_FACTOR;

        private readonly Stage stage;
        private readonly Texture2D[] stages = new Texture2D[4];

        private Vector2 target;

        public TierTwoEnemie(Vector2 position) : base(position, IMG_SCALE_FACTOR)
        {
            for (int i = 0; i < stages.Length - 1; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierTwo/T2P" + (i + 1));
            }
            stages[stages.Length - 1] = Tools.Content.Load<Texture2D>("Images/Sprites/Tanks/TierOne/T1PP");

            baseImg = stages[(int)stage];
            cannon = new TierOneCannon(CANNON_DIS_FROM_CENTRE, IMG_SCALE_FACTOR, Owner.Enemie, stage);

            Texture2D explosionSpritesheet = Tools.Content.Load<Texture2D>("Images/Sprites/Effects/spritesheet");
            explosionAnimation = new Animation(explosionSpritesheet, 3, 3, 9, 1, 1, 1, 2, basePosition, 0.3f, true);

            target = SetTarget();
        }

        public override Stage GetStage()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Vector2 NA, float dis = 0)
        {
            if(basePosition == target)
            {
                target = SetTarget();
            }
            return base.Update(target);
        }

        private Vector2 SetTarget()
        {
            return new Vector2(Tools.rnd.Next(Tools.ArenaBounds.Left, Tools.ArenaBounds.Right), Tools.rnd.Next(Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom));
        }
    }
}
