using System.Windows;
using ActiveWindows.Win32;
using ActiveWindows.Common.Extensions;

namespace ActiveWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Func<WindowInformation[]> filter;
        public MainWindow() => InitializeComponent();

        private void WindowLoaded(object sender, RoutedEventArgs e) => UseNoFilter(sender, e);

        private void DisplayOpenWindows(object sender, RoutedEventArgs e) => 
            lstOpenWindows.ClearWindows().AddWindows(filter());

        private void UseWindowsFilter(object sender, RoutedEventArgs e) =>
            filter = () => WindowsApi.GetOpenWindows().Where(IsNotOsWindow).ToArray();

        private bool IsNotOsWindow(WindowInformation windowInformation) => !windowInformation.IsOsWindow;

        private void UseNoFilter(object sender, RoutedEventArgs e) =>
            filter = () => WindowsApi.GetOpenWindows();
    }
}