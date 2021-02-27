using System;
using System.Drawing;
using System.IO;
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

            _uiManager = new UIManager(_stackPanel);
            _sessionControlButton = _uiManager.AddButton("Start Recording Session", Session_control);
        }
        

        private void Session_control(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_sessionManager.IsSessionInProgress)
            {
                _sessionManager.EndSession();
                _uiManager.UpdateButtonText(_sessionControlButton, "Start Recording Session");
            }
            else
            {
                _sessionManager.StartNewSession();
                _uiManager.UpdateButtonText(_sessionControlButton, "Stop Recording Session");
            }
        }


        private void MainWindow_Closing(object sender, EventArgs args)
        {
            _sessionManager.EndSession();
        }

        private StackPanel _stackPanel;
        private UIManager _uiManager;
        private Button _sessionControlButton;
        private readonly SessionManager _sessionManager;
    }
}
