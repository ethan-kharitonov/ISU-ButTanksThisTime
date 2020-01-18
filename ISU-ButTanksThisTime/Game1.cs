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

namespace ISU_ButTanksThisTime
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private bool inShop;

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
            Tools.Screen = GraphicsDevice.Viewport.Bounds;
            Tools.Graphics = GraphicsDevice;
            Tools.Content = Content;
            GameScene.LoadContent();

            Tools.RedSquare = new Texture2D(graphics.GraphicsDevice, 2, 2);

            Color[] data = new Color[2 * 2];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Red;
            }

            Tools.RedSquare.SetData(data);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Tools.GameTime = gameTime;
            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                inShop = true;
            }
            if (inShop)
            {
                
            }
            else
            {
                GameScene.Update();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (inShop)
            {
                Shop.Draw(spriteBatch);
            }
            else
            {
                GameScene.Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }

    }
}



