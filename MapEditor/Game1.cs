using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;

namespace MapEditor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Path Maker Variables
        private List<Vector2> enemyCheckPoints = new List<Vector2>();
        private Texture2D redSquare;
        private Texture2D line;
        private Texture2D mouseImg;
        private Vector2 mousePos = Vector2.Zero;
        private int moveSpeed = 1;

        private Texture2D backgroundImg;

        private const int ARENA_WIDTH = 10;
        private const int ARENA_HEIGHT = 6;

        private Rectangle screen;

        //Saving Variables
        private StreamReader inFile;
        private StreamWriter outFile;
        private string filePath;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1050;
            graphics.PreferredBackBufferHeight = 576;
            IsMouseVisible = false;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen = GraphicsDevice.Viewport.Bounds;

            Camera.LoadCamera(GraphicsDevice.Viewport, screen);


            backgroundImg = Content.Load<Texture2D>("Backgrounds/bg");
            mouseImg = Content.Load<Texture2D>("Crosshairs/crosshair001");

            redSquare = new Texture2D(graphics.GraphicsDevice, 2, 2);

            Color[] data = new Color[2 * 2];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Red;
            }

            redSquare.SetData(data);


            line = new Texture2D(graphics.GraphicsDevice, 2, 2);
            Color[] data1 = new Color[2 * 2];
            for (int i = 0; i < data.Length; ++i)
            {
                data1[i] = Color.LightBlue;
            }

            line.SetData(data1);

            filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filePath = Path.GetDirectoryName(filePath);
            filePath = filePath.Substring(6);
            filePath = filePath + "../../../../ISU-ButTanksThisTime/bin/Debug/saves/MyFile.txt";
            LoadPath();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                SavePath();
                Exit();
            }

            //mousePos = enemyCheckPoints.Count > 0 ? enemyCheckPoints[enemyCheckPoints.Count - 1] : mousePos;
            
            Camera.Update();

            /*if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                --mouse
            }*/

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                mousePos.X -= moveSpeed / Camera.GetZoom();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                mousePos.X += moveSpeed / Camera.GetZoom();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                mousePos.Y -= moveSpeed / Camera.GetZoom();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                mousePos.Y += moveSpeed / Camera.GetZoom();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if(!(enemyCheckPoints.Count > 0 && mousePos == enemyCheckPoints[enemyCheckPoints.Count - 1]))
                {
                    enemyCheckPoints.Add(mousePos);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Delete) && enemyCheckPoints.Count > 0)
            {
                enemyCheckPoints.RemoveAt(enemyCheckPoints.Count - 1);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.transforme);

            for (int r = -ARENA_WIDTH / 2; r < ARENA_WIDTH / 2; ++r)
            {
                for (int c = -ARENA_HEIGHT / 2; c < ARENA_HEIGHT / 2; ++c)
                {
                    Rectangle bgBpx = new Rectangle(r * backgroundImg.Width + screen.Center.X, c * backgroundImg.Height + screen.Center.Y, backgroundImg.Width, backgroundImg.Height);
                    spriteBatch.Draw(backgroundImg, bgBpx, null, Color.White, 0, new Vector2(backgroundImg.Width / 2, backgroundImg.Height / 2), SpriteEffects.None, 1f);
                }
            }

            for (int i = 0; i < enemyCheckPoints.Count; ++i)
            {
                Vector2 distance;
                if(i + 1 == enemyCheckPoints.Count)
                {
                    distance = mousePos - enemyCheckPoints[i];
                }
                else
                {
                    distance = enemyCheckPoints[i + 1] - enemyCheckPoints[i];
                }
                var length = Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2);
                length = Math.Sqrt(length);
                float angle = (float)Math.Atan2(distance.Y, distance.X);
                Rectangle box = new Rectangle((int)enemyCheckPoints[i].X, (int)enemyCheckPoints[i].Y, (int)length, 3);
                spriteBatch.Draw(line, box, null, Color.White, angle, new Vector2(0, line.Height/2), SpriteEffects.None, 1f);
            }

            foreach (Vector2 point in enemyCheckPoints)
            {
                spriteBatch.Draw(redSquare, point, null, Color.White, 0, new Vector2(redSquare.Width / 2, redSquare.Height / 2), 1 / Camera.GetZoom(), SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(mouseImg, mousePos, null, Color.White, 0, new Vector2(mouseImg.Width / 2, mouseImg.Height / 2), 1, SpriteEffects.None, 1f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void SavePath()
        {
            outFile = File.CreateText(filePath);

            foreach(Vector2 point in enemyCheckPoints)
            {
                outFile.WriteLine(point.X + "," + point.Y);
            }

            outFile.Close();
        }

        private void LoadPath()
        {
            inFile = File.OpenText(filePath);
            string[] data;

            enemyCheckPoints.Clear();
            while (!inFile.EndOfStream)
            {
                data = inFile.ReadLine().Split(',');
                enemyCheckPoints.Add(new Vector2((float)Convert.ToDouble(data[0]), (float)Convert.ToDouble(data[1])));
            }
            string[] points = inFile.ToString().Split(',', '\n');


            inFile.Close();
        }
    }

}
