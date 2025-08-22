using System.Diagnostics;

namespace ActiveWindows.Common.Extensions
{
    internal static class ProcessExtensions
    {
        internal static bool ProcessIsStartedFromWindowsDirectory(this Process process) =>
            process.MainModule.FileName.Contains("WINDOWS");
    }
}
