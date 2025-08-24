using System.Diagnostics;
using ActiveWindows.Common.Extensions;

namespace ActiveWindows.Win32
{
    public class WindowInformation
    {
        public string Caption { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public Process Process { get; set; } = null;
        public Rect WindowRect { get; set; }
        public bool IsWindowsUI => ClassName.Contains("Windows.UI");
        public bool IsOsWindow => Process.ProcessIsStartedFromWindowsDirectory() || IsWindowsUI;
    }
}
