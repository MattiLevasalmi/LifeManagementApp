using System.Collections.ObjectModel;

namespace LifeManagementApp
{
    public class Task
    {
        public string Text { get; set; }
        public string Priority { get; set; }

        public Task(string text, string priority) 
        { 
            Text = text;
            Priority = priority;
        }
    }

    public class Tasks : ObservableCollection<Task>
    {
        public string Name { get; set; }

        public Tasks(string name, ObservableCollection<Task> taskList) : base(taskList)
        {
            Name = name;
        }
    }
}
