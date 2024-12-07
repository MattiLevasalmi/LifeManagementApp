namespace LifeManagementApp.DbManager
{
    public class Tasks
    {
        public int TasksId { get; set; }
        public string Name { get; set; }
        public List<Task> TaskList { get; } = new();
    }
}