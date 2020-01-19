using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class TierFourCannon : Cannon
    {

        private static readonly int[] fireRate = { 0, 75};
        private static readonly int[] rotationSpeed = { 100, 1000};
        private static readonly int[] timeBTWRotations = { 1000, 150 };

        private readonly Bullet bullet;
        private readonly Texture2D img;

        private Timer rotateTimer;
        private const int ROTATION_INC = 45;
        private int aditinalRotation = 0;

        public static readonly CannonInfo Info;
        static TierFourCannon()
        {
            Texture2D cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P4");
            Info = new CannonInfo(75, 1000, cannonImg, Plasma.Info, new TierFourCannon(Owner.Player, Stage.Player, default, 0));
        }
        public TierFourCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int)owner], rotationSpeed[(int)owner], true, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P" + ((int)stage + 1));

            rotateTimer = new Timer(timeBTWRotations[(int)owner]);
            bullet = new Plasma(Vector2.Zero, 0, owner);
            active = false;
            rotateTimer.Reset();
        }

        public override void Update(Vector2 basePos, float baseRotation, Vector2 NA)
        {
            pos = CalcPos(basePos, baseRotation);

            if (rotateTimer.IsTimeUp(Tools.GameTime))
            {
                aditinalRotation += ROTATION_INC;
                rotation = aditinalRotation;
                Bullet newBullet = Bullet.Clone(pos, this.rotation);
                GameScene.AddBullet(newBullet);

                rotateTimer.Reset();
            }
        }

        protected override Bullet Bullet => bullet;

        public override Texture2D Img => img;
    }
}
