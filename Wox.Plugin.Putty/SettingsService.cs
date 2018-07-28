using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wox.Plugin.Putty
{
    public class SettingsService
    {
        public SettingsService()
        {
        }

        public Settings LoadSettings()
        {
            var settings = new Settings { AddPuttyExeToResults = true };
            try
            {
                using (var woxPuttySubKey = Registry.CurrentUser.CreateSubKey(@"Software\Wox.Plugin.Putty"))
                {
                    if (woxPuttySubKey != null)
                    {
                        var value = woxPuttySubKey.GetValue("AddPuttyExeToResults", true, RegistryValueOptions.None);
                        if (value is string && ((string)value) == "0")
                        {
                            settings.AddPuttyExeToResults = false;
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
                    }
                }
            }
            catch (Exception ex)
            {
                // Just ignore any exception.
            }
        }
    }
}