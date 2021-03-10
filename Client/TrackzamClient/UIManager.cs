using System;
using System.Windows;
using System.Windows.Controls;

namespace TrackzamClient
{
    public static class UIManager
    {
        public static void Initialize(StackPanel stackPanel, double width, double height)
        {
            _stackPanel = stackPanel;
            _width = width;
            _height = height;
        }

        public static Button AddButton(string text, RoutedEventHandler handler)
        {
            Button button = new Button();
            button.Content = text;
            button.Click += handler;
            _stackPanel.Children.Add(button);
            return button;
        }

        public static void AlignCenter(Control contentControl)
        {
            contentControl.HorizontalAlignment = HorizontalAlignment.Center;
            contentControl.VerticalAlignment = VerticalAlignment.Center;
        }
        
        public static void AlignCenterPanel(Panel panel)
        {
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.VerticalAlignment = VerticalAlignment.Center;
        }

        public static void SetSize(Control contentControl, double width, double height)
        {
            contentControl.Width = width;
            contentControl.Height = height;
        }

        public static void SetPanelSize(Panel panel, double width, double height)
        {
            panel.Width = width;
            panel.Height = height;
        }
        
        public static void SetTextBlockSize(TextBlock textBlock, double width, double height)
        {
            textBlock.Width = width;
            textBlock.Height = height;
        }

        public static TextBlock NewTextBlock(string content, double width, double height)
        {
            TextBlock textBlock = new TextBlock();
            SetTextBlockSize(textBlock, width, height);
            textBlock.Text = content;
            
            return textBlock;
        }
        
        public static void UpdateButtonText(Button button, string newText)
        {
            button.Content = newText;
        }
        
        public static TextBox AddTextBox(RoutedEventHandler handler)
        {
            TextBox textBox = new TextBox();
            _stackPanel.Children.Add(textBox);
            return textBox;
        }

        public static void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        
        public static void OpenLoginWindow(Action<(string login, string pass)> eventHandler)
        {
            _loginWindow = OpenWindow("login", _width, _height);
            _loginStackPanel = new StackPanel();

            TextBlock loginText = NewTextBlock("Login:", _width / 4, _height / 30);

            TextBox loginInput = new TextBox();
            SetSize(loginInput, _width/4, _height/20);
            loginInput.Text = "Type Login Here";
            loginInput.GotFocus += (sender, args) =>
            {
                if (_loginFocus) return;
                loginInput.Text = "";
                _loginFocus = true;
            };
            
            TextBlock passText = NewTextBlock("Password:", _width / 4, _height / 30);
            
            TextBox passInput = new TextBox();
            SetSize(passInput, _width/4, _height/20);
            passInput.Text = "Type Password Here";
            passInput.GotFocus += (sender, args) => 
            { 
                if (_passFocus) return;
                passInput.Text = "";
                _passFocus = true;
            };
            
            Button loginButton = new Button();
            loginButton.Content = "Login";
            loginButton.Click += (sender, args) =>
            {
                eventHandler.Invoke((loginInput.Text, passInput.Text));
            };
            

            _loginStackPanel.Children.Add(loginText);
            _loginStackPanel.Children.Add(loginInput);
            _loginStackPanel.Children.Add(passText);
            _loginStackPanel.Children.Add(passInput);
            _loginStackPanel.Children.Add(loginButton);
            _loginWindow.Content = _loginStackPanel;

            _loginWindow.Closed += OnLoginWindowClose;
        }

        private static bool _loginFocus = false;
        private static bool _passFocus = false;

        private static Window OpenWindow(string name, double width, double height)
        {
            Window window = new Window();
            window.Title = name;
            window.Width = width;
            window.Height = height;
            
            HideMainWindow();
            
            window.Show();
            return window;
        }

        public static void CloseLoginWindow()
        {
            _loginWindow.Close();
            ShowMainWindow();
        }

        private static void HideMainWindow()
        {
            Application.Current.MainWindow.Focusable = false;
            Application.Current.MainWindow.Hide();
        }
        
        private static void ShowMainWindow()
        {
            Application.Current.MainWindow.Focusable = true;
            Application.Current.MainWindow.Show();
        }

        private static void OnLoginWindowClose(object? sender, EventArgs eventArgs)
        {
            ShowMainWindow();
        }
        
        private static StackPanel _stackPanel;
        private static StackPanel _loginStackPanel;
        private static Window _loginWindow;
        private static double _width;
        private static double _height;
    }
}