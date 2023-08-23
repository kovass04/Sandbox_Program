using Newtonsoft.Json;
using Sandbox_Program.Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sandbox_Program.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
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
        public BaseViewModel()
        {
            //TODO dangerous methods that should be prescribed
            Task.Run(async () => await InitializeLanguageOptionsAsync()).Wait();
            Task.Run(async () => await InitializeProblemsAsync()).Wait();
        }
        public ObservableCollection<Problem> Problems { get; set; }
        public ObservableCollection<LanguageOption> LanguageOptions { get; set; }
        private async Task InitializeProblemsAsync() => Problems =
            new ObservableCollection<Problem>(await DeserializeJsonAsync<List<Problem>>("Problems.json"));
        private async Task InitializeLanguageOptionsAsync() => LanguageOptions = 
            new ObservableCollection<LanguageOption>(await DeserializeJsonAsync<List<LanguageOption>>("LanguageOptions.json"));

        public async Task<T> DeserializeJsonAsync<T>(string fileName)
        {
            T data;

            try
            {
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Data/{fileName}"));
                using (Stream stream = await file.OpenStreamForReadAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    data = JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                data = default;
            }

            return data;
        }
    }
}
