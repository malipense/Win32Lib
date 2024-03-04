using Xunit;
using Windows;
using System;

namespace WindowsTest
{
    public class WindowDesktopTest
    {
        [Fact]
        public void ShouldGetDesktopWindowHandle()
        {
            Windows.WindowDesktop wnd = new Windows.WindowDesktop();
            IntPtr handle = wnd.GetDesktopWindowHandle();
            
            Assert.NotEqual(IntPtr.Zero, handle);
        }

        [Fact]
        public void ShouldGetDesktopWallpaperPath()
        {
            Windows.WindowDesktop wnd = new Windows.WindowDesktop();
            string path = wnd.GetDesktopWallpaperPath();
            
            Assert.False(string.IsNullOrEmpty(path));
        }
    }
}