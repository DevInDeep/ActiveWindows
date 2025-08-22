using ActiveWindows.Win32;

namespace ActiveWindows
{
    internal class WindowManager
    {
        internal WindowInformation[] GetAllWindows()
        {
            List<WindowInformation> windows = new List<WindowInformation>();
            foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
            {
                IntPtr handle = window.Key;
                string title = window.Value;
                WindowInformation windowInformation = WindowList.winInfoGet(handle);
                windows.Add(windowInformation);
            }
            return windows.ToArray();
        }

        internal WindowInformation[] GetAllWindows(Func<WindowInformation, bool> filter) => 
            GetAllWindows().Where(filter).ToArray();
    }
}
