using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OneMoreStepToLoveYou.Entites
{
    public class gridItem
    {
        public Sprite sprite;
        private Vector2 centerPosition;
        public gridType type;

        public gridItem(Texture2D texture, Vector2 position, Color tiniColor, gridType type)
        {
            this.sprite = new Sprite(texture, position, tiniColor);
            centerPosition = kaninKitRail.getCenterPoint(position, texture.Width, texture.Height);
            this.type = type;
        }

        public Vector2 getCenterGridPosition
        {
            get
            {
                return centerPosition;
            }
        }
    }
}
