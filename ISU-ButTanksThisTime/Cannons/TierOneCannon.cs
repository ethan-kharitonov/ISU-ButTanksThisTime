using Microsoft.Xna.Framework;
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
        private static readonly int[] fireRate = {1000, 800, 700, 500};
        private static readonly int[] rotationSpeed = { 3, 4, 5, 3};
        private const bool ACTIVE = false;

        private readonly Bullet bullet;
        private readonly Texture2D img;

        public TierOneCannon( Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int)stage], rotationSpeed[(int)stage], ACTIVE, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierOne/T1P" + ((int)stage + 1));
            bullet = new MeduimBullet(Vector2.Zero, 0, Tank.IMG_SCALE_FACTOR, owner);
        }


        protected override Bullet Bullet => bullet;

        public override Texture2D Img => img;
    }
}
