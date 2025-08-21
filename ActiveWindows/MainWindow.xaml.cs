using ActiveWindows.Win32;
using System.Windows;

namespace ActiveWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void DisplayOpenWindows(object sender, RoutedEventArgs e)
        {
            lstOpenWindows.Items.Clear();
            
            foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
            {
                IntPtr handle = window.Key;
                string title = window.Value;
                WindowInformation windowInformation = WindowList.winInfoGet(handle);
                if (windowInformation.Process.MainModule.FileName.Contains("WINDOWS")) continue;
                if (windowInformation.Class.Contains("Windows.UI")) continue;
                lstOpenWindows.Items.Add($"{handle}: {title}");
            }
            
        }
    }
}