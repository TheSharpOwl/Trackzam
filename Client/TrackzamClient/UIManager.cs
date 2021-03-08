using System.Windows;
using System.Windows.Controls;

namespace TrackzamClient
{
    public partial class UIManager
    {
        public UIManager(StackPanel stackPanel)
        {
            _stackPanel = stackPanel;
        }

        public Button AddButton(string text, RoutedEventHandler handler)
        {
            Button button = new Button();
            button.Content = text;
            button.Click += handler;
            _stackPanel.Children.Add(button);
            return button;
        }

        public void UpdateButtonText(Button button, string newText)
        {
            button.Content = newText;
        }
        
        private TextBox AddTextBox(RoutedEventHandler handler)
        {
            TextBox textBox = new TextBox();
            _stackPanel.Children.Add(textBox);
            return textBox;
        }
        
        private StackPanel _stackPanel;
    }
}