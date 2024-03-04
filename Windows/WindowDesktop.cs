using System.Runtime.InteropServices;

namespace Windows
{
    public class WindowDesktop
    {
        //System-wide desktop parameters
        private const uint SPI_GETDESKTOPWALLPAPER = 0x0073;


        [DllImport("User32.dll", SetLastError = true)]
        extern private static IntPtr GetDesktopWindow();

        [DllImport("User32.dll", SetLastError = true)]
        extern private static uint SystemParametersInfoA(uint uiAction, 
            uint uiParam,
            IntPtr pvParam,
            uint fWinIni);

        /// <summary>
        /// </summary>
        /// <returns>A handle to the desktop window</returns>
        public IntPtr GetDesktopWindowHandle()
        {
            IntPtr hDesktopWindow = GetDesktopWindow();
            if(hDesktopWindow != IntPtr.Zero)
                return hDesktopWindow;

            return IntPtr.Zero;
        }

        /// <summary>
        /// </summary>
        /// <returns>The current wallpaper absolute path</returns>
        public string GetDesktopWallpaperPath()
        {
            IntPtr p = Marshal.AllocHGlobal(128);
            uint succedeed = SystemParametersInfoA(SPI_GETDESKTOPWALLPAPER, 128, p, 0);

            string path = Marshal.PtrToStringAnsi(p);
            Marshal.FreeHGlobal(p);

            return path;
        }
    }
}