using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace OneMoreStepToLoveYou.GameInterface
{
    public interface I_gameInterface
    {
        int DrawOrder { get; set; }

        void Update(float animator_elapsed);
        void Draw(SpriteBatch spriteBatch);
    }
}
