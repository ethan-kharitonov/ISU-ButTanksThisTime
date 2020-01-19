using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime.Collectibles
{
    internal class SpeedBoostItem : Item
    {
        public static readonly Item VoidObject = new SpeedBoostItem(default);

        public static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        static SpeedBoostItem() => img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Speed_Bonus");

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