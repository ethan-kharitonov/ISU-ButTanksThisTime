using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    class RelocateItem : Item
    {
        public static readonly Item VoidObject = new RelocateItem(default);

        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        static RelocateItem()
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Mobility_Icon");
        }

        public RelocateItem(Vector2 position) : base(position)
        {
        }

        public override Texture2D Img => img;

        public override ItemType ItemType => ItemType.Relocate;

        public override void Use()
        {
            GameScene.FreezeGame();
        }
    }
}
