﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OneMoreStepToLoveYou.Entites
{
    public class Sprite
    {
        public Texture2D gameSprite;
        public Vector2 position;
        public Color tintColor;

        public Rectangle spriteBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, gameSprite.Width, gameSprite.Height);
            }
        }

        public Sprite(Texture2D gameSprite, Vector2 position, Color tintColor)
        {
            this.gameSprite = gameSprite;
            this.position = position;
            this.tintColor = tintColor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameSprite, position, tintColor);
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            spriteBatch.Draw(gameSprite, position, null, tintColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
        }
    }
}
