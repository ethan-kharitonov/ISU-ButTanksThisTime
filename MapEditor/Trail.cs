using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MapEditor
{
    internal class Trail
    {
        private readonly Texture2D dot;
        private readonly Texture2D line;

        private Vector2 mousePos = Vector2.Zero;
        private const int MOUSE_SPEED = 2;

        public Trail(Color dots, Color lines, GraphicsDevice graphics)
        {
            dot = new Texture2D(graphics, 2, 2);

            var data = new Color[2 * 2];
            for (var i = 0; i < data.Length; ++i)
            {
                data[i] = dots;
            }

            dot.SetData(data);


            line = new Texture2D(graphics, 2, 2);
            var data1 = new Color[2 * 2];
            for (var i = 0; i < data.Length; ++i)
            {
                data1[i] = lines;
            }

            line.SetData(data1);
        }

        public Vector2 Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                mousePos.X -= MOUSE_SPEED / Camera.GetZoom();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                mousePos.X += MOUSE_SPEED / Camera.GetZoom();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                mousePos.Y -= MOUSE_SPEED / Camera.GetZoom();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                mousePos.Y += MOUSE_SPEED / Camera.GetZoom();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (!(Points.Count > 0 && mousePos == Points[Points.Count - 1]))
                {
                    Points.Add(mousePos);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Delete) && Points.Count > 0)
            {
                Points.RemoveAt(Points.Count - 1);
            }

            return mousePos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Points.Count; ++i)
            {
                Vector2 distance;
                if (i + 1 == Points.Count)
                {
                    distance = mousePos - Points[i];
                }
                else
                {
                    distance = Points[i + 1] - Points[i];
                }

                var length = Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2);
                length = Math.Sqrt(length);
                var angle = (float) Math.Atan2(distance.Y, distance.X);
                var box = new Rectangle((int) Points[i].X, (int) Points[i].Y, (int) length, 3);
                spriteBatch.Draw(line, box, null, Color.White, angle, new Vector2(0, line.Height / 2F), SpriteEffects.None, 1f);
            }

            foreach (var point in Points)
            {
                spriteBatch.Draw(dot, point, null, Color.White, 0, new Vector2(dot.Width / 2F, dot.Height / 2F), 1 / Camera.GetZoom(), SpriteEffects.None, 1f);
            }
        }

        public List<Vector2> Points { get; set; } = new List<Vector2>();
    }
}