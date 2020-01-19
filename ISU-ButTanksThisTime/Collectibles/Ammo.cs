using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class Ammo : Item
    {
        public static readonly Item VoidObject = new Ammo(default);

        private static readonly Texture2D img;

        // Guarantees that the static state is initialized right before the class is used. Without it, the static state
        // could be initialized too early.
        static Ammo()
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Items/Shotgun_Shells");
        }

        public Ammo(Vector2 position) : base(position, 15)
        {
        }

        public override Texture2D Img => img;
        public override ItemType ItemType => ItemType.Ammo;
        public override bool Usable => false;
        public override void Use()
        {
        }
    }
}
