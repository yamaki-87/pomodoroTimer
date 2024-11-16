﻿using pomodoroTimer.ViewModel;
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
        private TimerViewModel _timerViewModel = new TimerViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _timerViewModel;
        }

    }
}