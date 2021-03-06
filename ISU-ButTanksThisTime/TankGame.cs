// Author        : Ethan Kharitonov
// File Name     : TankGame.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : The main game class.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// The main game class.
    /// <para>
    /// Implements the <see cref="Microsoft.Xna.Framework.Game" />
    /// </para>
    /// </summary>
    /// <seealso cref="Microsoft.Xna.Framework.Game" />
    public class TankGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //the main game
        public static GameState State = GameState.Menu;

        /// <summary>
        /// Initializes a new instance of the <see cref="TankGame"/> class.
        /// </summary>
        public TankGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1050;
            graphics.PreferredBackBufferHeight = 576;
            IsMouseVisible = false;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //loads all tools data (see Tools)
            Tools.Screen = GraphicsDevice.Viewport.Bounds;
            Tools.Graphics = GraphicsDevice;
            Tools.Content = Content;
            Tools.ButtonImg = Content.Load<Texture2D>("Images/Sprites/UI/Button BG shadow");
            Tools.Font = Content.Load<SpriteFont>("Fonts/File");

            //loads the game scene
            GameScene.LoadContent();

        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Updates the game.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
            //updates tools game time
            Tools.GameTime = gameTime;

            //chooses the appropriate state to update
            switch (State)
            {
                case GameState.Game:
                    GameScene.Update();
                    IsMouseVisible = false;
                    graphics.ApplyChanges();
                    break;
                case GameState.Shop:
                    Shop.Instance.Update();
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
                    break;
                case GameState.Menu:
                    Menu.Update(this);
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
                    break;
                case GameState.LoseScreen:
                    LoseScreen.Update();
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
                    break;
                case GameState.Pause:
                    PauseMenu.Update();
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
                    break;
                case GameState.Instructions:
                    Instructions.Update();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //chooses the appropriate state to draw
            switch (State)
            {
                case GameState.Game:
                    GameScene.Draw(spriteBatch);
                    break;
                case GameState.Shop:
                    GameScene.Draw(spriteBatch);
                    Shop.Instance.Draw(spriteBatch);
                    break;
                case GameState.Menu:
                    Menu.Draw(spriteBatch);
                    break;
                case GameState.LoseScreen:
                    GameScene.Draw(spriteBatch);
                    LoseScreen.Draw(spriteBatch);
                    break;
                case GameState.Pause:
                    GameScene.Draw(spriteBatch);
                    PauseMenu.Draw(spriteBatch);
                    break;
                case GameState.Instructions:
                    Instructions.Draw(spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
