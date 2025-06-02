using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HijackGen.Plugins
{
    internal sealed class PluginManager
    {
        public readonly List<Plugin> BuiltInPlugins = [];
        public readonly List<Plugin> ThirdPartyPlugins = [];

        private readonly string DefaultPluginDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");

        public void LoadBuiltInPlugins()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(Plugin).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))
            {
                BuiltInPlugins.Add(Activator.CreateInstance(type) as Plugin);
            }
        }

        public void LoadThirdPartyPlugins()
        {
            LoadThirdPartyPlugins(DefaultPluginDirectory);
        }

        public void LoadThirdPartyPlugins(string pluginDirectory)
        {
            if (!Directory.Exists(pluginDirectory))
            {
                Debug.WriteLine($"Plugin directory does not exist: {pluginDirectory}");
                return;
            }
            foreach (string file in Directory.GetFiles(pluginDirectory, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    foreach (Type type in Assembly.LoadFrom(file).GetTypes()
                        .Where(t => typeof(Plugin).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))
                    {
                        ThirdPartyPlugins.Add(Activator.CreateInstance(type) as Plugin);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to load plugin from {file}: {ex.Message}");
                }
            }
        }
    }
}
