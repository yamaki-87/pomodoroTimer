using pomodoroTimer.DI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace pomodoroTimer.Model
{
    public class TimerModel
    {
        public TimeSpan Duration{ get; set; }
        public TimeSpan RemainingTime { get; set; }
        public double LimitTime{ get; set;}
        public DateTime StartTime {get;}

        private TimeSpan _breakTime;
        public TimeSpan? BreakTime
        {
            get
            {
                return _breakTime;
            }
            set
            {
                _breakTime = value == null ? BreakDuration : (TimeSpan)value;
            }
        }

        private static readonly TimeSpan BreakDuration = TimeSpan.FromMinutes(5);

        private SingletonDispatcherTimer _timer = SingletonDispatcherTimer.GetInstance;
        private MediaPlayer _player;
        
        enum TimerState
        {
            Start,
            Running,
            Stopped,
        }

        enum UserState
        {
            Studying,
            breaking,
        }
        
        private TimerState _state;
        private UserState _userState;

        public TimerModel(TimeSpan duration,TimeSpan? breakTime = null)
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            _player = new ();
            _player.Open(new Uri(@"E:\utils\PomodoroTimer\pomodoroTimer\Quiz-Results02-2.mp3"));

            _state = TimerState.Start;
            this.Duration = duration;
            BreakTime = breakTime;
            this.RemainingTime = duration;

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if(this.RemainingTime.TotalSeconds > 0)
            {
                this.RemainingTime = RemainingTime.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                if(_userState== UserState.Studying)
                {
                    _player.Play();
                    RemainingTime = (TimeSpan)BreakTime;
                    _userState = UserState.breaking;
                }
                else
                {
                    RemainingTime = Duration;
                    _userState = UserState.Studying;
                }
            }
        }

        public void Start()
        {
            _userState = UserState.Studying;

            if (_state == TimerState.Start)
            {
                _timer.Start();
                _state = TimerState.Running;
            }else if (_state == TimerState.Stopped)
            {
                _timer.Start();
                _state = TimerState.Running;
            }else if(_state == TimerState.Running)
            {
                _timer.Stop();
                _state = TimerState.Stopped;
            }
        }

        public void Stop()
        {
            if(_state == TimerState.Running)
            {
                _timer.Stop();
                _state= TimerState.Stopped;
            }
        }

        public void Reset()
        {
            Start();
            this.RemainingTime = this.Duration;
        }
    }
}
