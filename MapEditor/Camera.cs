using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapEditor
{
    internal static class Camera
    {
        public static Matrix Transforme;
        private static Viewport viewport;
        private static float zoom = 1;
        private static Vector2 target;

        public static void LoadCamera(Viewport viewport, Rectangle screen)
        {
            Camera.viewport = viewport;
            target = screen.Center.ToVector2();
        }

        public static void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                zoom += 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                zoom -= 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                target.Y -= 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                target.Y += 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                target.X -= 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                target.X += 5;
            }

            //target.X = MathHelper.Clamp(target.X, Tools.ArenaBounds.Left + Tools.screen.Width / 2, Tools.ArenaBounds.Right - Tools.screen.Width / 2);
            //target.Y = MathHelper.Clamp(target.Y, Tools.ArenaBounds.Top + Tools.screen.Height / 2, Tools.ArenaBounds.Bottom - Tools.screen.Height / 2);

            Transforme = Matrix.CreateTranslation(new Vector3(-target.X, -target.Y, 0)) * Matrix.CreateScale(new Vector3(zoom, zoom, 0)) * Matrix.CreateTranslation(new Vector3(viewport.Width / 2F, viewport.Height / 2F, 0));
        }

        public static float GetZoom() => zoom;
    }
}