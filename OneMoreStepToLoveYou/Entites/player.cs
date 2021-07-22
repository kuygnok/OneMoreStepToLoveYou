using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using OneMoreStepToLoveYou.GameInterface;

namespace OneMoreStepToLoveYou.Entites
{
    public class player : character, I_gameInterface
    {
        public int DrawOrder { get; set; }

        public player(Texture2D texture, gridPosition gridPos)
        {
            sprite = new Sprite(texture, Vector2.Zero, Color.White);
            m_gridPosition = gridPos;
            sprite.position = gameManager.GRID_DATA[m_gridPosition.row, m_gridPosition.column].getCenterGridPosition;
            sprite.position -= kaninKitRail.getCenterPoint(sprite.gameSprite.Width, sprite.gameSprite.Height);
        }

        public void Update()
        {
            keyboard.GetState();
            if (keyboard.HasBeenPressed(Keys.A))
                moveLeft();
            else if (keyboard.HasBeenPressed(Keys.D))
                moveRight();
            else if (keyboard.HasBeenPressed(Keys.S))
                moveDown();
            else if (keyboard.HasBeenPressed(Keys.W))
                moveUp();

            updatePosition();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
