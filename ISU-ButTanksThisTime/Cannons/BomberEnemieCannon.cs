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

        public BomberEnemieCannon(float disFromCentreBase, float scaleFactor, Owner owner) : base(FIRE_RATE, DAMEGE, ACTIVE, disFromCentreBase, scaleFactor, owner)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Inactive/I1");
        }

    }
}
