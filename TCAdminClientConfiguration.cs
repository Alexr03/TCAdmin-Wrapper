using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAdminWrapper
{
    public class TCAdminClientConfiguration
    {
        public string MySQLString { get; }

        public bool MySQLEncrypted { get; }

        public string ApplicationName { get; }

        public TCAdminSettings TcAdminSettings { get; }
    }

    public class TCAdminSettings
    {
        public bool EnableCache { get; set; }

        public bool Debug { get; set; }

        public bool DebugSQL { get; set; }

        public bool DebugPackets { get; set; }

        public string LogPath { get; set; }

        public string CachePath { get; set; }
    }
}
