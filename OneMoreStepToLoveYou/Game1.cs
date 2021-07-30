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

        //scene
        public static I_sceneManager scene = new I_sceneManager();
        text debugText;

        //transitional
        float transitionSpeed = 0.01f;
        float transitionAlpha = 1f;
        Sprite transitionPanel;
        bool is_fadeOut = true;
        bool is_fadeIn = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080 - 100;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //transition
            transitionPanel = new Sprite(Content.Load<Texture2D>("transition"), Vector2.Zero, Color.Black);

            //debug texts
            debugText = new text(Content.Load<SpriteFont>("debugFont"), Color.Black, Vector2.Zero);

            //grid
            scene.entites.Add(new I_gridBox(6, 6, Content.Load<SpriteFont>("debugFont"), graphics));
            scene.entites[0].DrawOrder = 1;
            gameManager.addShadowArea(5, 1);
            gameManager.addShadowArea(5, 2);
            gameManager.addShadowArea(5, 3);
            gameManager.addShadowArea(5, 4);
            gameManager.addShadowArea(5, 5);
            gameManager.addShadowArea(3, 1);
            gameManager.addShadowArea(4, 1);

            //player
            scene.entites.Add(new player(Content.Load<Texture2D>("qq"), new gridPosition(0, 4)));
            scene.entites[1].DrawOrder = 2;
            //ya dob
            scene.entites.Add(new yaDov(new gridPosition(0, 0), Content.Load<Texture2D>("ya")));
            scene.entites[2].DrawOrder = 2;
            //crowd
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 4)));
            scene.entites[3].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 3)));
            scene.entites[4].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 2)));
            scene.entites[5].DrawOrder = 3;
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
            if(is_fadeIn)
            {
                if (transitionAlpha < 1)
                {
                    transitionAlpha += transitionSpeed;
                    transitionPanel.tintColor = Color.Black * transitionAlpha;
                }
                else
                    is_fadeIn = false;
            }
            else if(is_fadeOut)
            {
                if (transitionAlpha > 0)
                {
                    transitionAlpha -= transitionSpeed;
                    transitionPanel.tintColor = Color.Black * transitionAlpha;
                }
                else if (transitionAlpha <= 0)
                    is_fadeOut = false;
            }
            else
            {
                scene.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            scene.Draw(spriteBatch);
            transitionPanel.Draw(spriteBatch);

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
