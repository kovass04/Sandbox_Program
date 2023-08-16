using System;
using System.IO;
using Windows.UI.Xaml.Controls;
using Sandbox_Program.Services;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Sandbox_Program.Models;
using Sandbox_Program.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sandbox_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string ThemeKey = "AppTheme";
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
            if (localSettings.Values.TryGetValue(ThemeKey, out var themeValue) && Enum.TryParse(themeValue.ToString(), out ElementTheme theme))
            {
                RequestedTheme = theme;
            }
        }

        private void SaveThemeToSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[ThemeKey] = RequestedTheme.ToString();
        }
    }
}
