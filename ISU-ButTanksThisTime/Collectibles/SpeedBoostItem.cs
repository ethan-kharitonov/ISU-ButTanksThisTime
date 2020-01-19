using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class SpeedBoostItem : Item
    {
        public static readonly Item VoidObject = new SpeedBoostItem(default);

        public static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        static SpeedBoostItem()
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Speed_Bonus");
        }

        public SpeedBoostItem(Vector2 position) : base(position)
        {
        }

        public override Texture2D Img => img;

        public override ItemType ItemType => ItemType.SpeedBoost;

        public override void Use()
        {
            GameScene.SpeedUpPlayer();
        }
    }
}
