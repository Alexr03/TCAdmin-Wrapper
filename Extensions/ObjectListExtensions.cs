namespace TCAdminWrapper.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TCAdmin.SDK.Objects;
    using TCAdmin.TaskScheduler.SDK.Objects;
    using Service = TCAdmin.GameHosting.SDK.Objects.Service;
    
    public static class ObjectListExtensions
    {
        public static List<Service> ToServices(this ObjectList objectList)
        {
            return ToOther<Service>(objectList);
        }
        
        public static List<Server> ToServers(this ObjectList objectList)
        {
            return ToOther<Server>(objectList);
        }
        
        public static List<Task> ToTasks(this ObjectList objectList)
        {
            return ToOther<Task>(objectList);
        }

        public static List<T> ToOther<T>(this ObjectList objectList)
        {
            return Array.ConvertAll(objectList.ToArray(), item => (T)item).ToList();
        }
    }
}