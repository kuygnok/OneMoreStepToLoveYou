using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OneMoreStepToLoveYou.GameInterface;
using OneMoreStepToLoveYou.Entites;

namespace OneMoreStepToLoveYou
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        I_sceneManager scene = new I_sceneManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //grid
            scene.entites.Add(new I_gridBox(5, 10, Content.Load<SpriteFont>("debugFont"), graphics));
            scene.entites[0].DrawOrder = 1;
            gameManager.addShadowArea(1, 0);
            gameManager.addShadowArea(1, 1);
            gameManager.addShadowArea(1, 2);
            gameManager.addShadowArea(1, 3);
            gameManager.addShadowArea(2, 3);
            gameManager.addShadowArea(2, 2);

            gameManager.addShadowArea(3, 2);
            gameManager.addShadowArea(3, 3);
            //player
            scene.entites.Add(new player(Content.Load<Texture2D>("Player"), new gridPosition(6, 2)));
            scene.entites[1].DrawOrder = 2;
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            scene.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            scene.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
