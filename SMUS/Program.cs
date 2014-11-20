using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Threading;
using SFML.Graphics;
using SFML.Window;
using SFML.Tools;
using SMUS.Module;

namespace SMUS
{
    internal class Program
    {
        public static bool IsRunning = true;
        public static RenderWindow Window;
        public static SpriteBatch SpriteBatch;
        public static AtlasData AtlasData;        
        public static bool WindowFocused = true;
        public static string AssPath;

        private static void Main(string[] args)
        {
            //Window set up.
            Window = new RenderWindow(new VideoMode(550, 104), "Smus", Styles.None);
            Window.SetFramerateLimit(61);
            Window.GainedFocus += (o, e) => { WindowFocused = true; };
            Window.LostFocus += (o, e) => { WindowFocused = false; };
            Window.MouseButtonPressed += (o, e) =>
            {
                //Hack to fix window focus.
                if (!WindowFocused)
                    Window.Position += new Vector2i(0, 0);
            };

            //Make sure the window can close properly.
            Window.Closed += (o,e) => Window.Close();
            Window.Closed += (o, e) => { if (Audio.Engine != null) Audio.Engine.Dispose(); };
            Window.Closed += (o, e) => IsRunning = false;

            //Get bin directory.
            var ass = Assembly.GetExecutingAssembly();
            AssPath = Path.GetDirectoryName(ass.Location);
 
            //Initialize Audio
            Audio.StartEngine();

            //Config
            Config.PopulateConfig(AssPath + "/Resources/Config/config.xml");
            
            //SpriteBatch/Atlas
            AtlasData = new AtlasData(AssPath + "/Resources/Textures/Atlas");
            SpriteBatch = new SpriteBatch(AtlasData.AtlasTexture);

            //Container
            var moduleContainer = new ModuleContainer();

            //Modules
            LoadModules(moduleContainer);

            //Update SpriteBatch
            SpriteBatch.SortZ();
            SpriteBatch.CalculateVertices();

            //Main loop
            while (IsRunning)
            {
                Window.DispatchEvents();
                if (!WindowFocused)
                    Thread.Sleep(200); //Doesn't need to run as smooth.
        
                Window.Clear(Config.Colors["background"]);

                if ((Keyboard.IsKeyPressed(Keyboard.Key.LAlt) || Keyboard.IsKeyPressed(Keyboard.Key.RAlt)) &&
                    Keyboard.IsKeyPressed(Keyboard.Key.F4))
                    IsRunning = false;

                moduleContainer.Update();

                Window.Display();
            }

            Audio.Engine.Dispose();
            Window.Close();
        }

        private static void LoadModules(ModuleContainer moduleContainer)
        {
            //Global resource/s.
            var baseFont = new Font(AssPath + "/Resources/Fonts/SourceSansPro-Regular.otf");
            baseFont.GetTexture(14).Smooth = false;
            //Modules
            /*  Modules shouldn't depend on other modules unless absolutely neccessary.
             *  Draw order is determined by the order they are added to the container.
             */
            var pBar = new ProgressBar();
            var songList = new SongList(baseFont);
            Audio.CurrentSongList = songList;
            var dragWindow = new DragWindow();
            var playButton = new PlayButton();
            var border = new Border();
            var volumeControl = new VolumeControl();
            var shuffle = new ShuffleButton();
            var repeat = new RepeatButton();
            var spriteBatch = new SpriteBatchMod(SpriteBatch);
            var search = new TextSearch(songList, baseFont);
            var hotkeys = new HotKeys(pBar, songList, volumeControl);

            moduleContainer.AddModule(pBar);
            moduleContainer.AddModule(songList);
            moduleContainer.AddModule(dragWindow);
            moduleContainer.AddModule(playButton);
            moduleContainer.AddModule(volumeControl);
            moduleContainer.AddModule(shuffle);
            moduleContainer.AddModule(repeat);
            moduleContainer.AddModule(spriteBatch);
            moduleContainer.AddModule(border);
            moduleContainer.AddModule(search);
            moduleContainer.AddModule(hotkeys);

            //Module specific resources
            songList.LoadFromMultipleDirectories(Config.musicDirectories);
            songList.SortByArtist();

            //Remove duplicates.
            var duplicates = songList.GroupBy(x => x.Name).Where(x=>x.Count() > 1);
            foreach (Song song in duplicates
                .SelectMany(duplicate => duplicate.Take(duplicate.Count()-1)))
            {
                songList.Remove(song);
            }
        }
    }
}