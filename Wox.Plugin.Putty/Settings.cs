using System;

namespace Wox.Plugin.Putty
{
    public class Settings
    {
        public bool AddPuttyExeToResults { get; set; }
        public bool AlwaysStartsSessionMaximized { get; set; }

        public Action<Settings> OnSettingsChanged { get; set; }
    }
}