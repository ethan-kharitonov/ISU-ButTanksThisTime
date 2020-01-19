using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal class MineDropperCannon : Cannon
    {
        private static readonly int[] fireRate = {1000, 750, 500, 90};
        private static readonly int[] rotationSpeed = {3, 4, 5, 5};
        private const bool ACTIVE = false;

        public static readonly CannonInfo Info;

        static MineDropperCannon()
        {
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/MineDropper/M4");
            Info = new CannonInfo(500, 5, cannonImg, FireBullet.Info, new MineDropperCannon(Owner.Player, Stage.Player, default, 0));
        }

        public MineDropperCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) stage], rotationSpeed[(int) stage], ACTIVE, position, rotation)
        {
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/MineDropper/M" + ((int) stage + 1));
            Bullet = new FireBullet(Vector2.Zero, 0, owner);
        }

        protected override Bullet Bullet { get; }

        protected override Texture2D Img { get; }
    }
}