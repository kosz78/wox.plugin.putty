using Microsoft.Win32;
using System;

namespace Wox.Plugin.Putty
{
    public class SettingsService
    {
        public SettingsService()
        {
        }

        public Settings LoadSettings()
        {
            var settings = new Settings
            {
                AddPuttyExeToResults = true,
                AlwaysStartsSessionMaximized = false,
            };

            try
            {
                using (var woxPuttySubKey = Registry.CurrentUser.CreateSubKey(@"Software\Wox.Plugin.Putty"))
                {
                    if (woxPuttySubKey != null)
                    {
                        // Read with default to true
                        var value = woxPuttySubKey.GetValue("AddPuttyExeToResults", true, RegistryValueOptions.None);
                        if (value is string && ((string)value) == "0")
                        {
                            settings.AddPuttyExeToResults = false;
                        }

                        // Read with default to false
                        value = woxPuttySubKey.GetValue("AlwaysStartsSessionMaximized", true, RegistryValueOptions.None);
                        if (value is string && ((string)value) == "1")
                        {
                            settings.AlwaysStartsSessionMaximized = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Just ignore any exception.
            }
            settings.OnSettingsChanged = (s) => SaveSettings(s);
            SaveSettings(settings);
            return settings;
        }

        private void SaveSettings(Settings settings)
        {
            try
            {
                using (var woxPuttySubKey = Registry.CurrentUser.CreateSubKey(@"Software\Wox.Plugin.Putty"))
                {
                    if (woxPuttySubKey != null)
                    {
                        woxPuttySubKey.SetValue("AddPuttyExeToResults", settings.AddPuttyExeToResults ? "1" : "0", RegistryValueKind.String);
                        woxPuttySubKey.SetValue("AlwaysStartsSessionMaximized", settings.AlwaysStartsSessionMaximized ? "1" : "0", RegistryValueKind.String);
                    }
                }
            }
            catch (Exception)
            {
                // Just ignore any exception.
            }
        }
    }
}