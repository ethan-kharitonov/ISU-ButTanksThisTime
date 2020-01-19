// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    /// <summary>
    /// Class TierFourCannon.
    /// Implements the <see cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    /// </summary>
    /// <seealso cref="ISU_ButTanksThisTime.Cannons.Cannon" />
    internal class TierFourCannon : Cannon
    {
        private static readonly int[] fireRate = {0, 75};
        private static readonly int[] rotationSpeed = {100, 1000};
        private static readonly int[] timeBtwRotations = {1000, 150};

        private readonly Timer rotateTimer;
        private const int ROTATION_INC = 45;
        private int additionalRotation;

        public static readonly CannonInfo Info;

        /// <summary>
        /// Initializes static members of the <see cref="TierFourCannon"/> class.
        /// </summary>
        static TierFourCannon()
        {
            var cannonImg = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P4");
            Info = new CannonInfo(75, 1000, cannonImg, Plasma.Info, new TierFourCannon(Owner.Player, Stage.Player, default, 0));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TierFourCannon"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="stage">The stage.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public TierFourCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int) owner], rotationSpeed[(int) owner], true, position, rotation)
        {
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P" + ((int) stage + 1));

            rotateTimer = new Timer(timeBtwRotations[(int) owner]);
            Bullet = new Plasma(Vector2.Zero, 0, owner);
            Active = false;
            rotateTimer.Reset();
        }

        /// <summary>
        /// Updates the specified base position.
        /// </summary>
        /// <param name="basePos">The base position.</param>
        /// <param name="baseRotation">The base rotation.</param>
        /// <param name="na">The na.</param>
        public override void Update(Vector2 basePos, float baseRotation, Vector2 na)
        {
            Pos = CalcPos(basePos, baseRotation);

            if (rotateTimer.IsTimeUp(Tools.GameTime))
            {
                additionalRotation += ROTATION_INC;
                Rotation = additionalRotation;
                var newBullet = Bullet.Clone(Pos, Rotation);
                GameScene.AddBullet(newBullet);

                rotateTimer.Reset();
            }
        }

        /// <summary>
        /// Gets the bullet.
        /// </summary>
        /// <value>The bullet.</value>
        protected override Bullet Bullet { get; }

        /// <summary>
        /// Gets the img.
        /// </summary>
        /// <value>The img.</value>
        protected override Texture2D Img { get; }
    }
}