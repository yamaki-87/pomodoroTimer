using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pomodoroTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _timeLimit;
        private DateTime _startTime;
        private MediaElement _player;

        enum State
        {
            Start,
            Running,
            Stopped,
        }

        private State _state;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _player = new MediaElement();
            _player.Source = new Uri("Quiz-Results02-2.mp3", UriKind.Relative);
            _player.LoadedBehavior = MediaState.Manual;
            TimerButton.Click += TimerButton_Click;
            _state = State.Start;

        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(Directory.GetCurrentDirectory());
            if (_state == State.Start) {
                StartButton();
            }else if (_state == State.Running)
            {
                _timer.Stop();
                _state = State.Stopped;
                TimerButton.Content = "Re";
            }else if ( _state == State.Stopped)
            {
                _timer.Start();
                _state= State.Running;
                TimerButton.Content = "Stop";
            }
        }


        private void StartButton()
        {
            var limitMinutes = 0.1;
            _timeLimit = TimeSpan.FromMinutes(limitMinutes);
            _startTime = DateTime.Now;

            _timer.Start();
            _state = State.Running;
            TimerButton.Content = "Stop";
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var elapasedTime = DateTime.Now - _startTime;
            TimeDisplayLabel.Content = (_timeLimit - elapasedTime).ToString(@"hh\:mm\:ss");

            if (elapasedTime >= _timeLimit)
            {
                _timer.Stop();
                PlayAlertSound();
                MessageBox.Show("時間を超過しました。");
            }
        }

        private void PlayAlertSound()
        {
            _player.Play();
        }
    }
}