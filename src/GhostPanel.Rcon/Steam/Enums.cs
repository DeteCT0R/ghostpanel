using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Steam
{
    public enum ServerEnvironment
    {
        Linux = 0x6C,
        Windows = 0x77,
        Mac = 0x6F
    }

    public enum ServerType
    {
        Dedicated = 0x64,
        NonDedicated = 0x6C,
        SourceTV = 0x70
    }

    public enum ServerVAC
    {
        Unsecured = 0x0,
        Secured = 0x1
    }

    public enum ServerVisibility
    {
        Public = 0x0,
        Private = 0x1
    }

    // SERVERDATA_AUTH_RESPONSE and SERVERDATA_EXECCOMMAND are both 2
    public enum PacketType
    {
        // SERVERDATA_RESPONSE_VALUE
        Response = 0,

        // SERVERDATA_AUTH_RESPONSE
        AuthResponse = 2,

        // SERVERDATA_EXECCOMMAND
        ExecCommand = 2,

        // SERVERDATA_AUTH
        Auth = 3
    }
}
