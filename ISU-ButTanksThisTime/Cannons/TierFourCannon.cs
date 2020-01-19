using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal class TierFourCannon : Cannon
    {
        private static readonly int[] fireRate = {0, 75};
        private static readonly int[] rotationSpeed = {100, 1000};
        private static readonly int[] timeBtwRotations = {1000, 150};

        private readonly Bullet bullet;
        private readonly Texture2D img;

        private readonly Timer rotateTimer;
        private const int ROTATION_INC = 45;
        private int aditinalRotation;

        public static readonly CannonInfo Info;

        static TierFourCannon()
        {
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P4");
            Info = new CannonInfo(75, 1000, cannonImg, Plasma.Info, new TierFourCannon(Owner.Player, Stage.Player, default, 0));
        }

        public TierFourCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) owner], rotationSpeed[(int) owner], true, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P" + ((int) stage + 1));

            rotateTimer = new Timer(timeBtwRotations[(int) owner]);
            bullet = new Plasma(Vector2.Zero, 0, owner);
            Active = false;
            rotateTimer.Reset();
        }

        public override void Update(Vector2 basePos, float baseRotation, Vector2 na)
        {
            Pos = CalcPos(basePos, baseRotation);

            if (rotateTimer.IsTimeUp(Tools.GameTime))
            {
                aditinalRotation += ROTATION_INC;
                Rotation = aditinalRotation;
                var newBullet = Bullet.Clone(Pos, Rotation);
                GameScene.AddBullet(newBullet);

                rotateTimer.Reset();
            }
        }

        protected override Bullet Bullet => bullet;

        public override Texture2D Img => img;
    }
}