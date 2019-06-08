namespace TCAdminWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TCAdmin.SDK.Objects;

    public class TcAdminClient
    {
        private readonly string _mySqlString;
        private readonly bool _encrypted;

        private User SystemUser { get; }

        private Server MasterServer { get; }

        public TcAdminClient(string mySqlString, bool encrypted)
        {
            //Initialise and set the MySQL information required for TCAdmin to function.
            _mySqlString = mySqlString;
            _encrypted = encrypted;
            Initialize();

            SystemUser = new User(2);
            MasterServer = GetMaster();
        }

        private void Initialize()
        {
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Database.MySql.ConnectionString.Encrypted", _encrypted.ToString());
            TCAdmin.SDK.Utility.AppSettings.Set("TCAdmin.Database.MySql.ConnectionString", _mySqlString);
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
