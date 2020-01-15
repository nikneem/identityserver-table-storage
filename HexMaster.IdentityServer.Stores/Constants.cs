using System;
using System.Collections.Generic;
using System.Text;

namespace HexMaster.IdentityServer.Stores
{
    public class Constants
    {

        public const char SplitCharacter = ',';

    }

    public class TableNames
    {
        public const string Clients = "clients";
    }

    public class PartitionKeys
    {
        public const string Clients = "client";
    }
}
