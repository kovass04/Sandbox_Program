using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Sandbox_Program.Models;
using Sandbox_Program.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;


namespace Sandbox_Program.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly DataService _Services;
        //TODO finish it          xaml code    IsOn="{Binding IsSwitchOn, Mode=TwoWay}  RequestedTheme="{Binding SelectedTheme}""
        /*private bool _isSwitchOn;
        public bool IsSwitchOn
        {
            get => _isSwitchOn;
            set
            {
                if (_isSwitchOn != value)
                {
                    _isSwitchOn = value;
                    OnPropertyChanged();
                }
            }
        }*/
        public ICommand SendCommand { get; private set; }
        
        public MainPageViewModel() 
        {
            _Services = new DataService();
            SendCommand = new RelayCommand(ExecuteSendCommand);
        }
        private string _noteText;
        public string NoteText
        {
            get { return _noteText; }
            set { SetProperty(ref _noteText, value); }
        }

        private string _outputText;
        public string OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }
        private async void ExecuteSendCommand()
        {
            //string code = NoteText;
            PostModel postModel = JsonConvert.DeserializeObject<PostModel>(await _Services.PostCodeAsync($"{NoteText}"));// write code 
            StatusModel statusModel = JsonConvert.DeserializeObject<StatusModel>(await _Services.GetStatusAsync(postModel.he_id));
            OutputText = await _Services.GetResultAsync(statusModel.result.run_status.output);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
