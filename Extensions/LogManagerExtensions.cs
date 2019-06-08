namespace TCAdminWrapper.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TCAdmin.Interfaces.Logging;
    using TCAdmin.SDK;

    public class LogManagerExtensions : LogManager
    {
        public static void Write(string message)
        {
            LogManager.Write(message, LogType.Console);
        }
    }
}
