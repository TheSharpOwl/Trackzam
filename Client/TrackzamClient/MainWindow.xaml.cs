using System;
using System.Windows;
using System.Windows.Controls;

namespace TrackzamClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            _stackPanel = new StackPanel();
            InitializeComponent();
            _startSessionButton = InitializeStartButton(_stackPanel);
            textBox = InitializeTextBox(_stackPanel);
            AddChild(_stackPanel);
            _sessionManager = new SessionManager(this);
        }

        private Button InitializeStartButton(StackPanel stackPanel)
        {
            Button button = new Button();
            button.Content = "Start Recording Session";
            button.Click += Session_control;
            stackPanel.Children.Add(button);
            return button;
        }

        private TextBox InitializeTextBox(StackPanel stackPanel)
        {
            TextBox textBox = new TextBox();
            stackPanel.Children.Add(textBox);
            return textBox;
        }

        private void Session_control(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_sessionManager.IsSessionInProgress)
            {
                _sessionManager.EndSession();
                _startSessionButton.Content = "Start Recording";
            }
            else
            {
                _sessionManager.StartNewSession();
                _startSessionButton.Content = "Stop Recording";
            }

        }


        void MainWindow_Closing(object sender, EventArgs args)
        {
            _sessionManager.EndSession();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private StackPanel _stackPanel;
        public TextBox textBox;
        private Button _startSessionButton;
        private SessionManager _sessionManager;
    }
}
