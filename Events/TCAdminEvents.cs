using TCAdminWrapper.Extensions;

namespace TCAdminWrapper.Events
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCAdmin.SDK.Objects;
    
    using Service = TCAdmin.GameHosting.SDK.Objects.Service;

    public class TcAdminEvents
    {
        public TcAdminEvents()
        {
            _snapshotTasksIds = new List<int>();

            Task.Run(async () =>
            {
                while (true)
                {
                    RunJob();
                    await Task.Delay(2000);
                }
            });

            Task.Run(async () =>
            {
                while (true)
                {
                    _snapshotTasksIds.Clear();
                    await Task.Delay(300000);
                }
            });
        }
        
        private void RunJob()
        {
            RunServicesJob();
            RunServersJob();
            
            RunTasksJob();
        }

        private void RunServicesJob()
        {
            var services = Service.GetServices().ToServices();
            foreach (var service in services)
            {
                var createdDate = TCAdmin.SDK.Misc.Dates.CurrentTimeZoneToUniversalTime(service.CreatedOn);
                var createdTimeSpan = DateTime.UtcNow - createdDate;
                if (createdTimeSpan <= TimeSpan.FromSeconds(2))
                {
                    ServiceCreated?.Invoke(service);
                }
                
                var modifiedDate = TCAdmin.SDK.Misc.Dates.CurrentTimeZoneToUniversalTime(service.ModifiedOn);
                var modifiedTimeSpan = DateTime.UtcNow - modifiedDate;
                if (modifiedTimeSpan <= TimeSpan.FromSeconds(2))
                {
                    ServiceModified?.Invoke(service);
                }
            }
            
            services.Clear();
        }
        
        private void RunServersJob()
        {
            var servers = Server.GetServers().ToServers();
            foreach (var server in servers)
            {
                var createdDate = TCAdmin.SDK.Misc.Dates.CurrentTimeZoneToUniversalTime(server.CreatedOn);
                var createdTimeSpan = DateTime.UtcNow - createdDate;
                var modifiedDate = TCAdmin.SDK.Misc.Dates.CurrentTimeZoneToUniversalTime(server.ModifiedOn);
                var modifiedTimeSpan = DateTime.UtcNow - modifiedDate;
                
                if (createdTimeSpan <= TimeSpan.FromSeconds(2))
                {
                    ServerCreated?.Invoke(server);
                }
                
                if (modifiedTimeSpan <= TimeSpan.FromSeconds(2))
                {
                    ServerModified?.Invoke(server);
                }
            }
            
            servers.Clear();
        }

        private void RunTasksJob()
        {
            var tasks = TCAdmin.TaskScheduler.SDK.Objects.Task.GetTasks(DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1)), DateTime.UtcNow, true, true, false, true, true, 0, null, null).ToArray();

            foreach (TCAdmin.TaskScheduler.SDK.Objects.Task task in tasks)
            {
                if (_snapshotTasksIds.Contains(task.TaskId)) continue;
                
                _snapshotTasksIds.Add(task.TaskId);
                TaskCreated?.Invoke(task);
            }
        }

        // Properties

        private readonly List<int> _snapshotTasksIds;
        
        // Events
        public event ServiceCreated ServiceCreated;

        public event ServiceModified ServiceModified;
        
        public event ServerCreated ServerCreated;
        
        public event ServerModified ServerModified;
        
        public event TaskCreated TaskCreated;
    }

    public delegate void ServiceModified(Service args);
    
    public delegate void ServiceCreated(Service args);
    
    public delegate void ServerCreated(Server args);
    
    public delegate void ServerModified(Server args);
    
    public delegate void TaskCreated(TCAdmin.TaskScheduler.SDK.Objects.Task args);
}