using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    class BurstCannon : Cannon
    {
        private static readonly int[] fireRate = { 150, 100, 80, 60 };
        private static readonly int[] rotationSpeed = { 4, 1000};
        private static readonly int[] burstRate = { 2000, 2500, 1500, 1000, 1000};
        private static readonly int[] burstLength = { 300, 500, 700, 800, 1000 };

        private readonly Timer timeBtwnBursts;
        private readonly Timer burstDuration;

        private readonly Bullet bullet;
        private readonly Texture2D img;

        bool inBurst = false;

        public static readonly CannonInfo Info;

        static BurstCannon()
        {
            Texture2D cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierThree/T3P4");
            Info = new CannonInfo(60, 1000, cannonImg, Laser.Info, new BurstCannon(Owner.Player, Stage.Player, default, 0),  1000, 1000);
        }


        public BurstCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int)stage], rotationSpeed[(int)owner], true, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierThree/T3P" + ((int)stage + 1));

            bullet = new Laser(Vector2.Zero, 0, Tank.IMG_SCALE_FACTOR, owner);
            
            timeBtwnBursts = new Timer(burstRate[(int)stage]);
            burstDuration = new Timer(burstLength[(int)stage]);


        }

        public override void Update(Vector2 basePos, float baseRotation, Vector2 target)
        {
            //start timer for shooting period
            if (active && !inBurst)
            {
                burstDuration.Reset();
                inBurst = true;
            }

            if (!burstDuration.IsTimeUp(Tools.GameTime))
            {
                active = true;
            }

            //if shooting is done but still inBurst start timer for reloading
            if (inBurst && burstDuration.IsTimeUp(Tools.GameTime))
            {
                active = false;
                if (timeBtwnBursts.IsTimeUp(Tools.GameTime))
                {
                    inBurst = false;
                    timeBtwnBursts.Reset();
                }
            }

            base.Update(basePos, baseRotation, target);
        }

        protected override Bullet Bullet => bullet;

        public override Texture2D Img => img;

    }
}
