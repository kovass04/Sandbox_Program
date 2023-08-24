using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Sandbox_Program.Models
{
    public class ThemeSettingsModel
    {
        private const string _themeKey = "AppTheme";

        public ElementTheme LoadTheme()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(_themeKey, out var themeValue) && Enum.TryParse(themeValue.ToString(), out ElementTheme theme))
            {
                return theme;
            }
            return ElementTheme.Default;
        }

        public void SaveTheme(ElementTheme theme)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[_themeKey] = theme.ToString();
        }

    }
}
