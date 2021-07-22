using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace OneMoreStepToLoveYou
{
    public class kaninKitRail
    {
        public static Vector2 getCenterPoint(Vector2 position, int width, int height)
        {
            return new Vector2(width / 2, height / 2) + position;
        }

        public static Vector2 getCenterPoint(int width, int height)
        {
            return new Vector2(width / 2, height / 2);
        }
    }
}
