using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


namespace SMUS
{
    class Program
    {
        public static bool IsRunning = true;

        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(100, 50), "Smus");
            //SongList list = new SongList();


            while (IsRunning)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);
                
                // list.Update(window);

                window.Display();
            }
        }
    }
}
