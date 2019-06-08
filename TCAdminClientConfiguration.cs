using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAdminWrapper
{
    /// <summary>
    /// The core configuration for the TCAdmin Client.
    /// </summary>
    public class TCAdminClientConfiguration
    {
        /// <summary>
        /// This is found in the TCAdmin.Monitor.exe.Config
        /// </summary>
        public string MySQLString { get; }

        public bool MySQLEncrypted { get; }

        /// <summary>
        /// The Name of this Client.
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// The settings for TCAdmin to follow.
        /// </summary>
        public TCAdminSettings TcAdminSettings { get; }
    }

    /// <summary>
    /// The sub class for TCAdmin to follow.
    /// </summary>
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
