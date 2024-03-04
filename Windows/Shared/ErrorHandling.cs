using System.Runtime.InteropServices;

namespace Windows.Shared
{
    internal class ErrorHandling
    {
        //https://learn.microsoft.com/en-us/windows/win32/api/errhandlingapi/nf-errhandlingapi-getlasterror
        //https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint FormatMessage(uint dwFlags, 
            IntPtr lpSource,
            uint dwMessageId,
            uint dwLanguageId,
            IntPtr lpBuffer,
            uint nSize,
            IntPtr arguments);

        public static string GetErrorMessage()
        {
            uint code = GetLastError();
            IntPtr messageBufferPtr = Marshal.AllocHGlobal(128);

            uint returnValue = FormatMessage(0x0, IntPtr.Zero, 0x0, 0x0, messageBufferPtr, 0x0, IntPtr.Zero);

            string message = Marshal.PtrToStringAnsi(messageBufferPtr);
            Marshal.FreeHGlobal(messageBufferPtr);
            return message;
        }
    }
}
