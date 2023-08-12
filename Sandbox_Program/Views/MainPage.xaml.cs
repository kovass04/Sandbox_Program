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
        //private readonly DataService _Services;
        private MainPageViewModel _viewModel; //TODO fix it
        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = new MainPageViewModel();
            DataContext = _viewModel;


            //_Services = new DataService();
            //_ = Loadss();
        }

        /*async Task Loadss()
        {
            PostModel postModel = JsonConvert.DeserializeObject<PostModel>(await _Services.PostCodeAsync("print 'Hello World'"));// write code 
            StatusModel statusModel = JsonConvert.DeserializeObject<StatusModel>(await _Services.GetStatusAsync(postModel.he_id));
            _ = _Services.GetResultAsync(statusModel.result.run_status.output);
        }*/
        private void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            RequestedTheme = Toggle.IsOn
                ? ElementTheme.Light
                : ElementTheme.Dark;
        }
    }
}
