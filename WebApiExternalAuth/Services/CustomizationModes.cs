using System;

namespace WebApiExternalAuth.Services
{
    [Flags]
    public enum CustomizationModes
    {
        None = 0,
        General = 0x1,
        TwoFactorsAuthentication = 0x2,
    }
}