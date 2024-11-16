using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace pomodoroTimer.Config
{
    public class Config
    {
        public TimeSpan StudyDuration 
        {
            get => Properties.Settings.Default.StudyDuration;
            set
            {
                Properties.Settings.Default.StudyDuration = value;
                Properties.Settings.Default.Save();
            }
        }

        public TimeSpan BreakDuration
        {
            get => Properties.Settings.Default.BreakDuration;
            set
            {
                Properties.Settings.Default.BreakDuration = value;
                Properties.Settings.Default.Save() ;
            }
        }
        
        private static Config _instance = new Config();

        private Config() { }

        public static Config GetInstance => _instance;
    }
}
