using LifeManagementApp.ViewModel;

namespace LifeManagementApp;

public partial class TodoPage : ContentPage
{
	public TodoPage(TaskViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}