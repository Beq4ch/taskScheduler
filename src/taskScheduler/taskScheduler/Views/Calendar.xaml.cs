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
        public bool nuoleg;
        public Calendar(bool oleg)
        {
            
            InitializeComponent();
            nuoleg = oleg;
            Calendarobj.TodayDate = TasksListView.Date;
        }
        private async void Calendarobj_DateSelectionChanged(object sender, XCalendar.Models.DateSelectionChangedEventArgs e)
        {

            try
            {               
                TasksListView.Date = Calendarobj.SelectedDates.Last();
                await Navigation.PopAsync();
            }
            catch { }

        }

        private void timePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TasksListView.MicroTime = timePicker.Time;

        }

    }
}