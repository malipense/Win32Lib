using Xunit;
using IPC.Mailslots;
using System;

namespace IPCTest.MailslotsTest
{
    public class MailslotsServerTest
    {
        [Fact]
        public void ShouldCreateMailslot()
        {
            MailslotsServer server = new MailslotsServer();
            var p = server.CreateMailslot(@"taxes\bob");

            Assert.True(p != IntPtr.Zero);
        }

        [Fact]
        public void ShouldRetrieveMailslotInfo()
        {
            MailslotsServer server = new MailslotsServer();
            var p = server.CreateMailslot(@"taxes\bob", 15);

            var info = server.GetMailslotInfo(p);

            Assert.NotNull(info);
        }
    }
}