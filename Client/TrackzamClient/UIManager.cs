using System.Windows;
using System.Windows.Controls;

namespace TrackzamClient
{
    public static class UIManager
    {
        public static void Initialize(StackPanel stackPanel)
        {
            _stackPanel = stackPanel;
        }

        public static Button AddButton(string text, RoutedEventHandler handler)
        {
            Button button = new Button();
            button.Content = text;
            button.Click += handler;
            _stackPanel.Children.Add(button);
            return button;
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
        
        
        private static StackPanel _stackPanel;
    }
}