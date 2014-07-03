using System.IO;
using SFML.Graphics;
using SFML.Window;
using SMUS.Module;

namespace SMUS
{
    internal class Program
    {
        public static bool IsRunning = true;
        public static RenderWindow Window;

        private static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(550, 104), "Smus", Styles.None);
            Window.SetFramerateLimit(60);


            Config.PopulateConfig(Directory.GetCurrentDirectory() + "/Resources/Config/config.xml");
            //Container
            var moduleContainer = new ModuleContainer();

            LoadModules(moduleContainer);

            //Main loop
            while (IsRunning)
            {
                Window.DispatchEvents();
                Window.Clear(Config.Colors.Background);

                moduleContainer.Update();

                Window.Display();
            }
        }

        private static void LoadModules(ModuleContainer moduleContainer)
        {
            //Global resource/s.
            var baseFont = new Font(Directory.GetCurrentDirectory() + "/Resources/Fonts/SourceSansPro-Regular.otf");

            //Modules
            /*  Modules shouldn't depend on other modules unless absolutely neccessary.
             *  Draw order is determined by the order they are added to the container.
             */
            var pBar = new ProgressBar();
            var songList = new SongList(baseFont);
            var dragWindow = new DragWindow();
            var playButton = new PlayButton();
            var border = new Border();

            moduleContainer.AddModule(pBar);
            moduleContainer.AddModule(songList);
            moduleContainer.AddModule(dragWindow);
            moduleContainer.AddModule(playButton);
            moduleContainer.AddModule(border);

            //Module specific resources
            songList.LoadFromMultipleDirectories(Config.musicDirectories);
        }
    }
}