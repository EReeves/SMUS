using System;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    //move the window while right click is held in.
    class DragWindow : Module, IModule
    {
        private Vector2i mousePos;
        private bool drag = false;

        public DragWindow(Locks locks, RenderWindow window) : base(locks, window)
        {     
            Locks.Add("windowmove");
        }

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
                    Window.Position -= mousePos - Mouse.GetPosition();
                    mousePos = Mouse.GetPosition();
                }
                else
                {
                    drag = false;
                    Locks.Release("windowmove");
                }
            }
            else if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                Locks.Use("windowmove", () => { });
                
                drag = true;
                mousePos = Mouse.GetPosition();
            }
        }
    }
}
