using System;

namespace KeeperSecurity.Authentication
{
    [Flags]
    internal enum SessionTokenRestriction
    {
        AccountRecovery = 1 << 0,
        ShareAccount = 1 << 1,
        AcceptInvite = 1 << 2,
        AccountExpired = 1 << 3,
    }
}
