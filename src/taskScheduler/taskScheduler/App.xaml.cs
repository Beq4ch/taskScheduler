using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using taskScheduler.Data;
using System.IO;
using SQLite;
using System.Reflection;

namespace taskScheduler
{
    [Preserve(AllMembers = true)]
    public partial class App : Application
    {
        public const string DATABASE_NAME = "TasksDatabase.db3";
        static TasksDB tasksDB;

        public static TasksDB TasksDB
        {
            get
            {
                if (tasksDB == null)
                {
                    tasksDB = new TasksDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        DATABASE_NAME));
                }
                return tasksDB;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
