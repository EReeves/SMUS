using SFML.Tools;
using SFML.Window;

namespace SMUS.Module
{
    class ShuffleButton : Module
    {
        private readonly Button button;

        public ShuffleButton()
        {
            button = new Button(new BatchedSprite(new Vector2f(0,0), Program.AtlasData["shuffle"], 4 ));
            button.sprite.Position = new Vector2f(Program.Window.Size.X - Program.Window.Size.X / 4, Program.Window.Size.Y - (button.sprite.AtlasPosition.Height + button.sprite.AtlasPosition.Height/ 2));
            button.sprite.Colour = Config.Colors["buttonsfaded"];
            button.shadowSprite.Position = button.sprite.Position += new Vector2f(0, 1);
            
            button.OnPress += () =>
            {
                bool shuffle = Audio.NextState == Audio.State.Shuffle;
                Audio.NextState = !shuffle ? Audio.State.Shuffle : Audio.State.Next;
                button.sprite.Colour = !shuffle ? Config.Colors["buttons"] : Config.Colors["buttonsfaded"];
                Program.SpriteBatch.CalculateVertices();
            };
        }

        public override void Update()
        {
            if (Audio.NextState == Audio.State.Shuffle) return;
            button.sprite.Colour = Config.Colors["buttonsfaded"];
            Program.SpriteBatch.CalculateVertices();

        }
    }
}
