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
            AddPuttyExeInResults.IsChecked = _settings.AddPuttyExeToResults;

            AddPuttyExeInResults.Checked += (o, ev) =>
            {
                _settings.AddPuttyExeToResults = true;
                _settings.OnSettingsChanged?.Invoke(_settings);
            };

            AddPuttyExeInResults.Unchecked += (o, ev) =>
            {
                _settings.AddPuttyExeToResults = false;
                _settings.OnSettingsChanged?.Invoke(_settings);
            };
        }
    }
}