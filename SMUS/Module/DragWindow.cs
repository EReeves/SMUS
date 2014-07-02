using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    //Move the window while right click is held in.
    internal class DragWindow : Module, IModule
    {
        private bool drag;
        private Vector2i mousePos;

        public override void Update()
        {
            Move();
        }

        private void Move()
        {
            if (drag)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Right))
                {
                    Program.Window.Position -= mousePos - Mouse.GetPosition();
                    mousePos = Mouse.GetPosition();
                }
                else
                {
                    drag = false;
                }
            }
            else if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                Vector2i pos = Mouse.GetPosition(Program.Window);
                if (pos.X < 0 || pos.Y < 0 ||
                    pos.X > Program.Window.Size.X || pos.Y > Program.Window.Size.Y) return;

                drag = true;
                mousePos = Mouse.GetPosition();
            }
        }
    }
}