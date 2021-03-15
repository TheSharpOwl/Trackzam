using System;
using System.Windows;
using System.Windows.Controls;

namespace TrackzamClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///
    /// Starting point of the application, where everything is being initialized
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            _sessionManager = new SessionManager();
            _userLogin = new UserLogin();
            _userLogin.RetrieveLoginStatus();
            
            InitializeUI();
            
            ConfigManager.RetrieveConfigData(_curDirTextBlock);
        }

        private void InitializeUI()
        {
            _stackPanel = new StackPanel();
            UIManager.SetPanelSize(_stackPanel, Width, Height);
            AddChild(_stackPanel);
            
            UIManager.Initialize(_stackPanel, Width, Height);
            _sessionControlButton = UIManager.AddButton("Start Recording Session", Session_control);
            UIManager.AlignCenter(_sessionControlButton);
            UIManager.SetSize(_sessionControlButton, Width/2, Height/10);

            if (!_userLogin.IsLoggedIn)
            {
                _loginButton = UIManager.AddButton("Login", OpenLoginWindow);
                UIManager.AlignCenter(_loginButton);
                UIManager.SetSize(_loginButton, Width/4, Height/20);   
            }

            _curDirTextBlock = UIManager.AddTextBlock();
            UIManager.SetTextBlockSize(_curDirTextBlock, Width / 2, Height / 20);
            _curDirTextBlock.TextAlignment = TextAlignment.Center;
            _curDirTextBlock.TextTrimming = TextTrimming.CharacterEllipsis;

            _changeDirButton = UIManager.AddButton("Change storage directory", ChangeDirectory);
            UIManager.AlignCenter(_changeDirButton);
            UIManager.SetSize(_changeDirButton, Width / 4, Height / 20);
        }

        private void OpenLoginWindow(object sender, RoutedEventArgs routedEventArgs)
        {
            UIManager.OpenLoginWindow(TryLogin);
        }

        private void TryLogin((string login, string pass, string email) authData)
        {
            try
            {
                if (_userLogin.TryLogin(authData.login, authData.pass, authData.email))
                {
                    UIManager.CloseLoginWindow();
                    _stackPanel.Children.Remove(_loginButton);
                }
                else
                {
                    UIManager.ShowMessage("Incorrect login or password");
                }
            }
            catch (Exception e)
            {
                UIManager.ShowMessage(e.Message);
            }

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
                if (!_userLogin.IsLoggedIn)
                {
                    UIManager.ShowMessage("You must log in first!");
                    return;
                }
                _sessionManager.StartNewSession();
                UIManager.UpdateButtonText(_sessionControlButton, "Stop Recording Session");
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sessionManager.IsSessionInProgress) 
                _sessionManager.EndSession();
        }

        private void ChangeDirectory(object sender, RoutedEventArgs routedEventArgs)
        {
            ConfigManager.SetStorageDirectory();
        }

        private UserLogin _userLogin;
        
        private StackPanel _stackPanel;
        private Button _sessionControlButton;
        private Button _loginButton;
        private Button _changeDirButton;
        private TextBlock _curDirTextBlock;
        private readonly SessionManager _sessionManager;
    }
}