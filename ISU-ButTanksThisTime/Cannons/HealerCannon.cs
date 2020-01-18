using Microsoft.Xna.Framework;
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
        private readonly Texture2D img;
        private static readonly int[] ROTATION_SPEED = { 5, 6, 7 };


        public HealerCannon(Stage stage, Vector2 position, float rotation) : base(0, ROTATION_SPEED[(int)stage], false, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Healer/H" + ((int)stage + 1));

        }

        protected override Bullet Bullet => null;

        public override Texture2D Img => img;
    }
}
