using pomodoroTimer.Commander;
using pomodoroTimer.Config;
using pomodoroTimer.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Config.Config _config = Config.Config.GetInstance;
        public TimerModel TimerModel {  get; set; }
        public Action CloseAction { get; set; }
        public ICommand SaveCommand { get; }

        public ICommand CloseCommand { get; }

        public double StudyDuration {  get; set; }
        public double BreakDuration { get; set; }

        public ConfigViewModel() 
        {
            SaveCommand = new RelayCommand(SaveSetting);
            CloseCommand = new RelayCommand(HiddenWindow);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveSetting()
        {
            if (StudyDuration <= 0 && BreakDuration <= 0)
                return;

            _config.StudyDuration = TimeSpan.FromMinutes(StudyDuration);

            _config.BreakDuration = TimeSpan.FromMinutes(BreakDuration);

            TimerModel.BreakTime = _config.BreakDuration;
            TimerModel.RemainingTime = _config.StudyDuration;
            HiddenWindow();
        }
        
        private void HiddenWindow()
        {
            CloseAction();   
        }
    }
}
