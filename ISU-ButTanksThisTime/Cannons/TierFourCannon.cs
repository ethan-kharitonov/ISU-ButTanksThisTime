using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class TierFourCannon : Cannon
    {
        private readonly Texture2D[] stages = new Texture2D[3];

        public TierFourCannon(float disFromCentreBase, Owner owner, Stage stage) : base(0, 100, true, disFromCentreBase)
        {
            for (int i = 0; i < stages.Length - 1; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P" + (i + 1));
            }

            bullet = new Plasma(Vector2.Zero, 0, owner);
            img = stages[(int)stage];
        }

    }
}
