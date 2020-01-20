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

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Enum State
    /// </summary>
    public enum State
    {
        Game,
        Shop,
        Menu,
        LoseScreen,
        Pause
    }

    /// <summary>
    /// Class Game1.
    /// Implements the <see cref="Microsoft.Xna.Framework.Game" />
    /// </summary>
    /// <seealso cref="Microsoft.Xna.Framework.Game" />
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static State State = State.Menu;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game1"/> class.
        /// </summary>
        public Game1()
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
            Tools.Screen = GraphicsDevice.Viewport.Bounds;
            Tools.Graphics = GraphicsDevice;
            Tools.Content = Content;
            Tools.ButtonImg = Content.Load<Texture2D>("Images/Sprites/UI/Button BG shadow");
            GameScene.LoadContent();

            Tools.Font = Content.Load<SpriteFont>("Fonts/File");
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
            Tools.GameTime = gameTime;
            switch (State)
            {
                case State.Game:
                    GameScene.Update();
                    IsMouseVisible = false;
                    graphics.ApplyChanges();
                    break;
                case State.Shop:
                    Shop.Instance.Update();
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
                    break;
                case State.Menu:
                    Menu.Update(this);
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
                    break;
                case State.LoseScreen:
                    LoseScreen.Update();
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
                    break;
                case State.Pause:
                    PauseMenu.Update();
                    IsMouseVisible = true;
                    graphics.ApplyChanges();
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

            switch (State)
            {
                case State.Game:
                    GameScene.Draw(spriteBatch);
                    break;
                case State.Shop:
                    GameScene.Draw(spriteBatch);
                    Shop.Instance.Draw(spriteBatch);
                    break;
                case State.Menu:
                    Menu.Draw(spriteBatch);
                    break;
                case State.LoseScreen:
                    GameScene.Draw(spriteBatch);
                    LoseScreen.Draw(spriteBatch);
                    break;
                case State.Pause:
                    GameScene.Draw(spriteBatch);
                    PauseMenu.Draw(spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}