using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IPC
{
    //for objects created by CreateFile, CreatePipe, CreateProcess, RegCreateKeyEx, RegSaveKeyEx
    public struct ACCESS_ALLOWED_ACE
    {
        public ACE_HEADER header;
        public ACCESS_MASK mask;
        public uint sidStart;
    }

    public struct ACE_HEADER
    {
        public byte aceType;
        public byte aceFlags;
        public int aceSize;
    }
    public struct SECURITY_ATTRIBUTES
    {
        public uint nLength;
        public IntPtr lpSecurityDescriptor;
        public bool bInheritHandle;
    }
    public struct SECURITY_DESCRIPTOR
    {
        public byte Revision;
        public byte Sbz1;
        public ushort Control;
        public IntPtr Owner;
        public IntPtr Group;
        public IntPtr Sacl;
        public IntPtr Dacl;
    }
    public struct ACL
    {
        public byte AclRevision;
        public byte Sbz1;
        public short AclSize;
        public short AceCount;
        public short Sbz2;
    }
    //ACE
    
    public enum ACCESS_MASK : ulong
    {
        DELETE          =  (0x00010000L),
        READ_CONTROL    =  (0x00020000L),
        WRITE_DAC       =  (0x00040000L),
        WRITE_OWNER     =  (0x00080000L),
        SYNCHRONIZE     =  (0x00100000L),

        POLICY_LOOKUP_NAMES = (0x00000800L)
    }

    public enum ACCESS_MODE
    {
        NOT_USED_ACCESS = 0,
        GRANT_ACCESS,
        SET_ACCESS,
        DENY_ACCESS,
        REVOKE_ACCESS,
        SET_AUDIT_SUCCESS,
        SET_AUDIT_FAILURE
    }

    public enum INHERITANCE
    {
        NO_INHERITANCE                      =   0x0,
        SUB_OBJECTS_ONLY_INHERIT            =   0x1,
        SUB_CONTAINERS_ONLY_INHERIT         =   0x2,
        SUB_CONTAINERS_AND_OBJECTS_INHERIT  =   0x3,
        INHERIT_NO_PROPAGATE                =   0x4,
        INHERIT_ONLY                        =   0x8
    }

    public enum MULTIPLE_TRUSTEE_OPERATION
    {
        NO_MULTIPLE_TRUSTEE,
        TRUSTEE_IS_IMPERSONATE,
    }

    public enum TRUSTEE_FORM
    {
        TRUSTEE_IS_SID,
        TRUSTEE_IS_NAME,
        TRUSTEE_BAD_FORM,
        TRUSTEE_IS_OBJECTS_AND_SID,
        TRUSTEE_IS_OBJECTS_AND_NAME
    }

    public enum TRUSTEE_TYPE
    {
        TRUSTEE_IS_UNKNOWN,
        TRUSTEE_IS_USER,
        TRUSTEE_IS_GROUP,
        TRUSTEE_IS_DOMAIN,
        TRUSTEE_IS_ALIAS,
        TRUSTEE_IS_WELL_KNOWN_GROUP,
        TRUSTEE_IS_DELETED,
        TRUSTEE_IS_INVALID,
        TRUSTEE_IS_COMPUTER
    }
    public struct EXPLICT_ACCESSA
    {
        public int grfAccessPermissions;
        public int grfAccessMode;
        public int grfInheritanceS;
        public TRUSTEE trustee;
    }

    public struct TRUSTEE
    {
        public IntPtr pMultipleTrustee;
        public int MultipleTrusteeOperation;
        public int TrusteeForm;
        public int TrusteeType;
        public IntPtr ptstrName;
    }

    public struct SID
    {
        public byte Revision;
        public byte SubAuthorityCount;
        public SID_IDENTIFIER_AUTHORITY IdentifierAuthority;
        public ulong[] SubAuthority = new ulong[1];
    }

    public struct SID_IDENTIFIER_AUTHORITY
    {
        public byte[] Value = new byte[6];
    }

    public struct LSA_UNICODE_STRING
    {
        public ushort Length;
        public ushort MaximumLength;
        public IntPtr Buffer;
    }

    public struct LSA_OBJECT_ATTRIBUTES
    {
        public ulong Length;
        public IntPtr RootDirectory;
        public IntPtr ObjectName;
        public ulong Attributes;
        public IntPtr SecurityDescriptor;
        public IntPtr SecurityQualityOfService;
    }

    public struct LUID //Local Unique Indentifier
    {
        public ulong LowPart;
        public long HighPart;
    }

    public struct LARGE_INTEGER
    {
        public ulong LowPart;
        public long HighPart;
        public long QuadPart;
    }
    public struct LSA_LAST_INTER_LOGON_INFO
    {
        LARGE_INTEGER LastSuccessfulLogon;
        LARGE_INTEGER LastFailedLogon;
        long FailedAttemptCountSinceLastSuccessfulLogon;
    }
    
    public struct SECURITY_LOGON_SESSION_DATA
    {
        public ulong Size;
        public LUID LogonId;
        public LSA_UNICODE_STRING UserName;
        public LSA_UNICODE_STRING LogonDomain;
        public LSA_UNICODE_STRING AuthenticationPackage;
        public ulong LogonType;
        public ulong Session;
        public IntPtr Sid;
        public LARGE_INTEGER LogonTime;
        public LSA_UNICODE_STRING LogonServer;
        public LSA_UNICODE_STRING DnsDomainName;
        public LSA_UNICODE_STRING Upn;
        public ulong UserFlags;
        public LSA_LAST_INTER_LOGON_INFO LastLogonInfo;
        public LSA_UNICODE_STRING LogonScript;
        public LSA_UNICODE_STRING ProfilePath;
        public LSA_UNICODE_STRING HomeDirectory;
        public LSA_UNICODE_STRING HomeDirectoryDrive;
        public LARGE_INTEGER LogoffTime;
        public LARGE_INTEGER KickOffTime;
        public LARGE_INTEGER PasswordLastSet;
        public LARGE_INTEGER PasswordCanChange;
        public LARGE_INTEGER PasswordMustChange;
    }

    public class ObjectSecurity
    {
        private readonly string _trustee_name = "DESKTOP-LL0MLVU/eduar";
        private readonly string _sid = "S-1-5-32-544";

        [DllImport("Advapi32.dll")]
        extern private static bool InitializeSecurityDescriptor(out IntPtr pSecurityDescriptor, uint dwRevision);

        [DllImport("Advapi32.dll")]
        extern private static bool GetSecurityDescriptorControl(IntPtr pSecurityDescriptor, out IntPtr pControl, out IntPtr lpdwRevision);

        [DllImport("Advapi32.dll")]
        extern private static uint SetEntriesInAclA(ulong cCountOfExplicitEntries, IntPtr pListOfExplicitEntries, IntPtr oldAcl, ref IntPtr newAcl);
        
        [DllImport("Advapi32.dll", SetLastError = true)]
        extern private static bool LookupAccountName(IntPtr lpSystemName, IntPtr lpAccountName, IntPtr Sid, IntPtr cbSid, IntPtr ReferencedDomainName, IntPtr cchReferencedDomainName, IntPtr peUse);

        [DllImport("Advapi32.dll", SetLastError = true)]
        extern private static int ConvertStringSidToSidA(IntPtr StringSid, ref IntPtr Sid);

        [DllImport("kernel32.dll")]
        extern private static IntPtr LocalFree(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        [DllImport("Advapi32.dll", SetLastError = true)]
        private static extern ulong LsaOpenPolicy(IntPtr SystemName, IntPtr ObjectAttributes, ulong DesiredAccess, ref IntPtr PolicyHandle);

        [DllImport("Advapi32.dll", SetLastError = true)]
        private static extern ulong LsaClose(IntPtr ObjectHandle);

        [DllImport("Secur32.dll", SetLastError = true)]
        extern private static ulong LsaEnumerateLogonSessions(IntPtr LogonSessionCount, ref IntPtr LogonSessionList);

        [DllImport("Secur32.dll", SetLastError = true)]
        extern private static ulong LsaFreeReturnBuffer(IntPtr Buffer);

        [DllImport("Secur32.dll", SetLastError = true)]
        extern private static ulong LsaGetLogonSessionData(IntPtr LogonId, ref IntPtr ppLogonSessionData);

        public void ListLogonSessions()
        {
            IntPtr pSessionCount = Marshal.AllocHGlobal(64);
            IntPtr pLUID = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LUID)));
            
            var r = LsaEnumerateLogonSessions(pSessionCount, ref pLUID);
            LUID lUID = new LUID();

            if (r == 0)
            {
                var count = Marshal.ReadInt64(pSessionCount);
                lUID = Marshal.PtrToStructure<LUID>(pLUID);


                SECURITY_LOGON_SESSION_DATA session = new SECURITY_LOGON_SESSION_DATA();
                IntPtr pSession = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SECURITY_LOGON_SESSION_DATA)));

                var x = LsaGetLogonSessionData(pLUID, ref pSession);
                if(x == 0)
                {
                    session = Marshal.PtrToStructure<SECURITY_LOGON_SESSION_DATA>(pLUID);

                    byte[] cvt = new byte[session.DnsDomainName.Length];
                    Marshal.Copy(session.DnsDomainName.Buffer, cvt, 0, session.DnsDomainName.Length - 1);
                    string text = Encoding.ASCII.GetString(cvt);

                    LsaFreeReturnBuffer(pSession);
                }
            }

            LsaFreeReturnBuffer(pLUID);
            Marshal.FreeHGlobal(pLUID);
            Marshal.FreeHGlobal(pSessionCount);

        }

        public IntPtr GetPolicyHandle()
        {
            IntPtr handle = IntPtr.Zero;
            IntPtr hObjectAttrib = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LSA_OBJECT_ATTRIBUTES)));

            LSA_OBJECT_ATTRIBUTES objAttrib = new LSA_OBJECT_ATTRIBUTES();
            Marshal.StructureToPtr(objAttrib, hObjectAttrib, true);

            LsaOpenPolicy(IntPtr.Zero, hObjectAttrib, (ulong)ACCESS_MASK.POLICY_LOOKUP_NAMES, ref handle);            
            LsaClose(handle);
            Marshal.FreeHGlobal(hObjectAttrib);

            return handle;
        }


        public void ConvertStringToSid()
        {
            //https://limbioliong.wordpress.com/2012/04/17/how-to-implement-pointer-to-pointer-in-c-without-using-unsafe-code/

            IntPtr pStringSid = Marshal.StringToHGlobalAuto(_sid);
            IntPtr pSid = IntPtr.Zero;

            var succeeded = ConvertStringSidToSidA(pStringSid, ref pSid);

            if (succeeded == 0)
            {
                //failed
                var code = GetLastError();
                Marshal.FreeHGlobal(pStringSid);
                Marshal.FreeHGlobal(pSid);
            }

            else
            {
                SID sid = new SID();
                Marshal.PtrToStructure(pSid, sid);
            }
        }

        public void GetAccountSID()
        {
            IntPtr pUserAccount = Marshal.StringToHGlobalUni(_trustee_name);
            IntPtr pSid = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SID)));
            IntPtr pCbSid = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            IntPtr pReferencedDomainName = Marshal.AllocHGlobal(32);
            IntPtr pCchReferencedDomainName = Marshal.AllocHGlobal(32);
            IntPtr pPeUse = Marshal.AllocHGlobal(4);

            
            var succeeded = LookupAccountName(IntPtr.Zero, pUserAccount, pSid, pCbSid, pReferencedDomainName,
                pCchReferencedDomainName, pPeUse);

            if(!succeeded)
            {
                var code = GetLastError();
                Marshal.FreeHGlobal(pUserAccount);
                Marshal.FreeHGlobal(pSid);
                Marshal.FreeHGlobal(pCbSid);
                Marshal.FreeHGlobal(pReferencedDomainName);
                Marshal.FreeHGlobal(pCchReferencedDomainName);
                Marshal.FreeHGlobal(pPeUse);
            }

            SID Sid = Marshal.PtrToStructure<SID>(pSid);
            ulong cbSid = (ulong)Marshal.ReadInt64(pCbSid);
            string referencedDomainName = Marshal.PtrToStringUni(pReferencedDomainName);
            ulong cchReferencedDomainName = (ulong)Marshal.ReadInt64(pCchReferencedDomainName);
            int SID_NAME_USE = Marshal.ReadInt32(pPeUse);

            Marshal.FreeHGlobal(pUserAccount);
            Marshal.FreeHGlobal(pSid);
            Marshal.FreeHGlobal(pCbSid);
            Marshal.FreeHGlobal(pReferencedDomainName);
            Marshal.FreeHGlobal(pCchReferencedDomainName);
            Marshal.FreeHGlobal(pPeUse);
        }

        public bool CreateSecurityDescriptor()
        {
            IntPtr pSid = Marshal.StringToHGlobalUni(_sid);

            EXPLICT_ACCESSA explictAccess = new EXPLICT_ACCESSA();
            explictAccess.grfAccessPermissions = (int)ACCESS_MASK.READ_CONTROL;
            explictAccess.grfAccessMode = (int)ACCESS_MODE.GRANT_ACCESS;
            explictAccess.grfInheritanceS = (int)INHERITANCE.NO_INHERITANCE;
            explictAccess.trustee.pMultipleTrustee = IntPtr.Zero;
            explictAccess.trustee.MultipleTrusteeOperation = (int)MULTIPLE_TRUSTEE_OPERATION.NO_MULTIPLE_TRUSTEE;
            explictAccess.trustee.TrusteeForm = (int)TRUSTEE_FORM.TRUSTEE_IS_SID;
            explictAccess.trustee.TrusteeType = (int)TRUSTEE_TYPE.TRUSTEE_IS_USER;
            explictAccess.trustee.ptstrName = pSid;

            IntPtr pExplictAccess = Marshal.AllocHGlobal(Marshal.SizeOf(explictAccess));
            Marshal.StructureToPtr(explictAccess, pExplictAccess, true);


            IntPtr pNewAcl = IntPtr.Zero;
            
            var succeeded = SetEntriesInAclA(1, pExplictAccess, IntPtr.Zero, ref pNewAcl);

            if(succeeded == 0)
            {
                
            }

            IntPtr re = LocalFree(pNewAcl);
            if (re != IntPtr.Zero)
            {

            }

            Marshal.FreeHGlobal(re);
            Marshal.FreeHGlobal(pNewAcl);
            Marshal.FreeHGlobal(pExplictAccess);
            Marshal.FreeHGlobal(pSid);

            return true;
        }












        public IntPtr CreateSecurityAttribute()
        {
            SECURITY_ATTRIBUTES attrib = new SECURITY_ATTRIBUTES();
            int size = Marshal.SizeOf(typeof(SECURITY_ATTRIBUTES));

            attrib.nLength = (uint)size;
            attrib.bInheritHandle = false;
            attrib.lpSecurityDescriptor = IntPtr.Zero;

            IntPtr attribPointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(attrib, attribPointer, true);

            return attribPointer;
        }
        public IntPtr Stuff()
        {
            var structSize = Marshal.SizeOf(typeof(SECURITY_DESCRIPTOR));
            IntPtr pSecDescriptor = Marshal.AllocHGlobal(structSize);
            SECURITY_DESCRIPTOR secDescriptor = new SECURITY_DESCRIPTOR();

            Marshal.StructureToPtr(secDescriptor, pSecDescriptor, true);

            var s = GetSecurityDescriptorControl(pSecDescriptor, out IntPtr pControl, out IntPtr lpdwRevision);

            secDescriptor.Revision = (byte)Marshal.ReadByte(lpdwRevision);
            secDescriptor.Control = (ushort)Marshal.ReadInt16(pControl);

            Marshal.FreeHGlobal(pControl);
            Marshal.FreeHGlobal(lpdwRevision);
            Marshal.FreeHGlobal(pSecDescriptor);

            return IntPtr.Zero;
        }
        
    }
}
