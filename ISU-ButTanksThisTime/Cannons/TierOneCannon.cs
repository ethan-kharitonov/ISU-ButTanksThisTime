using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Tanks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal class TierOneCannon : Cannon
    {
        private static readonly int[] fireRate = {1000, 800, 700, 300};
        private static readonly int[] rotationSpeed = {3, 4, 5, 4};
        private const bool ACTIVE = false;

        private readonly Bullet bullet;
        private readonly Texture2D img;

        public static readonly CannonInfo Info;

        static TierOneCannon()
        {
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierOne/T1P4");
            Info = new CannonInfo(500, 3, cannonImg, MeduimBullet.Info, new TierOneCannon(Owner.Player, Stage.Player, default, 0));
        }

        public TierOneCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) stage], rotationSpeed[(int) stage], ACTIVE, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierOne/T1P" + ((int) stage + 1));
            bullet = new MeduimBullet(Vector2.Zero, 0, Tank.IMG_SCALE_FACTOR, owner);
        }


        protected override Bullet Bullet => bullet;

        public override Texture2D Img => img;
    }
}