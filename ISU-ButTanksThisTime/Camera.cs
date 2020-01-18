﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class Camera
    {
        public Matrix transforme;
        private Viewport viewport;
        private float zoom = 1;

        public Camera (Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Update(Vector2 target)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                zoom += 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                zoom -= 0.01f;
            }

            target.X = MathHelper.Clamp(target.X, Tools.ArenaBounds.Left + Tools.Screen.Width / 2, Tools.ArenaBounds.Right - Tools.Screen.Width / 2);
            target.Y = MathHelper.Clamp(target.Y, Tools.ArenaBounds.Top + Tools.Screen.Height / 2, Tools.ArenaBounds.Bottom - Tools.Screen.Height / 2);

            transforme = Matrix.CreateTranslation(new Vector3(-target.X, -target.Y, 0)) * Matrix.CreateScale(new Vector3(zoom, zoom, 0)) * Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));

        }

        public float GetZoom() => zoom;
    }
}
