using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrackzamClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Keylogger k;
        public MainWindow()
        {
            InitializeComponent();
            ActiveWindowLogger = new ActiveWindowLoggerClass(ActiveWindowLoggerTextBox);
            _audioRecorder = new AudioRecorder(this);
            k = new Keylogger();
            k.setPath(@"C:\\Test");
            k.Start();
            ActiveWindowLogger.StartLogging("C:\\Users\\Public\\ActiveWindowLogs");
        }

        void MainWindow_Closing(object sender, EventArgs args)
        {
            k.Stop();
            ActiveWindowLogger.StopLogging();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
            
        public ActiveWindowLoggerClass ActiveWindowLogger;
        private AudioRecorder _audioRecorder;
    }
}
