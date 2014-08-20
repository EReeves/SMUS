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
