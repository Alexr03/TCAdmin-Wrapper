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
    }
}
