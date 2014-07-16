using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Tools;

namespace SMUS.Module
{
    class SpriteBatchMod : Module
    {
        private readonly SpriteBatch spriteBatch;

        public SpriteBatchMod(SpriteBatch sb)
        {
            spriteBatch = sb;
        }

        public override void Update()
        {
            spriteBatch.Render(Program.Window);
        }
    }
}
