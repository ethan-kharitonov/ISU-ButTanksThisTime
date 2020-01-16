using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class TierOneCannon : Cannon
    {
        private const int FIRE_RATE = 1000;
        private const int DAMEGE = 25;
        private const bool ACTIVE = false;
        private Texture2D[] stages = new Texture2D[4];

        public TierOneCannon(float disFromCentreBase, float scaleFactor, Owner owner, Stage stage) : base(FIRE_RATE, DAMEGE, ACTIVE, disFromCentreBase, scaleFactor, owner)
        {
            for(int i = 0; i < stages.Length - 1; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierOne/T1P" + (i + 1));
            }
            stages[stages.Length - 1] = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierOne/T1PP");

            img = stages[(int)stage];
        }
    }
}
