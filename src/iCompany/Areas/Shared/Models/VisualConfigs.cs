using System;
using System.Reflection;

namespace iCompany.Areas.Shared.Models
{
    public class VisualConfigs
    {
        public string Title { get; set; }
        public string Theme { get; set; }
        public Version Version { get; private set; }

        public VisualConfigs()
        {
            Version = typeof(Startup).GetTypeInfo().Assembly.GetName().Version;
        }
    }
}
