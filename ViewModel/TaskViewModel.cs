using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.DbManager;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.ViewModel
{
    public partial class TaskViewModel : ObservableObject
    {
        private TodoContext db;

        public TaskViewModel(TodoContext td)
        {
            db = td;
            Items = new ObservableCollection<Tasks>();
            loadTasks();
        }

        private async void loadTasks()
        {
            var taskdb = db.TaskList.Include(tl => tl.TaskList).OrderBy(t => t.TasksId).ToList();
            foreach (var tasklist in taskdb)
            {
                ObservableCollection<Task> listofTasks = new ObservableCollection<Task>();
                foreach (var task in tasklist.TaskList)
                {
                    listofTasks.Add(new Task(task.Text, task.Priority));
                }
                Items.Add(new Tasks(tasklist.Name, listofTasks));
            }
        }

        [ObservableProperty]
        ObservableCollection<Tasks> items;

        [ObservableProperty]
        string text;

        [ObservableProperty]
        string priority;

        [ObservableProperty]
        string title;

        [RelayCommand]
        void Add(Tasks t)
        {
            if (string.IsNullOrEmpty(Text) || string.IsNullOrEmpty(Priority))
                return;

            var tasksList = db.TaskList.Where(tl => tl.Name == t.Name).First();
            tasksList.TaskList.Add(new DbManager.Task { Text = Text, Priority = Priority });
            db.SaveChanges();

            t.Add(new Task(Text, Priority));
            Text = string.Empty;
            Priority = string.Empty;
        }

        [RelayCommand]
        void AddCategory()
        {
            if (string.IsNullOrEmpty(Title))
                return;

            db.Add(new DbManager.Tasks { Name = Title });
            db.SaveChanges();

            Items.Add(new Tasks(Title, new ObservableCollection<Task>()));
            Title = string.Empty;
        }

        [RelayCommand]
        void DeleteTask(Task t)
        {
            string categoryName = "";
            foreach (Tasks tasks in Items)
            {
                if (tasks.Contains(t))
                {
                    tasks.Remove(t);
                    categoryName = tasks.Name;
                    continue;
                }
            }

            var tasksList = db.TaskList.Where(tl => tl.Name == categoryName).First();
            var task = tasksList.TaskList.Where(task => task.Text == t.Text).First();
            tasksList.TaskList.Remove(task);
            db.SaveChanges();
        }

        [RelayCommand]
        void DeleteCategory(Tasks t)
        {
            var tasksList = db.TaskList.Where(tl => tl.Name == t.Name).First();
            db.TaskList.Remove(tasksList);
            db.SaveChanges();

            Items.Remove(t);
        }
    }
}
