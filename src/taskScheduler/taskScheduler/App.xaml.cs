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
        public static TasksDB tasksDB;

        public static TasksDB TasksDB
        {
            get
            {
                if (tasksDB == null)
                {
                    tasksDB = new TasksDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        DATABASE_NAME));

                    /*if (!File.Exists(dbPath))
                    {
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"taskScheduler.Database.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                fs.Flush();
                            }
                        }
                    }
                    tasksDB = new TasksDB(dbPath);
                }*/
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
