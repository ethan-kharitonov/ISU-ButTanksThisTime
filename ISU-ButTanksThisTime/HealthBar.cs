using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class HealthBar
    {
        private readonly Texture2D barImg;
        private const float IMG_SCALE_FACTOR = 0.07f;
        private const int DIS_ABOVE_TANK = 40;
        private Texture2D healthImg;
        private const int BAR_LENGTH = 78;
        private Rectangle healthBox;

        public readonly int MAX_HEALTH;

        private Vector2 position;
        public HealthBar(int startHealth)
        {
            MAX_HEALTH = startHealth;
            barImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/HealthBar");

            healthImg = new Texture2D(Tools.graphics, (int)(barImg.Width * IMG_SCALE_FACTOR), (int)(barImg.Height * IMG_SCALE_FACTOR));

            Color[] data = new Color[(int)(barImg.Width * IMG_SCALE_FACTOR) * (int)(barImg.Height * IMG_SCALE_FACTOR)];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Green;
            }

            healthImg.SetData(data);

        }

        public void Update(Vector2 tankPos, int health)
        {
            position = tankPos - new Vector2(0, DIS_ABOVE_TANK);

            int healthPercent = (MAX_HEALTH / 100) * health;
            healthBox = new Rectangle((int)(position.X - barImg.Width * 0.5f * IMG_SCALE_FACTOR + 3), (int)(position.Y - barImg.Height * 0.5 * IMG_SCALE_FACTOR + 3), (int)(BAR_LENGTH/100.0 * healthPercent), (int)(barImg.Height * IMG_SCALE_FACTOR) - 4);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barImg, position, null, Color.White, 0, new Vector2(barImg.Width * 0.5f, barImg.Height * 0.5f), IMG_SCALE_FACTOR, SpriteEffects.None, 1f);
            spriteBatch.Draw(healthImg, healthBox, Color.White);
        }
    }
}
