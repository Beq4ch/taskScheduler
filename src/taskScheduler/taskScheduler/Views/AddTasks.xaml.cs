using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskScheduler.Models;
using Xamarin.Forms;
using taskScheduler.Views.RendererAndEffects;
using Xamarin.Forms.BehaviorsPack;
using Xamarin.Forms.Platform;
using Plugin.LocalNotification;
using taskScheduler.CustomInterfaceRepresentation;
using IntelliAbb.Xamarin.Controls;
using Android.App;

namespace taskScheduler.Views
{
    [QueryProperty (nameof(ItemID), nameof(ItemID))]
    public partial class AddTasks : ContentPage
    {
        public int rowsCount = 0;

        public static DateTime Date = new DateTime();
        public string ItemID 
        { 
            set
            {
                LoadTask(value);
            }
        }
        public AddTasks()
        {
            InitializeComponent();

            BindingContext = new TaskFilds();

        }
        private async void LoadTask(string value)
        {
            try
            {
                int id = int.Parse(value);

                TaskFilds task = await App.TasksDB.GetTaskAsync(id);

                BindingContext = task;
            }
            catch { }
        }

        protected override void OnAppearing()
        {
            TaskFilds task = new TaskFilds();

            taskStartButton.Text += $"{task.TaskCreatedDate}";
            taskCompletionButton.Text += $"{task.TaskCreatedDate}";

            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                bool saveOrNot = await DisplayAlert("Пердупреждение", "Сохранить последние внесенные изменения?", "Да", "Нет");
                if (saveOrNot)
                    OnSaveButton_Clicked(saveButton, EventArgs.Empty);
                else
                {
                    TaskFilds task = (TaskFilds)BindingContext;

                    if (!string.IsNullOrWhiteSpace(task.Name))
                    {
                        await Shell.Current.GoToAsync("..");
                        return;
                    }

                    await App.TasksDB.DeleteNoteAsync(task);

                    await Shell.Current.GoToAsync("..");

                }
            });
            return true;
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            TaskFilds task = (TaskFilds)BindingContext;

            task.TaskCreatedDate = TasksListView.Date.ToString("dd.MM.yyyy");

            if (!string.IsNullOrWhiteSpace(task.Name))
                await App.TasksDB.SaveTaskAsync(task);
            else
            {
                await App.TasksDB.DeleteNoteAsync(task);

                await Shell.Current.GoToAsync("..");

            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            TaskFilds task = (TaskFilds)BindingContext;

            await App.TasksDB.DeleteNoteAsync(task);

            await Shell.Current.GoToAsync("..");
        }

        private void TaskDone_CheckedChanged(object sender, CheckedChangedEventArgs e )
        {
            StrikethroughEditor strikethrough = new StrikethroughEditor();
            if (taskDone.IsChecked)
                taskName.Effects.Add(strikethrough);
            else
                taskName.Effects.Clear();
                
        }
        private async void SaveTaskName_Completed(object sender, EventArgs e)
        {
            TaskFilds task = (TaskFilds)BindingContext;

            if (!string.IsNullOrWhiteSpace(task.Name) && !string.IsNullOrWhiteSpace(task.Description))
                await App.TasksDB.SaveTaskAsync(task);
        }
        private async void taskStartDateAndTimeButton(object sender, EventArgs e)
        {
            /*await Navigation.PushAsync(new Calendar(false));*/

            var notification = new Plugin.LocalNotification.NotificationRequest
            {
                BadgeNumber = 1,
                Description = "Test Description",
                Title = "Notification",
                ReturningData = "Dummy Data",
                NotificationId = 1,
                Schedule = {
                    NotifyTime = DateTime.Now.AddSeconds(3)
                    /*NotifyRepeatInterval = */
                }

            };

            await NotificationCenter.Current.Show(notification);
        }
        private async void deadlineForCompletingTaskButton(object sender, EventArgs e)
        {
            /*await Navigation.PushAsync(new Calendar(fa));*/
        }

        /*public async void AddStep_Completed(object sender, EventArgs e)
        {
            rowsCount++;

            gridStep.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto }
            };

            TaskFilds steps = new TaskFilds();

            Binding bindingStepsDone = new Binding { Source = steps, Path = "StepIsDone" };
            Checkbox StepDone = new Checkbox()
            {
                OutlineColor = Color.Blue,
                Margin = 0,
                FillColor = Color.Blue,
                CheckColor = Color.White,
                Shape = Shape.Circle,
                IsChecked = Convert.ToBoolean(bindingStepsDone)
            };
            StepDone.SetBinding(Checkbox.IsCheckedProperty, bindingStepsDone);

            Binding bindingSteps = new Binding { Source = steps, Path = "Step" };
            XEditor Step = new XEditor
            {
                Placeholder = "Добавить шаг",
                MaxLength = 256,
                Margin = 0,
                Text = Convert.ToString(bindingSteps),
                AutoSize = EditorAutoSizeOption.TextChanges
            };
            Step.SetBinding(XEditor.TextProperty, bindingSteps);
             *//*stepTaskDone_IsCheckedChanged(StepDone, (CheckedChangedEventArgs)EventArgs.Empty);*//*


             gridStep.Children.Add(StepDone, 0, rowsCount);

             gridStep.Children.Add(Step, 1, rowsCount);


            TaskFilds task = (TaskFilds)BindingContext;
            if (!string.IsNullOrWhiteSpace(task.Name))
                await App.TasksDB.SaveTaskAsync(task);

            StepTask stepTask = (StepTask)BindingContext;
            if (!string.IsNullOrWhiteSpace(stepTask.Step))
                await App.TasksDB.SaveStepTaskAsync(stepTa);

        }

        private void StepTaskDone_IsCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            StrikethroughEditor strike = new StrikethroughEditor();
            if (stepTaskDone.IsChecked)
                stepTask.Effects.Add(strike);
            else
                stepTask.Effects.Clear();
        }*/
    }
}