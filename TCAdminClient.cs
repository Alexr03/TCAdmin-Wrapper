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

        private readonly string _mySqlString;
        private readonly bool _encrypted;

        private User SystemUser { get; }

        private Server MasterServer { get; }

        public TcAdminClient(TCAdminClientConfiguration configuration)
        {
            //Store Configuration
            Configuration = configuration;

            //Initialise and set the MySQL information required for TCAdmin to function.
            _mySqlString = configuration.MySQLString;
            _encrypted = configuration.MySQLEncrypted;
            Initialize();

            SystemUser = new User(2);
            MasterServer = GetMaster();
        }

        private void Initialize()
        {
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Database.MySql.ConnectionString.Encrypted", _encrypted.ToString());
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Database.MySql.ConnectionString", _mySqlString);
            Instance = this;
        }

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
