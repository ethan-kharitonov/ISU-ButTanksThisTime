using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class TierThreeCannon : Cannon
    {
        private Texture2D[] stages = new Texture2D[3];
        public TierThreeCannon(float disFromCentreBase, Owner owner, Stage stage) : base(300, 100, true, disFromCentreBase)
        {
            for (int i = 0; i < stages.Length - 1; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierThree/T3P" + (i + 1));
            }

            img = stages[(int)stage];

            bullet = new Laser(Vector2.Zero, 0, Tank.IMG_SCALE_FACTOR, owner);
        }
    }
}
