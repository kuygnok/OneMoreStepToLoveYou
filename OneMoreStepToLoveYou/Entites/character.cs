using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMoreStepToLoveYou.Entites
{
    public class character
    {
        public Sprite sprite;
        public gridPosition m_gridPosition;
        private Vector2 targetPosition;
        private int moveSpeed = 10;
        public bool is_move = false;

        public void moveLeft()
        {
            if (m_gridPosition.column <= 0 || gameManager.GRID_DATA[m_gridPosition.left.row, m_gridPosition.left.column].type == gridType.Unwalkable)
                return;

            m_gridPosition = m_gridPosition.left;
            is_move = true;
            targetPosition = gameManager.GRID_DATA[m_gridPosition.row, m_gridPosition.column].getCenterGridPosition;
            targetPosition -= kaninKitRail.getCenterPoint(sprite.gameSprite.Width, sprite.gameSprite.Height);
        }

        public void moveRight()
        {
            if (m_gridPosition.column >= gameManager.GRID_COLUMN - 1 || gameManager.GRID_DATA[m_gridPosition.right.row, m_gridPosition.right.column].type == gridType.Unwalkable)
                return;

            m_gridPosition = m_gridPosition.right;
            is_move = true;
            targetPosition = gameManager.GRID_DATA[m_gridPosition.row, m_gridPosition.column].getCenterGridPosition;
            targetPosition -= kaninKitRail.getCenterPoint(sprite.gameSprite.Width, sprite.gameSprite.Height);
        }

        public void moveUp()
        {
            if (m_gridPosition.row <= 0 || gameManager.GRID_DATA[m_gridPosition.up.row, m_gridPosition.up.column].type == gridType.Unwalkable)
                return;

            m_gridPosition = m_gridPosition.up;
            is_move = true;
            targetPosition = gameManager.GRID_DATA[m_gridPosition.row, m_gridPosition.column].getCenterGridPosition;
            targetPosition -= kaninKitRail.getCenterPoint(sprite.gameSprite.Width, sprite.gameSprite.Height);
        }

        public void moveDown()
        {
            if (m_gridPosition.row >= gameManager.GRID_ROW - 1 || gameManager.GRID_DATA[m_gridPosition.down.row, m_gridPosition.down.column].type == gridType.Unwalkable)
                return;

            m_gridPosition = m_gridPosition.down;
            is_move = true;
            targetPosition = gameManager.GRID_DATA[m_gridPosition.row, m_gridPosition.column].getCenterGridPosition;
            targetPosition -= kaninKitRail.getCenterPoint(sprite.gameSprite.Width, sprite.gameSprite.Height);
        }

        public void updatePosition()
        {
            //sprite.position = gameManager.GRID_DATA[m_gridPosition.row, m_gridPosition.column].getCenterGridPosition;
            //sprite.position -= kaninKitRail.getCenterPoint(sprite.gameSprite.Width, sprite.gameSprite.Height);
            if (is_move)
            {
                if(Vector2.Distance(sprite.position, targetPosition) <= 1)
                {
                    is_move = false;
                    return;
                }
                Vector2 diration = targetPosition - sprite.position;
                diration.Normalize();
                diration *= moveSpeed;
                sprite.position += diration;
            }
        }
    }
}
