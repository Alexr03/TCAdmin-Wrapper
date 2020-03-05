namespace TCAdminWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Extensions;
    using TCAdmin.SDK;
    using TCAdmin.SDK.Objects;

    public class TcAdminClient
    {
        public static TcAdminClient Instance;

        public TCAdminClientConfiguration Configuration { get; }

        private User SystemUser { get; }

        private Server MasterServer { get; }

        /// <summary>
        /// The Client that connects to TCAdmin.
        /// </summary>
        /// <param name="configuration">The settings for TCAdmin to use.</param>
        public TcAdminClient(TCAdminClientConfiguration configuration)
        {
            //Store Configuration
            Configuration = configuration;

            //Initialise and set the MySQL information required for TCAdmin to function.
            Initialize();

            SystemUser = new User(2);
            MasterServer = GetMaster();
        }

        /// <summary>
        /// Initialise the Database, Providers, Debugs, Paths and Cache to be used.
        /// </summary>
        private void Initialize()
        {
            //Database Information
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Database.MySql.ConnectionString.Encrypted", Configuration.MySQLEncrypted.ToString());
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Database.MySql.ConnectionString", Configuration.MySQLString);
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Database.Provider", "TCAdmin.DatabaseProviders.MySql.MySqlConnectorManager,TCAdmin.DatabaseProviders.MySql");

            //Providers
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Log.Provider", "TCAdmin.LogProviders.Text.TextFileLogger,TCAdmin.LogProviders.Text");

            //Debugs
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Debug", Configuration.TcAdminSettings.Debug.ToString());
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.DebugSql", Configuration.TcAdminSettings.DebugSql.ToString());
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.DebugPackets", Configuration.TcAdminSettings.DebugPackets.ToString());

            //Paths
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.LogPath", Configuration.TcAdminSettings.LogPath);
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.CachePath", Configuration.TcAdminSettings.CachePath);

            //Cache
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.EnableCache", Configuration.TcAdminSettings.EnableCache.ToString());

            //Set the instance so we can access it elsewhere.
            Instance = this;
        }

        /// <summary>
        /// Gets the Master Server for the TCAdmin Instance.
        /// </summary>
        /// <returns><see cref="Server"/></returns>
        private Server GetMaster()
        {
            var servers = Server.GetServers();
            foreach (Server server in servers)
            {
                if (server.IsMaster)
                {
                    return MasterServer;
                }
            }

            return new Server(1);
        }
    }
}
