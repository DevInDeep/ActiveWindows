using System.Text;
using System.Diagnostics;
using HWND = System.IntPtr;
using System.Runtime.InteropServices;

namespace ActiveWindows.Win32
{
    public static class WindowsApi
    {
        public static WindowInformation[] GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            List<WindowInformation> windowInformationList = new List<WindowInformation>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                Rect currentWindowRect = new Rect();
                GetWindowRect(hWnd, ref currentWindowRect);
                
                StringBuilder captionBuilder = new StringBuilder(length);
                GetWindowText(hWnd, captionBuilder, length + 1);
                StringBuilder classNameBuilder = new StringBuilder(1024);
                GetClassName(hWnd, classNameBuilder, classNameBuilder.Capacity);

                if (GetProcess(hWnd, out Process process))
                {

                    WindowInformation windowInformation = new WindowInformation()
                    {
                        Caption = captionBuilder.ToString(),
                        ClassName = classNameBuilder.ToString(),
                        WindowRect = currentWindowRect,
                        Process = process
                    };
                    windowInformationList.Add(windowInformation);
                }
                return true;

            }, 0);
            return windowInformationList.ToArray();
        }
        public static bool GetProcess(IntPtr hWnd, out Process process)
        {
            process = null;
            try
            {
                int processID = 0;
                GetWindowThreadProcessId(hWnd, out processID);
                process = Process.GetProcessById(processID);
                return true;
            }
            catch { return false; }
        }
        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();
        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);
        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
        [DllImport("USER32.DLL")]
        public static extern bool IsWindowVisible(HWND hWnd);
        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);
    }
}
