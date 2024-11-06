using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace LifeManagementApp.ViewModel
{
    public partial class TaskViewModel : ObservableObject
    {
        public TaskViewModel()
        {
            Items = new ObservableCollection<Tasks>();
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

            t.Add(new Task(Text, Priority));
            Text = string.Empty;
            Priority = string.Empty;
        }

        [RelayCommand]
        void AddCategory()
        {
            if (string.IsNullOrEmpty(Title))
                return;
            Items.Add(new Tasks(Title, new ObservableCollection<Task>()));
            Title = string.Empty;
        }

        [RelayCommand]
        void Delete(Task t)
        {
            foreach (Tasks tasks in Items)
            {
                if (tasks.Contains(t))
                {
                    tasks.Remove(t);
                    return;
                }
            }
        }
    }
}
