using System;
using Xunit;
using IPC;

namespace IPCTest
{
    public class ObjectSecurityTest
    {
        [Fact]
        public void ShouldCreateAccessControlList()
        {
            var objSec = new ObjectSecurity();
            
            var re = objSec.CreateSecurityDescriptor();

            Assert.True(re);
        }
        [Fact]
        public void ShouldAccountSID()
        {
            var objSec = new ObjectSecurity();

            objSec.GetAccountSID();

            Assert.True(true);
        }
        [Fact]
        public void ShouldConvertSidStringToSid()
        {
            var objSec = new ObjectSecurity();

            objSec.ConvertStringToSid();

            Assert.True(true);
        }
        [Fact]
        public void ShouldGetPolicyHandle()
        {
            var objSec = new ObjectSecurity();

            var p = objSec.GetPolicyHandle();

            Assert.True(true);
        }
        [Fact]
        public void ShouldListLogonSessions()
        {
            var objSec = new ObjectSecurity();

            objSec.ListLogonSessions();
            Assert.True(true);
        }
    }
}
