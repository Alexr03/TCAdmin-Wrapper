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

        public TCAdminClientConfiguration(string mySqlString, bool mySqlEncrypted, string applicationName, TCAdminSettings tcAdminSettings = null)
        {
            this.MySQLString = mySqlString;
            this.MySQLEncrypted = mySqlEncrypted;
            this.ApplicationName = applicationName;
            this.TcAdminSettings = tcAdminSettings ?? new TCAdminSettings();
        }
    }

    /// <summary>
    /// The sub class for TCAdmin to follow.
    /// </summary>
    public class TCAdminSettings
    {
        /// <summary>
        /// Enable the TCAdmin Cache.
        /// </summary>
        public bool EnableCache { get; }

        /// <summary>
        /// Enable debug mode for the application
        /// </summary>
        public bool Debug { get; }

        /// <summary>
        /// Print all SQL debug information.
        /// </summary>
        public bool DebugSql { get; }

        /// <summary>
        /// Print all traffic packets information.
        /// </summary>
        public bool DebugPackets { get; }

        /// <summary>
        /// Set the path to store TCAdmin logs.
        /// </summary>
        public string LogPath { get; }

        /// <summary>
        /// Set the path to store TCAdmin cache.
        /// </summary>
        public string CachePath { get; }

        public TCAdminSettings(bool enableCache = true, bool debug = false, bool debugSql = false, bool debugPackets = false,
            string logPath = "./Logs", string cachePath = "./Cache")
        {
            this.EnableCache = enableCache;
            this.Debug = debug;
            this.DebugSql = debugSql;
            this.DebugPackets = debugPackets;
            this.LogPath = logPath;
            this.CachePath = cachePath;
        }
    }
}
