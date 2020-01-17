using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class HealerCannon : Cannon
    {
        private readonly Texture2D[] stages = new Texture2D[3];

        public HealerCannon(float disFromCentreBase, Stage stage) : base(0, 0, false, disFromCentreBase)
        {
            for (int i = 0; i < stages.Length - 1; ++i)
            {
                stages[i] = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Healer/H" + (i + 1));
            }

            img = stages[(int)stage];
        }
    }
}
