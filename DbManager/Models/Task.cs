namespace LifeManagementApp.DbManager
{
    public class Task
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Priority { get; set; }
        public Tasks Tasks { get; set; }
        public int TasksId { get; set; }
    }
}