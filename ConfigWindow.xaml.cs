using pomodoroTimer.Model;
using pomodoroTimer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pomodoroTimer
{
    /// <summary>
    /// ConfigWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private ConfigViewModel _viewModel = new();
        public ConfigWindow(TimerModel timerModel)
        {
            InitializeComponent();
            DataContext = _viewModel;
            if(DataContext is ConfigViewModel viewModel)
            {
                viewModel.CloseAction = Close;
                viewModel.TimerModel = timerModel;
            }
        }
    }
}
