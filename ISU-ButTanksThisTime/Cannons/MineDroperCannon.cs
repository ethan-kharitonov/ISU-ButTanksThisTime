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
        private static readonly int[] fireRate = { 1000, 750, 500, 90 };
        private static readonly int[] rotationSpeed = { 3, 4, 5, 5 };
        private const bool ACTIVE = false;

        private readonly Bullet bullet;
        private readonly Texture2D img;

        public static readonly CannonInfo Info;
        static MineDroperCannon()
        {
            Texture2D cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/MineDroper/M4");
            Info = new CannonInfo(500, 5, cannonImg, FireBullet.Info, new MineDroperCannon(Owner.Player, Stage.Player, default, 0));
        }

        public MineDroperCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int)stage], rotationSpeed[(int)stage], ACTIVE, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/MineDroper/M" + ((int)stage + 1));
            bullet = new FireBullet(Vector2.Zero, 0, owner);
        }

        protected override Bullet Bullet => bullet;

        public override Texture2D Img => img;
    }
}
