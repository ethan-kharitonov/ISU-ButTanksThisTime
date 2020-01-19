using ISU_ButTanksThisTime.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Cannons
{
    internal class HealerCannon : Cannon
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private static readonly int[] ROTATION_SPEED = {5, 6, 7};


        public HealerCannon(Stage stage, Vector2 position, float rotation) : base(0, ROTATION_SPEED[(int) stage], false, position, rotation) => 
            Img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/Healer/H" + ((int) stage + 1));

        protected override Bullet Bullet => null;

        protected override Texture2D Img { get; }
    }
}