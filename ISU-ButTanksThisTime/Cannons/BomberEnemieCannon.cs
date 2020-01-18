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

        private readonly Texture2D img;

        public BomberEnemieCannon(Stage stage, Vector2 position, float rotation) : base(FIRE_RATE, Tank.ROTATION_SPEED, ACTIVE, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Inactive/I" + ((int)stage + 1));
        }

        protected override Bullet Bullet => null;
        protected override Texture2D Img => img;
    }
}
