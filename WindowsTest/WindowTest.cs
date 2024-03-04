using System;
using Xunit;

namespace WindowsTest
{
    public class WindowTest
    {
        [Fact]
        public void ShouldCreateAWindow()
        {
            Windows.Window wnd = new Windows.Window();
            IntPtr handle = wnd.CreateAndDisplayWindow();

            Assert.True(handle != IntPtr.Zero);
        }

        [Fact]
        public void ShouldCreateUnicodeWindow()
        {
            Windows.Window wnd = new Windows.Window();
            IntPtr handle = wnd.CreateAndDisplayWindow();
            var encoding = wnd.GetWindowEncoding(handle);

            Assert.Equal(Windows.Encoding.Unicode, encoding);
        }
    }
}
