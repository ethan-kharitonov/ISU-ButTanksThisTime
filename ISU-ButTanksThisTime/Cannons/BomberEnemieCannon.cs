using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class BomberEnemieCannon : Cannon
    {
        private const int FIRE_RATE = 1000;
        private const int DAMEGE = 25;
        private const bool ACTIVE = false;

        private readonly Texture2D[] stages = new Texture2D[3];


        public BomberEnemieCannon(float disFromCentreBase, Stage stage) : base(FIRE_RATE, DAMEGE, ACTIVE, disFromCentreBase)
        {
            for (int i = 0; i < stages.Length; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Inactive/I" + (i + 1));
            }

            img = stages[(int)stage];
        }

    }
}
