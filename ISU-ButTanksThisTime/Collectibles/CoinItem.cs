using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class CoinItem : Item
    {
        public static readonly Item VoidObject = new CoinItem(default);

        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        static CoinItem()
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Coin_A");
        }

        public CoinItem(Vector2 position, int amount = 10) : base(position, amount)
        {

        }

        public override Texture2D Img => img;

        public override bool Usable => false;

        public override ItemType ItemType => ItemType.Coin;

        public override void Use() => throw new NotSupportedException();
    }
}
