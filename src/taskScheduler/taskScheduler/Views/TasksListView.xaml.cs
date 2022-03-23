using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using taskScheduler.Models;
using taskScheduler.Views;
using System.Windows.Input;
using Xamarin.Forms.Xaml;
using taskScheduler.Views.RendererAndEffects;
using SQLite;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;

namespace taskScheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksListView : ContentPage
    {
        public ObservableCollection<TaskData> TasksList { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public TasksListView()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            BindingContext = new TaskData();

            listView.ItemsSource = await App.TasksDB.GetTasksAsync();

            base.OnAppearing();
        }
        private async void AddButtonClick_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddTasks));
            
        }

        private async void OnSelectionChanged(object sender, ItemTappedEventArgs e)
        {
            if(e.Item != null)
            {
                TaskData task = (TaskData)e.Item;
                await Shell.Current.GoToAsync(
                    $"{nameof(AddTasks)}?{nameof(AddTasks.ItemID)}={task.ID.ToString()}");
            }
        }

        private async void DeleteTask_Clicked(object sender, EventArgs e)
        {
            TaskData task = (TaskData)BindingContext;

            await App.TasksDB.DeleteNoteAsync(task);

        }
        public async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }
    }
}