using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class CoinPileItem : CoinItem
    {
        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        static CoinPileItem()
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Coin_B");
        }
        public CoinPileItem(Vector2 position) : base(position, 25)
        {

        }

        public override Texture2D Img => img;
    }
}
