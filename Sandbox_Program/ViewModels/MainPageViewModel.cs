using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Sandbox_Program.Models;
using Sandbox_Program.Services;
using System.Windows.Input;


namespace Sandbox_Program.ViewModels
{
    /// <summary>
    /// View model for the main page of the application.
    /// </summary>
    public class MainPageViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly DataService _services;
        private string _outputText;
        private LanguageOption _selectedOption;
        private int _memoryLimit;
        private int _timeLimit;
        private Problem _selectedProblem;

        #endregion

        #region Public Properties

        /// <summary>
        /// Command for sending code for evaluation.
        /// </summary>
        public ICommand SendCommand { get; private set; }

        /// <summary>
        /// Text that displays the output or status of the evaluation.
        /// </summary>
        public string OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }

        /// <summary>
        /// The selected programming language option.
        /// </summary>
        public LanguageOption SelectedOption
        {
            get { return _selectedOption; }
            set { SetProperty(ref _selectedOption, value); }
        }

        /// <summary>
        /// The memory limit for code evaluation.
        /// </summary>
        public int MemoryLimit
        {
            get { return _memoryLimit; }
            set { SetProperty(ref _memoryLimit, value); }
        }

        /// <summary>
        /// The time limit for code evaluation.
        /// </summary>
        public int TimeLimit
        {
            get { return _timeLimit; }
            set { SetProperty(ref _timeLimit, value); }
        }

        /// <summary>
        /// The selected problem for which the code is being evaluated.
        /// </summary>
        public Problem SelectedProblem
        {
            get { return _selectedProblem; }
            set { SetProperty(ref _selectedProblem, value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageViewModel()
        {
            _services = new DataService();
            SendCommand = new RelayCommand(ExecuteSendCommand);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes the command to send the code for evaluation.
        /// </summary>
        private async void ExecuteSendCommand()
        {
            if (SelectedOption == null || string.IsNullOrWhiteSpace(SelectedOption.BaseCode) ||
                string.IsNullOrWhiteSpace(SelectedOption.Code) || MemoryLimit <= 0 || TimeLimit <= 0 ||
                SelectedProblem == null)
            {
                // Display an error message or handle invalid input
                OutputText = "Error one of the parameters is empty";
                return;
            }

            PostModel postModel = JsonConvert.DeserializeObject<PostModel>(await _services.PostCodeAsync(
                SelectedOption.BaseCode, SelectedOption.Code, MemoryLimit, TimeLimit, SelectedProblem.SampleInput));
            OutputText = postModel.result.compile_status;
            
            StatusModel statusModel = JsonConvert.DeserializeObject<StatusModel>(await _services.GetStatusAsync(postModel.he_id));
            OutputText = statusModel.result.compile_status;
            string result = await _services.GetResultAsync(statusModel.result.run_status.output);

            //TODO Complete the processing of the result

            if (result == SelectedProblem.SampleOutput + "\n") 
            {
                OutputText = "Result \n" + result;
            }
            else
            {
                if (statusModel.request_status.code == "REQUEST_COMPLETED" || statusModel.request_status.code == "CODE_COMPILED") 
                {
                    OutputText = "The answer is not correct or you code is bad\nOutput : " + result;
                }
                else
                {
                    OutputText = statusModel.request_status.code;
                }
            }
        }
        #endregion
    }
}
