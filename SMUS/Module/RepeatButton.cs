using SFML.Tools;
using SFML.Window;

namespace SMUS.Module
{
    class RepeatButton : Module
    {
        private readonly Button button;

        public RepeatButton()
        {
            button = new Button(new BatchedSprite(new Vector2f(0,0), Program.AtlasData["repeat"], 4 ));
            button.sprite.Position = new Vector2f(Program.Window.Size.X - Program.Window.Size.X / 4, button.sprite.AtlasPosition.Width/2);
            button.sprite.Colour = Config.Colors.ButtonsFaded;
            button.shadowSprite.Position = button.sprite.Position += new Vector2f(0, 1);

            button.OnPress += () =>
            {
                bool repeat = Audio.NextState == Audio.State.Repeat;
                Audio.NextState = !repeat ? Audio.State.Repeat : Audio.State.Next;
                button.sprite.Colour = !repeat ? Config.Colors.Buttons : Config.Colors.ButtonsFaded;
                Program.SpriteBatch.CalculateVertices();
            };
        }

        public override void Update()
        {
            if (Audio.NextState == Audio.State.Repeat) return;
            button.sprite.Colour = Config.Colors.ButtonsFaded;
            Program.SpriteBatch.CalculateVertices();
        }
    }
}
