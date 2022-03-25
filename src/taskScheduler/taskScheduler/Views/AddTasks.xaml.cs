using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskScheduler.Models;
using Xamarin.Forms;
using taskScheduler.Views.RendererAndEffects;
using Xamarin.Forms.BehaviorsPack;
using taskScheduler.CustomInterfaceRepresentation;
using IntelliAbb.Xamarin.Controls;

namespace taskScheduler.Views
{
    [QueryProperty (nameof(ItemID), nameof(ItemID))]
    public partial class AddTasks : ContentPage
    {
        public int rowsCount = 0;
        
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

            BindingContext = new TaskData();


        }
        private async void LoadTask(string value)
        {
            try
            {
                int id = int.Parse(value);

                TaskData task = await App.TasksDB.GetTaskAsync(id);

                BindingContext = task;
            }
            catch { }
        }

        protected override void OnAppearing()
        {
            
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
                    TaskData task = (TaskData)BindingContext;

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
            TaskData task = (TaskData)BindingContext;

            task.Created = DateTime.Now;
            task.TaskCreatedDate = DateTime.Now.ToString("dd.MM.yyyy");

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
            TaskData task = (TaskData)BindingContext;

            await App.TasksDB.DeleteNoteAsync(task);

            await Shell.Current.GoToAsync("..");
        }

        private void taskDone_CheckedChanged(object sender, CheckedChangedEventArgs e )
        {
            StrikethroughEditor strikethrough = new StrikethroughEditor();
            if (taskDone.IsChecked)
                taskName.Effects.Add(strikethrough);
            else
                taskName.Effects.Clear();
                
        }

        public async void AddStep_Completed(object sender, EventArgs e)
        {
            rowsCount++;

            gridStep.RowDefinitions = new RowDefinitionCollection();

            gridStep.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto});

            TaskData steps = new TaskData();
            /*Binding bindingStepsDone = new Binding { Source = steps, Path = "StepIsDone" };*/

            Checkbox stepDone = new Checkbox {
                OutlineColor = Color.Blue,
                Margin = 0,
                FillColor = Color.Blue,
                CheckColor = Color.White,
                Shape = Shape.Circle
            };

            /*Checkbox stepDone = new Checkbox()
            {
                
                *//*IsChecked = Convert.ToBoolean(bindingStepsDone)*//*
            };*/

            /*stepDone.SetBinding(Checkbox.IsCheckedProperty, bindingStepsDone);*/

            Binding bindingSteps = new Binding { Source = steps, Path = "Step" };

            XEditor step = new XEditor
            {
                Placeholder = "Добавить шаг",
                MaxLength = 256,
                Margin = 0,
                Text = "Hi",
                AutoSize = EditorAutoSizeOption.TextChanges
            };

            step.SetBinding(XEditor.TextProperty, bindingSteps);
            /*stepTaskDone_IsCheckedChanged(stepDone, (CheckedChangedEventArgs)EventArgs.Empty);*/


            gridStep.Children.Add(stepDone, 0, rowsCount);

            gridStep.Children.Add(step, 1, rowsCount);


            TaskData task = (TaskData)BindingContext;

            task.Created = DateTime.Now;        

            if (!string.IsNullOrWhiteSpace(task.Name))
                await App.TasksDB.SaveTaskAsync(task);

        }

        private void stepTaskDone_IsCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            StrikethroughEditor strike = new StrikethroughEditor();
            if (stepTaskDone.IsChecked)
                stepTask.Effects.Add(strike);
            else
                stepTask.Effects.Clear();
        }

        private async void saveTaskName_Completed(object sender, EventArgs e)
        {
            TaskData task = (TaskData)BindingContext;

            task.Created = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(task.Name))
                await App.TasksDB.SaveTaskAsync(task);
        }
    }
}