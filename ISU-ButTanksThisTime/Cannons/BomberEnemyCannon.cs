using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal class BomberEnemyCannon : Cannon
    {
        private const int FIRE_RATE = 0;
        private const bool ACTIVE = false;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};

        private readonly Texture2D img;

        public BomberEnemyCannon(Stage stage, Vector2 position, float rotation) : base(FIRE_RATE, ROTATION_SPEED[(int) stage], ACTIVE, position, rotation) => img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Inactive/I" + ((int) stage + 1));

        protected override Bullet Bullet => null;
        public override Texture2D Img => img;
    }
}