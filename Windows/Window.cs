using System.Runtime.InteropServices;
using Windows.Shared;

namespace Windows
{
    public class Window
    {
        //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowtexta
        //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongptra
        //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga

        [DllImport("User32.dll", SetLastError = true)]
        extern private static bool SetWindowTextA(IntPtr hWnd, string lpString);

        [DllImport("User32.dll", SetLastError = true)]
        extern private static long SetWindowLongPtrA(IntPtr hWnd, int nIndex, long dwNewLong); 
        
        [DllImport("User32.dll")]
        extern private static IntPtr FindWindow();

        [DllImport("User32.dll")]
        extern private static IntPtr CreateWindowExA(ulong dwExStyle, string lpClassName, string lpWindowName, ulong dwStyle,
            int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("User32.dll")]
        extern private static bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("User32.dll")]
        extern private static int IsWindowUnicode(IntPtr hWnd);

        public void SetWindowTitle(string title)
        {
            IntPtr hWnd = FindWindow();
            bool succeeded = SetWindowTextA(hWnd, title);

            if (!succeeded)
                ErrorHandling.GetErrorMessage();
        }

        public IntPtr CreateAndDisplayWindow()
        {
            IntPtr hwnd = CreateWindowExA(0x00000100L, "Message", "MyTestWindow", 0x00C00000L, 0, 0, 500, 500, IntPtr.Zero,
                IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            ShowWindow(hwnd, 10);
            return hwnd;
        }
        public Encoding GetWindowEncoding(IntPtr hWnd)
        {
            var n = IsWindowUnicode(hWnd);
            return (Encoding)n;
        }
    }

    public enum Encoding
    {
        ASCII = 0,
        Unicode = 1
    }
}
