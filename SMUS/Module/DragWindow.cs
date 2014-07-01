using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    //move the window while right click is held in.
    class DragWindow : Module
    {
        private Vector2i mousePos;
        private bool drag = false;

        public DragWindow(RenderWindow _window) : base(_window)
        {
            
        }

        public override void Update()
        {
            if (drag)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Right))
                {
                    Window.Position -= mousePos - Mouse.GetPosition();
                    mousePos = Mouse.GetPosition();
                }
                else
                    drag = false;
            }
            else if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                drag = true;
                mousePos = Mouse.GetPosition();
            }
        }
    }
}
