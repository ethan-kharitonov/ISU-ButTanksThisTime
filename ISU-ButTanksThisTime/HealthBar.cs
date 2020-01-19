using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    internal class HealthBar
    {
        private readonly Texture2D barImg;
        private Rectangle barBox;
        private const float IMG_SCALE_FACTOR = 0.5f;
        private const int DIS_ABOVE_TANK = 40;
        private Texture2D healthImg;
        private const int BAR_LENGTH = 85;
        private Rectangle healthBox;

        public readonly float MAX_HEALTH;

        public HealthBar(int startHealth, Vector2 position)
        {
            MAX_HEALTH = startHealth;
            barImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Loading Bar BG");
            barBox = new Rectangle((int) position.X, (int) position.Y, BAR_LENGTH, 8);

            healthImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/Loading Bar");
            healthBox = barBox;
        }

        public void Update(Vector2 tankPos, float health)
        {
            barBox.X = (int) tankPos.X;
            barBox.Y = (int) tankPos.Y - DIS_ABOVE_TANK;

            healthBox.X = barBox.X - barBox.Width / 2;
            healthBox.Y = barBox.Y;

            //healthBox = barBox;
            healthBox.Width = (int) (health / MAX_HEALTH * BAR_LENGTH) - 2;
            healthBox.Height = barBox.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barImg, barBox, null, Color.White, 0, new Vector2(barImg.Width * 0.5f, barImg.Height * 0.5f), SpriteEffects.None, 1f);
            spriteBatch.Draw(healthImg, healthBox, null, Color.White, 0, new Vector2(0, healthImg.Height * 0.5f), SpriteEffects.None, 1f);


            //spriteBatch.Draw(healthImg, healthBox, Color.White);
        }
    }
}