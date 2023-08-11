using System;
using System.IO;
using Windows.UI.Xaml.Controls;
using Sandbox_Program.Services;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Sandbox_Program.Models;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sandbox_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly DataService _Services;
        public MainPage()
        {
            this.InitializeComponent();
            _Services = new DataService();
            _ = Loadss();
        }

        async Task Loadss()
        {
            PostModel postModel = JsonConvert.DeserializeObject<PostModel>(await _Services.PostCodeAsync("print 'Hello World'"));// write code 
            StatusModel statusModel = JsonConvert.DeserializeObject<StatusModel>(await _Services.GetStatusAsync(postModel.he_id));
            _ = _Services.GetResultAsync(statusModel.result.run_status.output);
        }
    }
}
