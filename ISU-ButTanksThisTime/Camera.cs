// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class Camera.
    /// </summary>
    internal class Camera
    {
        public Matrix Transform;
        private Viewport viewport;
        private float zoom = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="viewport">The viewport.</param>
        public Camera(Viewport viewport) => this.viewport = viewport;

        /// <summary>
        /// Updates the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
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

            Transform = Matrix.CreateTranslation(new Vector3(-target.X, -target.Y, 0)) * Matrix.CreateScale(new Vector3(zoom, zoom, 0)) * Matrix.CreateTranslation(new Vector3(viewport.Width / 2F, viewport.Height / 2F, 0));
        }
    }
}