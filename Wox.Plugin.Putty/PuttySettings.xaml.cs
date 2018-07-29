using System;
using System.Windows;
using System.Windows.Controls;

namespace Wox.Plugin.Putty
{
    /// <summary>
    /// Interaction logic for PuttySettings.xaml
    /// </summary>
    public partial class PuttySettings : UserControl
    {
        private Settings _settings;

        public PuttySettings(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindBooleanToCheckbox(AddPuttyExeInResults, () => _settings.AddPuttyExeToResults, (v) => _settings.AddPuttyExeToResults = v);
            BindBooleanToCheckbox(AlwaysStartsSessionMaximized, () => _settings.AlwaysStartsSessionMaximized, (v) => _settings.AlwaysStartsSessionMaximized = v);
        }

        private void BindBooleanToCheckbox(CheckBox checkBox, Func<bool> readBool, Action<bool> writeBool)
        {
            checkBox.IsChecked = readBool();

            checkBox.Checked += (o, ev) =>
            {
                writeBool(true);
                _settings.OnSettingsChanged?.Invoke(_settings);
            };

            checkBox.Unchecked += (o, ev) =>
            {
                writeBool(false);
                _settings.OnSettingsChanged?.Invoke(_settings);
            };
        }
    }
}