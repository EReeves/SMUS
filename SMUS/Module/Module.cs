using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using SFML.Graphics;

namespace SMUS.Module
{
    class ModuleContainer
    {
        private readonly List<IModule> modules = new List<IModule>();
        public readonly Locks Locks = new Locks();

        public ModuleContainer()
        {
            
        }

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
        public Locks Locks { get; set; }

        protected Module(Locks locks, RenderWindow window)
        {
            Window = window;
            Locks = locks;
        }

        public abstract void Update();
    }

    interface IModule
    {
        RenderWindow Window { get; set; }
        Locks Locks { get; set; }
        void Update();
    }
}
