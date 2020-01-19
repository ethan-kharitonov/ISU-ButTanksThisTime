using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_ButTanksThisTime
{
    public enum State
    {
        Game,
        Shop,
        Menu,
        LoseScreen,
        Pause
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static State state = State.Menu;

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
            Tools.buttonImg = Content.Load<Texture2D>("Images/Sprites/UI/Button BG shadow");
            GameScene.LoadContent();

            Tools.Font = Content.Load<SpriteFont>("Fonts/File");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Tools.GameTime = gameTime;
            switch (state)
            {
                case State.Game:
                    GameScene.Update();
                    IsMouseVisible = false;
                    graphics.ApplyChanges();
                    break;
                case State.Shop:
                    Shop.Update();
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (state)
            {
                case State.Game:
                    GameScene.Draw(spriteBatch);
                    break;
                case State.Shop:
                    GameScene.Draw(spriteBatch);
                    Shop.Draw(spriteBatch);
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