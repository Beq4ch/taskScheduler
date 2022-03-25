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
using taskScheduler.Data;

namespace taskScheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksListView : ContentPage
    {
        public ObservableCollection<TaskFilds> TasksList { get; set; }

        public IEnumerable<TaskFilds> selectdd { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public TasksListView()
        {
            InitializeComponent();

            

            

            RefreshCommand = new AsyncCommand(Refresh);
        }
        
        protected override async void OnAppearing()
        {
            BindingContext = new TaskFilds();

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
                TaskFilds task = (TaskFilds)e.Item;
                await Shell.Current.GoToAsync(
                    $"{nameof(AddTasks)}?{nameof(AddTasks.ItemID)}={task.ID.ToString()}");
            }
        }

        private async void DeleteTask_Clicked(object sender, EventArgs e)
        {
            TaskFilds task = (TaskFilds)BindingContext;

            await App.TasksDB.DeleteNoteAsync(task);

        }
        public async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            TasksList.Clear();

            var tasks = await App.TasksDB.GetTasksAsync();

            IsBusy = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            

            var selection = await App.TasksDB.db.QueryAsync<TaskFilds>(
                "SELECT * FROM Tasks WHERE Created = ?", "{System.DateTime.Year}");
            foreach (var k in selection)
                LabelDate.Text = Convert.ToString(k.Created);
        }
    }
}