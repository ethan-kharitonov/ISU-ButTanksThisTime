using Animation2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class TierFourCannon : Cannon
    {
        private static readonly int[] fireRate = { 0, 75};
        private static readonly int[] rotationSpeed = { 100, 1000};


        private readonly Bullet bullet;
        private readonly Texture2D img;
        public TierFourCannon(Owner owner, Stage stage, Vector2 position, float rotation) : base(fireRate[(int)owner], rotationSpeed[(int)owner], true, position, rotation)
        {
            img = Tools.Content.Load<Texture2D>("Images/Sprites/Cannons/TierFour/T4P" + ((int)stage + 1));

            bullet = new Plasma(Vector2.Zero, 0, owner);
        }

        protected override Bullet Bullet => bullet;

        protected override Texture2D Img => img;
    }
}
