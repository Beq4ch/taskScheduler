using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskScheduler.Views;
using Xamarin.Forms;
using System.Windows.Input;

namespace taskScheduler
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddTasks), typeof(AddTasks));
        }
    }
}