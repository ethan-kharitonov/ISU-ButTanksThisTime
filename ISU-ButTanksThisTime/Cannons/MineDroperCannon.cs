using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class MineDroperCannon : Cannon
    {
        private static readonly int[] fireRate = { 1500, 1000, 700, 500 };
        private static readonly int[] rotationSpeed = { 3, 4, 5, 5 };
        private const bool ACTIVE = false;

        private readonly Bullet bullet;
        private readonly Texture2D img;

        public MineDroperCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int)stage], rotationSpeed[(int)stage], ACTIVE, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/MineDroper/M" + ((int)stage + 1));
            bullet = new FireBullet(Vector2.Zero, 0, owner);
        }

        protected override Bullet Bullet => bullet;

        protected override Texture2D Img => img;
    }
}
