// Author        : Ethan Kharitonov
// File Name     : Camera.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Implements the camera objects.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Implements the camera objects.
    /// </summary>
    internal class Camera
    {
        public Matrix Transform;
        private Viewport viewport;

        //indicates how zoomed in the camera is
        private const float ZOOM = 1;

        public static float ZOOM1 => ZOOM;

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="viewport">The viewport.</param>
        public Camera(Viewport viewport) => this.viewport = viewport;

        /// <summary>
        /// Updates the camera state.
        /// </summary>
        /// <param name="target">The target to point the Camera to.</param>
        public void Update(Vector2 target)
        {
            //keep the camera on the map
            target.X = MathHelper.Clamp(target.X, Tools.ArenaBounds.Left + Tools.Screen.Width / 2, Tools.ArenaBounds.Right - Tools.Screen.Width / 2);
            target.Y = MathHelper.Clamp(target.Y, Tools.ArenaBounds.Top + Tools.Screen.Height / 2, Tools.ArenaBounds.Bottom - Tools.Screen.Height / 2);

            //update the camera transform
            Transform = Matrix.CreateTranslation(new Vector3(-target.X, -target.Y, 0)) * Matrix.CreateScale(new Vector3(ZOOM, ZOOM, 0)) * Matrix.CreateTranslation(new Vector3(viewport.Width / 2F, viewport.Height / 2F, 0));
        }
    }
}