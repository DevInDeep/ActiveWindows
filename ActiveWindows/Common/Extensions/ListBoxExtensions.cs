using ActiveWindows.Win32;
using System.Windows.Controls;

namespace ActiveWindows.Common.Extensions
{
    internal static class ListBoxExtensions
    {
        internal static ListBox ClearWindows(this ListBox listBox)
        {
            listBox.ItemsSource = null;
            return listBox;
        }

        internal static void AddWindows(this ListBox listBox, params WindowInformation[] windowInfos)
        {
            listBox.ItemsSource = windowInfos;
            listBox.DisplayMemberPath = "Caption";
        }
    }
}
