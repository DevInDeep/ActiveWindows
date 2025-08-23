using ActiveWindows.Win32;

namespace ActiveWindows
{
    internal class WindowManager
    {
        internal WindowInformation2[] GetAllWindows()
        {
            List<WindowInformation2> windows = new List<WindowInformation2>();
            foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
            {
                IntPtr handle = window.Key;
                string title = window.Value;
                WindowInformation2 windowInformation = WindowList.winInfoGet(handle);
                windows.Add(windowInformation);
            }
            return windows.ToArray();
        }

        internal WindowInformation2[] GetAllWindows(Func<WindowInformation2, bool> filter) => 
            GetAllWindows().Where(filter).ToArray();
    }
}
