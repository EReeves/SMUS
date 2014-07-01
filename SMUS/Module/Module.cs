using System.Collections.Generic;
using SFML.Graphics;

namespace SMUS.Module
{
    class ModuleContainer
    {
        private readonly List<IModule> modules = new List<IModule>();

        public void AddModule(IModule module)
        {
            modules.Add(module);
        }

        public void Update()
        {
            foreach (IModule module in modules)
            {
                module.Update();
            }
        }
    }

    abstract class Module : IModule
    {
        public RenderWindow Window { get; set; }

        protected Module(RenderWindow _window)
        {
            Window = _window;
        }

        public abstract void Update();

    }

    interface IModule
    {
        RenderWindow Window { get; set; }
        void Update();
    }
}
