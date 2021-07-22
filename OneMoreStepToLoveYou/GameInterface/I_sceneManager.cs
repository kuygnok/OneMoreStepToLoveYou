using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using OneMoreStepToLoveYou.GameInterface;

namespace OneMoreStepToLoveYou.GameInterface
{
    public class I_sceneManager
    {
        public List<I_gameInterface> entites = new List<I_gameInterface>();
        List<I_gameInterface> entitesRemove = new List<I_gameInterface>();

        public void Update()
        {
            foreach (I_gameInterface item in entites)
            {
                if (entitesRemove.Contains(item))
                    continue;

                item.Update();
            }

            foreach (I_gameInterface item in entitesRemove)
            {
                entites.Remove(item);
            }

            entitesRemove.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (I_gameInterface item in entites.OrderBy(e => e.DrawOrder))
            {
                item.Draw(spriteBatch);
            }
        }

        public void Remove(I_gameInterface remover)
        {
            entitesRemove.Add(remover);
        }
    }
}
