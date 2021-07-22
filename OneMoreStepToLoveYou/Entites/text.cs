using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OneMoreStepToLoveYou.Entites
{
    public class text
    {
        private SpriteFont font;
        public Color fontColor;
        private Vector2 position;

        public text(SpriteFont font, Color fontColor, Vector2 position)
        {
            this.font = font;
            this.fontColor = fontColor;
            this.position = position;
        }

        public void drawFont(SpriteBatch spriteBatch, String messege)
        {
            spriteBatch.DrawString(font, messege, position, fontColor);
        }
    }
}
