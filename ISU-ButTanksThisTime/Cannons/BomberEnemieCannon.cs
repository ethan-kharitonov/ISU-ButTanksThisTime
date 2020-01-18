using Microsoft.Xna.Framework;
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
        private const int FIRE_RATE = 0;
        private const bool ACTIVE = false;
        private static readonly int[] ROTATION_SPEED = { 5, 6, 7 };

        private readonly Texture2D img;

        public BomberEnemieCannon(Stage stage, Vector2 position, float rotation) : base(FIRE_RATE, ROTATION_SPEED[(int)stage], ACTIVE, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Inactive/I" + ((int)stage + 1));
        }

        protected override Bullet Bullet => null;
        protected override Texture2D Img => img;
    }
}
