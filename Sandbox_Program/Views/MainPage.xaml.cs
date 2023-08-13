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
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainPageViewModel();//TODO fix it
        }
        private void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            RequestedTheme = Toggle.IsOn
                ? ElementTheme.Light
                : ElementTheme.Dark;
        }
    }
}
