using System;
using System.Runtime.InteropServices;

namespace IPC.Mailslots
{
    public class MailslotInfo
    {
        public MailslotInfo(uint maxMessageSize, uint nextMessageSize, uint messageCount, uint readTimeout)
        {
            MaxMessageSize = maxMessageSize;
            NextMessageSize = nextMessageSize;
            MessageCount = messageCount;
            ReadTimeout = readTimeout;
        }
        public uint MaxMessageSize { get; }
        public uint NextMessageSize { get; }
        public uint MessageCount { get; }
        public uint ReadTimeout { get; }
    }

    public class MailslotsServer
    {
        private readonly string _mailslotsPrefix = @"\\.\mailslot\";

        [DllImport("kernel32.dll")]
        extern private static IntPtr CreateMailslotA(string lpName, uint nMaxMessageSize, ulong lReadTimeout, IntPtr lpSecurityAttributes);

        [DllImport("kernel32.dll")]
        extern private static bool GetMailslotInfo(IntPtr hMailslot, out uint lpMaxMessageSize, out uint lpNextSize, out uint lpMessageCount, out uint lpReadTimeout);

        public IntPtr CreateMailslot(string name, uint readTimeout = 0, uint maxMessageSize = 424)
        {
            ObjectSecurity os = new ObjectSecurity();

            IntPtr securityAttrib = os.CreateSecurityAttribute();
            
            IntPtr handle = CreateMailslotA(_mailslotsPrefix + name, maxMessageSize, readTimeout, securityAttrib);
            Marshal.FreeHGlobal(securityAttrib);

            return handle;
        }

        public MailslotInfo GetMailslotInfo(IntPtr hMailslot)
        {
            uint maxMessageSize, nextSize, messageCount, readTimeout;  
            var succeeded = GetMailslotInfo(hMailslot, out maxMessageSize, out nextSize, out messageCount, out readTimeout);

            if(!succeeded)
                return null;

            return new MailslotInfo(maxMessageSize, nextSize, messageCount, readTimeout);
        }
    }
}
