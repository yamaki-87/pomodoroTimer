using pomodoroTimer.Commander;
using pomodoroTimer.Config;
using pomodoroTimer.DI;
using pomodoroTimer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        private TimerModel _timerModel;

        public event PropertyChangedEventHandler? PropertyChanged;
        public string RemainingDisplay => _timerModel.RemainingTime.ToString(@"hh\:mm\:ss");

        private SingletonDispatcherTimer _timer = SingletonDispatcherTimer.GetInstance;
        private Config.Config _config = Config.Config.GetInstance;

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand ConfigShowCommand { get; }
        
        public TimerViewModel()
        {
            _timerModel = new (_config.StudyDuration,_config.BreakDuration);
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            ResetCommand = new RelayCommand(Reset);
            ConfigShowCommand = new RelayCommand(ConfigWindowShow);

            _timer.Tick += Tick_OnPropertyChanged;
        }

        private void Start()
        {
            _timerModel.Start();
            OnPropertyChanged(nameof(RemainingDisplay));
        }
        private void Stop()
        {
            _timerModel.Stop();
            OnPropertyChanged(nameof(RemainingDisplay));
        }

        private void Reset()
        {
            _timerModel.Reset();
            OnPropertyChanged(nameof(RemainingDisplay));
        }


        private void Tick_OnPropertyChanged(object? o,EventArgs e)
        {
            OnPropertyChanged(nameof(RemainingDisplay));
        }

        private void ConfigWindowShow()
        {
            var configWindow = new ConfigWindow(_timerModel);
            configWindow.Show();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
