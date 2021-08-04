using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OneMoreStepToLoveYou.Entites;

namespace OneMoreStepToLoveYou.GameInterface
{
    public class I_dialouge : I_gameInterface
    {
        public int DrawOrder { get; set; }

        private bool is_play;

        private float transitionSpeed = 0.05f;
        private float MAX_BG_midderAlpha = 0.9f;
        private float upperDestinationY = -340f;
        private float lowerDestinationY = 700f;
        private float transitionMovingSpeed = 15;

        private float BG_Alpha = 0;
        private Sprite BG_upper;
        private Sprite BG_lower;
        private Sprite BG_midder;

        public I_dialouge(GraphicsDeviceManager graphics)
        {
            Vector2 sceneSize = new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Vector2 startPoint = Vector2.Zero;
            BG_upper = new Sprite(kaninKitRail.getBoxTexture(graphics, (int)sceneSize.X, (int)sceneSize.Y / 2, Color.White, 0), startPoint - new Vector2(0, 150), Color.White * 0);
            BG_lower = new Sprite(kaninKitRail.getBoxTexture(graphics, (int)sceneSize.X, (int)sceneSize.Y / 2, Color.White, 0), startPoint + new Vector2(0, 450), Color.White * 0);
            BG_midder = new Sprite(kaninKitRail.getBoxTexture(graphics, (int)sceneSize.X, (int)sceneSize.Y, Color.Black, 0), startPoint, Color.White * 0);
            gameManager.dialouge = this;
        }

        public void Update(float animator_elapsed)
        {
            if(is_play)
            {
                if (BG_Alpha < 1)
                {
                    BG_Alpha += transitionSpeed;
                    BG_lower.tintColor = Color.White * BG_Alpha;
                    BG_upper.tintColor = Color.White * BG_Alpha;
                    if(BG_Alpha < MAX_BG_midderAlpha)
                        BG_midder.tintColor = Color.White * BG_Alpha;
                }
                if(BG_lower.position.Y < lowerDestinationY || BG_upper.position.Y > upperDestinationY)
                {
                    if (BG_lower.position.Y < lowerDestinationY)
                        BG_lower.position.Y += transitionMovingSpeed;
                    if (BG_upper.position.Y > upperDestinationY)
                        BG_upper.position.Y -= transitionMovingSpeed;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BG_midder.Draw(spriteBatch);
            BG_upper.Draw(spriteBatch);
            BG_lower.Draw(spriteBatch);
        }

        public void dialogeOn()
        {
            gameManager.is_PAUSE = true;
            is_play = true;
        }
    }
}
