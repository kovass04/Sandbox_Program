using System;
using Windows.UI.Xaml.Controls;
using Sandbox_Program.ViewModels;
using Windows.UI.Xaml;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sandbox_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string _themeKey = "AppTheme";
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainPageViewModel();
            LoadThemeFromSettings();
        }
        private void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            RequestedTheme = Toggle.IsOn ? ElementTheme.Light : ElementTheme.Dark;
            SaveThemeToSettings();
        }

        private void LoadThemeFromSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(_themeKey, out var themeValue) && Enum.TryParse(themeValue.ToString(), out ElementTheme theme))
            {
                RequestedTheme = theme;
            }
        }

        private void SaveThemeToSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[_themeKey] = RequestedTheme.ToString();
        }
    }
}
