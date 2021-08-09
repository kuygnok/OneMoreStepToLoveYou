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
        static int sceneToGo;

        //transitional
        float transitionSpeed = 0.05f;
        float transitionAlpha = 1f;
        Sprite transitionPanel;
        static bool is_fadeOut = true;
        static bool is_fadeIn = false;

        //camera
        playerCamera camera;
        public static bool is_CameraOn = false;

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
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //camera
            camera = new playerCamera();

            //transition
            transitionPanel = new Sprite(kaninKitRail.getBoxTexture(graphics, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, Color.Black, 0), Vector2.Zero, Color.Black);

            //debug texts
            debugText = new text(Content.Load<SpriteFont>("debugFont"), Color.Black, Vector2.Zero);

            //in game entites
            scene_LV1();

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
            #region transition
            if (is_fadeIn)
            {
                if (transitionAlpha < 1)
                {
                    transitionAlpha += transitionSpeed;
                    transitionPanel.tintColor = Color.Black * transitionAlpha;
                }
                else
                {
                    is_fadeIn = false;
                    resetConfingulation();
                    sceneChange();
                    is_fadeOut = true;
                }
            }
            else if (is_fadeOut)
            {
                if (transitionAlpha > 0)
                {
                    transitionAlpha -= transitionSpeed;
                    transitionPanel.tintColor = Color.Black * transitionAlpha;
                }
                else if (transitionAlpha <= 0)
                    is_fadeOut = false;

                //load some content
            }
            else
            {
                scene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            #endregion
            if (is_CameraOn)
                camera.Follow(gameManager.M_PLAYER.sprite);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //draw component
            if (is_CameraOn)
                spriteBatch.Begin(transformMatrix: camera.Transform);
            else
                spriteBatch.Begin();
            scene.Draw(spriteBatch);
            /*string debugMessege = "";
            for (int i = 0; i < gameManager.GRID_ROW; i++)
            {
                for (int j = 0; j < gameManager.GRID_COLUMN; j++)
                {
                    debugMessege += gameManager.GRID_DATA[i, j].type.ToString() + "   | ";
                }
                debugMessege += "\n";
            }
            debugMessege += "\n\n\n\n" + gameManager.playerStep;
            debugText.drawFont(spriteBatch, debugMessege);*/
            spriteBatch.End();

            //draw transition
            spriteBatch.Begin();
            transitionPanel.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void sceneChange()
        {
            switch (sceneToGo)
            {
                case 1:
                    scene_LV1();
                    break;
                case 2:
                    scene_LV2();
                    break;
                default:
                    break;
            }
        }

        public static void changeSceneTo(int sceneIndex)
        {
            sceneToGo = sceneIndex;
            //fade transition panel
            is_fadeIn = true;
        }

        private static void resetConfingulation()
        {
            scene.entites.Clear();
            is_CameraOn = false;
            gameManager.crowds.Clear();
            gameManager.GRID_DATA = null;
            gameManager.playerStep = 0;
            gameManager.ya = null;
        }

        private void scene_LV1()
        {
            //grid
            scene.entites.Add(new I_gridBox(7, 8, Content.Load<SpriteFont>("debugFont"), graphics));
            scene.entites[0].DrawOrder = 1;
            gameManager.addShadowArea(0, 4);
            gameManager.addShadowArea(0, 6);
            gameManager.addShadowArea(1, 0);
            gameManager.addShadowArea(2, 0);
            gameManager.addShadowArea(5, 0);
            gameManager.addShadowArea(3, 0);
            gameManager.addShadowArea(4, 0);
            gameManager.addShadowArea(6, 0);
            gameManager.addShadowArea(7, 2);
            gameManager.addShadowArea(7, 3);
            gameManager.addShadowArea(7, 4);
            gameManager.addShadowArea(7, 5);
            gameManager.addShadowArea(7, 6);

            //player
            scene.entites.Add(new player(Content.Load<Texture2D>("qq"), new gridPosition(0, 0)));
            scene.entites[1].DrawOrder = 2;
            //ya dob
            scene.entites.Add(new yaDov(new gridPosition(0, 1), Content.Load<Texture2D>("ya")));
            scene.entites[2].DrawOrder = 2;
            //crowd
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 5)));
            scene.entites[3].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 3)));
            scene.entites[4].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 2)));
            scene.entites[5].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(0, 3)));
            scene.entites[6].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(2, 1)));
            scene.entites[7].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(2, 2)));
            scene.entites[8].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(2, 6)));
            scene.entites[9].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(4, 1)));
            scene.entites[10].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(3, 3)));
            scene.entites[11].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(3, 4)));
            scene.entites[12].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(3, 5)));
            scene.entites[13].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(4, 2)));
            scene.entites[14].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(5, 1)));
            scene.entites[15].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(5, 3)));
            scene.entites[16].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(5, 4)));
            scene.entites[17].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(5, 5)));
            scene.entites[18].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(5, 6)));
            scene.entites[19].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(6, 3)));
            scene.entites[20].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 1)));
            scene.entites[21].DrawOrder = 3;
            //p earth
            scene.entites.Add(new pEarth(new gridPosition(0, 2), Content, "CoketumpBreathe", 3, 1, 10));
            scene.entites[22].DrawOrder = 3;
            //dialoge
            scene.entites.Add(new I_dialouge(graphics));
            scene.entites[23].DrawOrder = 10;
            gameManager.dialouge.addDialogue(new dialouge("พี่โลก", "ราเซนกันนนนน", Content.Load<Texture2D>("pEarthRasengunSaiNaKung"), 0.6f));
        }

        private void scene_LV2()
        {
            //grid
            scene.entites.Add(new I_gridBox(7, 8, Content.Load<SpriteFont>("debugFont"), graphics));
            scene.entites[0].DrawOrder = 1;
            gameManager.addShadowArea(0, 3);
            gameManager.addShadowArea(1, 2);
            gameManager.addShadowArea(1, 3);
            gameManager.addShadowArea(0, 4);
            gameManager.addShadowArea(1, 4);
            gameManager.addShadowArea(2, 2);
            gameManager.addShadowArea(3, 0);
            gameManager.addShadowArea(3, 2);
            gameManager.addShadowArea(4, 2);
            gameManager.addShadowArea(4, 3);
            gameManager.addShadowArea(4, 4);
            gameManager.addShadowArea(5, 4);
            gameManager.addShadowArea(7, 4);
            gameManager.addShadowArea(7, 5);


            //player
            scene.entites.Add(new player(Content.Load<Texture2D>("qq"), new gridPosition(0, 5)));
            scene.entites[1].DrawOrder = 2;
            //ya dob
            scene.entites.Add(new yaDov(new gridPosition(6, 4), Content.Load<Texture2D>("ya")));
            scene.entites[2].DrawOrder = 2;
            //crowd
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 5)));
            scene.entites[3].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(1, 6)));
            scene.entites[4].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(2, 5)));
            scene.entites[5].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(3, 5)));
            scene.entites[6].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(4, 5)));
            scene.entites[7].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(4, 6)));
            scene.entites[8].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(6, 2)));
            scene.entites[9].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(6, 3)));
            scene.entites[10].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(7, 0)));
            scene.entites[11].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(7, 1)));
            scene.entites[12].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(5, 1)));
            scene.entites[13].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(5, 2)));
            scene.entites[14].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(3, 1)));
            scene.entites[15].DrawOrder = 3;
            scene.entites.Add(new crowd(Content.Load<Texture2D>("Player"), new gridPosition(2, 1)));
            scene.entites[16].DrawOrder = 3;
            //p earth
            scene.entites.Add(new pEarth(new gridPosition(0, 2), Content, "CoketumpBreathe", 3, 1, 10));
            scene.entites[17].DrawOrder = 3;
            //dialoge
            /*scene.entites.Add(new I_dialouge(graphics));
            scene.entites[7].DrawOrder = 10;*/
        }
    }
}
