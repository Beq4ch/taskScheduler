using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using XCalendar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace taskScheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentPage
    {
        public Calendar()
        {
            
            InitializeComponent();
        }
        private async void Calendarobj_DateSelectionChanged(object sender, XCalendar.Models.DateSelectionChangedEventArgs e)
        {
            TasksListView curDate = new TasksListView();
            TasksListView listsView = new TasksListView();
            try
            {
                curDate.CurDate.Text = Calendarobj.SelectedDates.Last().ToString("dd.MM.yyyy");
                listsView.listView.ItemsSource = await App.TasksDB.GetTasksAsync();
                await Navigation.PopAsync();
            }
            catch { }

        }
    }
}