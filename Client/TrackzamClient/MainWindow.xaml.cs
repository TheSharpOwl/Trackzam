using System;
using System.Windows;
using System.Windows.Controls;

namespace TrackzamClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeUI();
            _sessionManager = new SessionManager();
        }

        private void InitializeUI()
        {
            _stackPanel = new StackPanel();
            AddChild(_stackPanel);

            UIManager.Initialize(_stackPanel);
            _sessionControlButton = UIManager.AddButton("Start Recording Session", Session_control);
        }

        private void Session_control(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_sessionManager.IsSessionInProgress)
            {
                _sessionManager.EndSession();
                UIManager.UpdateButtonText(_sessionControlButton, "Start Recording Session");
            }
            else
            {
                _sessionManager.StartNewSession();
                UIManager.UpdateButtonText(_sessionControlButton, "Stop Recording Session");
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sessionManager.IsSessionInProgress) 
                _sessionManager.EndSession();
            
        }

        private StackPanel _stackPanel;
        private Button _sessionControlButton;
        private readonly SessionManager _sessionManager;
    }
}