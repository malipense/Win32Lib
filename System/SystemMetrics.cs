using System.Runtime.InteropServices;

namespace SystemElements
{
    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics
    public struct ScreenSize
    {
        public int x;
        public int y;
    }
    public class SystemMetrics
    {
        [DllImport("User32.dll", SetLastError = false)]
        extern private static int GetSystemMetrics(int nIndex);

        public ScreenSize GetScreenSize() 
        {
            int width = GetSystemMetrics((int)Metrics.SM_CXSCREEN);
            int height = GetSystemMetrics((int)Metrics.SM_CYSCREEN);

            ScreenSize screenSize = new ScreenSize();
            screenSize.x = width;
            screenSize.y = height;

            return screenSize;
        }
        public int GetBootUpType() 
        {
            int bootUpType = GetSystemMetrics((int)Metrics.SM_CLEANBOOT);
            return bootUpType;
        }
        public int GetDisplayMonitors()
        {
            int numberOfVisibleDisplays = GetSystemMetrics((int)Metrics.SM_CMONITORS);
            return numberOfVisibleDisplays;
        }
    }
}