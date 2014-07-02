using System.Collections.Generic;

namespace SMUS.Module
{
    internal class ModuleContainer
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

    internal abstract class Module : IModule
    {
        public abstract void Update();
    }

    internal interface IModule
    {
        void Update();
    }
}