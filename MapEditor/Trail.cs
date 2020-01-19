using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MapEditor
{
    internal class Trail
    {
        private List<Vector2> points = new List<Vector2>();
        private Texture2D dot;
        private Texture2D line;

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
                if (!(points.Count > 0 && mousePos == points[points.Count - 1]))
                {
                    points.Add(mousePos);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Delete) && points.Count > 0)
            {
                points.RemoveAt(points.Count - 1);
            }

            return mousePos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < points.Count; ++i)
            {
                Vector2 distance;
                if (i + 1 == points.Count)
                {
                    distance = mousePos - points[i];
                }
                else
                {
                    distance = points[i + 1] - points[i];
                }

                var length = Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2);
                length = Math.Sqrt(length);
                var angle = (float) Math.Atan2(distance.Y, distance.X);
                var box = new Rectangle((int) points[i].X, (int) points[i].Y, (int) length, 3);
                spriteBatch.Draw(line, box, null, Color.White, angle, new Vector2(0, line.Height / 2), SpriteEffects.None, 1f);
            }

            foreach (var point in points)
            {
                spriteBatch.Draw(dot, point, null, Color.White, 0, new Vector2(dot.Width / 2, dot.Height / 2), 1 / Camera.GetZoom(), SpriteEffects.None, 1f);
            }
        }

        public List<Vector2> GetPoints => points;
        public void SetPoints(List<Vector2> oldPoints) => points = oldPoints;
    }
}