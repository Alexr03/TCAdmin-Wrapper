namespace TCAdminWrapper.Objects
{
    using TCAdmin.GameHosting.SDK.Objects;
    using TCAdmin.TaskScheduler.ModuleApi;

    class TaskData
    {
        public TaskInfo TaskInfo { get; set; }

        public Service Service { get; set; }

        public StepInfo StepInfo { get; set; }
    }
}
