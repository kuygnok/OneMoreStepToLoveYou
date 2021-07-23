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
        text debugText;

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

            //debug texts
            debugText = new text(Content.Load<SpriteFont>("debugFont"), Color.Black, Vector2.Zero);

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
            scene.entites.Add(new player(Content.Load<Texture2D>("Player"), new gridPosition(5, 2)));
            scene.entites[1].DrawOrder = 2;
            //crowd
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(7, 2)));
            scene.entites[2].DrawOrder = 2;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(8, 2)));
            scene.entites[3].DrawOrder = 2;
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

            string debugMessege = "";
            for (int i = 0; i < gameManager.GRID_ROW; i++)
            {
                for (int j = 0; j < gameManager.GRID_COLUMN; j++)
                {
                    debugMessege += gameManager.GRID_DATA[i, j].type.ToString() + "   | ";
                }
                debugMessege += "\n";
            }
            debugMessege += "\n\n\n\n" + gameManager.playerStep;
            debugText.drawFont(spriteBatch, debugMessege);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
