using System;
using System.Windows;
using System.Windows.Controls;

namespace TrackzamClient
{
    /// <summary>
    /// A lot of methods for UI creation - buttons, windows, panels, text input fields
    /// </summary>
    public static class UIManager
    {
        public static void Initialize(StackPanel stackPanel, double width, double height)
        {
            _stackPanel = stackPanel;
            _width = width;
            _height = height;
        }
        
        /// <summary>
        /// Adds a button to private StackPanel and returns it
        /// </summary>
        public static Button AddButton(string text, RoutedEventHandler handler)
        {
            Button button = new Button();
            button.Content = text;
            button.Click += handler;
            _stackPanel.Children.Add(button);
            return button;
        }

        /// <summary>
        /// Aligns given element to center
        /// </summary>
        public static void AlignCenter(Control contentControl)
        {
            contentControl.HorizontalAlignment = HorizontalAlignment.Center;
            contentControl.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// Sets element size
        /// </summary>
        public static void SetSize(Control contentControl, double width, double height)
        {
            contentControl.Width = width;
            contentControl.Height = height;
        }
        
        /// <summary>
        /// Sets panel size (panel is not a usual element like button)
        /// </summary>
        public static void SetPanelSize(Panel panel, double width, double height)
        {
            panel.Width = width;
            panel.Height = height;
        }
        
        /// <summary>
        /// Sets TextBlock size
        /// </summary>
        public static void SetTextBlockSize(TextBlock textBlock, double width, double height)
        {
            textBlock.Width = width;
            textBlock.Height = height;
        }
 
        /// <summary>
        /// Creates and a new text block
        /// </summary>
        /// <returns> New TextBox </returns>
        public static TextBlock NewTextBlock(string content, double width, double height)
        {
            TextBlock textBlock = new TextBlock();
            SetTextBlockSize(textBlock, width, height);
            textBlock.Text = content;
            
            return textBlock;
        }
        
        /// <summary>
        /// Sets a given button's text
        /// </summary>
        public static void UpdateButtonText(Button button, string newText)
        {
            button.Content = newText;
        }

        /// <summary>
        /// Sets a given text block's text
        /// </summary>
        public static void UpdateTextBlockText(TextBlock textBlock, string newText)
        {
            textBlock.Text = newText;
        }
        
        /// <summary>
        /// Creates a new Text Block and attaches it to local StackPanel
        /// </summary>
        /// <returns> New attached TextBlock </returns>
        public static TextBlock AddTextBlock()
        {
            TextBlock textBlock = new TextBlock();
            _stackPanel.Children.Add(textBlock);
            return textBlock;
        }

        /// <summary>
        /// Shows a little window with message
        /// </summary>
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        
        /// <summary>
        ///  Initializes and opens a login window
        /// Adds all buttons and text input fields inside itself
        /// Adds all event handlers inside buttons' logic
        /// </summary>
        public static void OpenLoginWindow(Action<(string login, string pass, string email)> eventHandler)
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
            PasswordBox passInput = new PasswordBox();
            SetSize(passInput, _width/4, _height/20);
            //passText.PasswordChar(
            //passInput.Text = "Type Password Here";
            passInput.PasswordChar = '*';
            passInput.GotFocus += (sender, args) => 
            { 
                if (_passFocus) return;
                passInput.Password = "";
                _passFocus = true;
            };
            
            TextBlock emailText = NewTextBlock("Email:", _width / 4, _height / 30);
            
            TextBox emailInput = new TextBox();
            SetSize(emailInput, _width/4, _height/20);
            emailInput.Text = "Type Email Here";
            emailInput.GotFocus += (sender, args) => 
            { 
                if (_emailFocus) return;
                emailInput.Text = "";
                _emailFocus = true;
            };
            
            Button loginButton = new Button();
            loginButton.Content = "Login";
            loginButton.Click += (sender, args) =>
            {
                eventHandler.Invoke((loginInput.Text, passInput.Password, emailInput.Text));
            };
            

            _loginStackPanel.Children.Add(loginText);
            _loginStackPanel.Children.Add(loginInput);
            _loginStackPanel.Children.Add(passText);
            _loginStackPanel.Children.Add(passInput);
            _loginStackPanel.Children.Add(emailText);
            _loginStackPanel.Children.Add(emailInput);
            _loginStackPanel.Children.Add(loginButton);
            _loginWindow.Content = _loginStackPanel;

            _loginWindow.Closed += OnLoginWindowClose;
        }

        private static bool _loginFocus = false;
        private static bool _passFocus = false;
        private static bool _emailFocus = false;

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