using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMoreStepToLoveYou.Entites
{
    public class playerCamera
    {
        public Matrix Transform { get; set; }

        public void Follow(Sprite target)
        {
            var position = Matrix.CreateTranslation(
              (1920 / 2) * -1,
              -target.position.Y - (target.gameSprite.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                1920 / 2,
                1080 / 2,
                0);

            Transform = position * offset;
        }
    }
}
