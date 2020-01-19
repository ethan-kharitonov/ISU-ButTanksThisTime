using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MapEditor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Path Maker Variables
        private Texture2D mouseImg;
        private Vector2 mousePos = Vector2.Zero;

        private Texture2D backgroundImg;

        private const int ARENA_WIDTH = 10;
        private const int ARENA_HEIGHT = 6;

        private Rectangle screen;

        //Saving Variables
        private string filePath;

        private readonly IList<Trail> paths = new Trail[3];
        private int curPath = 1;

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

            paths[0] = new Trail(Color.Red, Color.LightBlue, GraphicsDevice);
            paths[1] = new Trail(Color.Green, Color.Aqua, GraphicsDevice);
            paths[2] = new Trail(Color.Black, Color.Purple, GraphicsDevice);


            filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filePath = Path.GetDirectoryName(filePath);
            filePath = filePath.Substring(6);
            filePath = filePath + "../../../../ISU-ButTanksThisTime/saves/MyFile.txt";
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            else
            {
                LoadPath();
            }
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

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                curPath = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                curPath = 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                curPath = 2;
            }


            mousePos = paths[curPath].Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.transforme);

            for (var r = -ARENA_WIDTH / 2; r < ARENA_WIDTH / 2; ++r)
            {
                for (var c = -ARENA_HEIGHT / 2; c < ARENA_HEIGHT / 2; ++c)
                {
                    var bgBpx = new Rectangle(r * backgroundImg.Width + screen.Center.X, c * backgroundImg.Height + screen.Center.Y, backgroundImg.Width, backgroundImg.Height);
                    spriteBatch.Draw(backgroundImg, bgBpx, null, Color.White, 0, new Vector2(backgroundImg.Width / 2, backgroundImg.Height / 2), SpriteEffects.None, 1f);
                }
            }

            foreach (var path in paths)
            {
                path.Draw(spriteBatch);
            }

            spriteBatch.Draw(mouseImg, mousePos, null, Color.White, 0, new Vector2(mouseImg.Width / 2, mouseImg.Height / 2), 1, SpriteEffects.None, 1f);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SavePath()
        {
            var outFile = File.CreateText(filePath);
            foreach (var path in paths)
            {
                foreach (var point in path.GetPoints)
                {
                    outFile.WriteLine(point.X + "," + point.Y);
                }

                outFile.WriteLine("#");
            }

            outFile.Close();
        }

        private void LoadPath()
        {
            var curPath = 0;
            var points = new List<Vector2>();
            using (var inFile = File.OpenText(filePath))
            {
                string[] data;
                while (!inFile.EndOfStream)
                {
                    string line;
                    while (!inFile.EndOfStream && (line = inFile.ReadLine()) != "#")
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        data = line.Split(',');
                        points.Add(new Vector2((float) Convert.ToDouble(data[0]), (float) Convert.ToDouble(data[1])));
                    }

                    paths[curPath].SetPoints(points);
                    points = new List<Vector2>();
                    ++curPath;
                }
            }
        }
    }
}