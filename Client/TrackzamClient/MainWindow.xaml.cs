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
            InitializeStartButton();
            //ActiveWindowLogger = new ActiveWindowLoggerClass(ActiveWindowLoggerTextBox);
            _audioRecorder = new AudioRecorder(this);
            k = new Keylogger();
            k.SetPath(@"C:\\Test");
            k.Start();

            ActiveWindowLogger.StartLogging("C:\\Users\\Public\\ActiveWindowLogs");

            _sessionManager = new SessionManager();
        }

        private void InitializeStartButton()
        {
            var sp = new StackPanel();
            startSessionButton = new Button();
            startSessionButton.Content = "Start Recording Session";
            startSessionButton.Click += Session_control;
            sp.Children.Add(startSessionButton);
            this.AddChild(sp);
        }

        private void Session_control(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_sessionManager.IsSessionInProgress)
            {
                _sessionManager.EndSession();
                startSessionButton.Content = "Start Recording";
            }
            else
            {
                _sessionManager.StartNewSession(ActiveWindowLogger, k, _audioRecorder);
                startSessionButton.Content = "Stop Recording";
            }

        }

        void MainWindow_Closing(object sender, EventArgs args)
        {
            k.Stop();
            ActiveWindowLogger.StopLogging();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private Button startSessionButton;
        public ActiveWindowLoggerClass ActiveWindowLogger;
        private AudioRecorder _audioRecorder;
        private SessionManager _sessionManager;
    }
}
