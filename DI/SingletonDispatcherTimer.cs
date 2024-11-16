using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace pomodoroTimer.DI
{
    public sealed class SingletonDispatcherTimer : DispatcherTimer 
    {
        private static readonly SingletonDispatcherTimer _instance = new();

        private SingletonDispatcherTimer() { }

        public static SingletonDispatcherTimer GetInstance=>_instance;
    }
}
