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
using System.Globalization;

namespace taskScheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksListView : ContentPage
    {
        public ObservableCollection<TaskFilds> TasksList { get; set; }
        public IEnumerable<TaskFilds> Selectdd { get; set; }
        public AsyncCommand RefreshCommand { get; }

        public static DateTime Date = new DateTime();
        public static bool check = true;
        DateTime Tomorrow = new DateTime();
        DateTime DayAfterTomorrow = new DateTime();
        DateTime Yesterday = new DateTime();
        public TasksListView()
        {
            InitializeComponent();

            Date = DateTime.Today;
            Tomorrow = Date.AddDays(1);
            DayAfterTomorrow = Date.AddDays(2);
            Yesterday = Date.AddDays(-1);
            ButYesterday.Text = $"{Yesterday.ToString("dd.MM.yyyy")}\n{Yesterday.ToString("ddd")}\nВчера";
            ButToday.Text = $"{Date.ToString("dd.MM.yyyy")}\n{Date.ToString("ddd")}\nСегодня";
            ButTomorrow.Text = $"{Tomorrow.ToString("dd.MM.yyyy")}\n{Tomorrow.ToString("ddd")}\nЗавтра";
            ButAfterTomorrow.Text = $"{DayAfterTomorrow.ToString("dd.MM.yyyy")}\n{DayAfterTomorrow.ToString("ddd")}\nПослезавтра";

            RefreshCommand = new AsyncCommand(Refresh);
        } 
         
         async void SearchByDate (DateTime date)
         {
            if (check)
            {
                date = DateTime.Today;
                check = false;
                curDate.Text = date.ToString("dd.MM.yyyy");
            }
            else
            {
                listView.ItemsSource = await App.TasksDB.db.QueryAsync<TaskFilds>(
               "SELECT * FROM Tasks WHERE TaskCreatedDate = ?", date.ToString("dd.MM.yyyy"));
                curDate.Text = date.ToString("dd.MM.yyyy");
            }
                if (date < DateTime.Today)
                    Plus.IsEnabled = false;
                else
                    Plus.IsEnabled = true;
         }
        protected override async void OnAppearing()
        {
            

            BindingContext = new TaskFilds();

            listView.ItemsSource = await App.TasksDB.db.QueryAsync<TaskFilds>(
               "SELECT * FROM Tasks WHERE TaskCreatedDate = ?", DateTime.Now.ToString("dd.MM.yyyy"));

            SearchByDate(Date);

            base.OnAppearing();
        }
        private async void AddButtonClick_Clicked(object sender, EventArgs e)
        {
            TaskFilds task = (TaskFilds)BindingContext;

            task.TaskCreatedDate = DateTime.Now.ToString("dd.MM.yyyy");

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

            /*listView.ItemsSource = await App.TasksDB.GetTasksAsync();*/

            IsBusy = false;
        }

        private void Button_Clicked_ToDay(object sender, EventArgs e)
        {
            Date = DateTime.Today;
            SearchByDate(Date);
        }
        private void Button_Clicked_Tomorrow(object sender, EventArgs e)
        {
            Date = Tomorrow;
            SearchByDate(Tomorrow);
        }
        private void Button_Clicked_Afet_Tomorrow(object sender, EventArgs e)
        {
            Date = DayAfterTomorrow;
            SearchByDate(DayAfterTomorrow);
        }private void Button_Clicked_Yesterday(object sender, EventArgs e)
        {
            Date = Yesterday;
            SearchByDate(Yesterday);
        }
        private async void Button_Clicked_Calendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Calendar());
        }
    }
}