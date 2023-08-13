using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Sandbox_Program.Models;
using Sandbox_Program.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SendCommand { get; private set; }
        
        public MainPageViewModel() 
        {
            _Services = new DataService();
            SendCommand = new RelayCommand(ExecuteSendCommand);
            LanguageOptions = new ObservableCollection<LanguageOption>
                {
                    new LanguageOption { Code = "C", Name = "C" },
                    new LanguageOption { Code = "CPP14", Name = "C++14" },
                    new LanguageOption { Code = "CPP17", Name = "C++17" },
                    new LanguageOption { Code = "CLOJURE", Name = "Clojure" },
                    new LanguageOption { Code = "CSHARP", Name = "C#" },
                    new LanguageOption { Code = "GO", Name = "Go" },
                    new LanguageOption { Code = "HASKELL", Name = "Haskell" },
                    new LanguageOption { Code = "JAVA8", Name = "Java 8" },
                    new LanguageOption { Code = "JAVA14", Name = "Java 14" },
                    new LanguageOption { Code = "JAVASCRIPT_NODE", Name = "JavaScript(Nodejs)" },
                    new LanguageOption { Code = "KOTLIN", Name = "Kotlin" },
                    new LanguageOption { Code = "OBJECTIVEC", Name = "Objective C" },
                    new LanguageOption { Code = "PASCAL", Name = "Pascal" },
                    new LanguageOption { Code = "PERL", Name = "Perl" },
                    new LanguageOption { Code = "PHP", Name = "PHP" },
                    new LanguageOption { Code = "PYTHON", Name = "Python 2" },
                    new LanguageOption { Code = "PYTHON3", Name = "Python 3" },
                    new LanguageOption { Code = "PYTHON3_8", Name = "Python 3.8" },
                    new LanguageOption { Code = "R", Name = "R" },
                    new LanguageOption { Code = "RUBY", Name = "Ruby" },
                    new LanguageOption { Code = "RUST", Name = "Rust" },
                    new LanguageOption { Code = "SCALA", Name = "Scala" },
                    new LanguageOption { Code = "SWIFT", Name = "Swift" },
                    new LanguageOption { Code = "TYPESCRIPT", Name = "TypeScript" }
                };
        }
        private LanguageOption _selectedLanguageOption;
        public LanguageOption SelectedLanguageOption
        {
            get { return _selectedLanguageOption; }
            set { SetProperty(ref _selectedLanguageOption, value); }
        }

        public ObservableCollection<LanguageOption> LanguageOptions { get; set; }
        public class LanguageOption
        {
            public string Code { get; set; }
            public string Name { get; set; }
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

        public ObservableCollection<string> Options { get; set; }

        private string _selectedOption;
        public string SelectedOption
        {
            get { return _selectedOption; }
            set { SetProperty(ref _selectedOption, value); }
        }













        private async void ExecuteSendCommand()
        {
            PostModel postModel = JsonConvert.DeserializeObject<PostModel>(await _Services.PostCodeAsync(NoteText, SelectedOption)); //TODO else function
            StatusModel statusModel = JsonConvert.DeserializeObject<StatusModel>(await _Services.GetStatusAsync(postModel.he_id));
            OutputText = await _Services.GetResultAsync(statusModel.result.run_status.output);
            
        }

















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
