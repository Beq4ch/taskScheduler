using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using IntelliAbb.Xamarin.Controls;
using SQLite;
using taskScheduler.CustomInterfaceRepresentation;

namespace taskScheduler.Models
{
    [Table("Tasks")]
    public class TaskData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Step { get; set; }
        public bool IsCompleted { get; set; }
        public bool StepIsDone { get; set; }
        public string Description { get; set; }
        public DateTime DistributionByDay { get; set; }
        public string Importance { get; set; }
        public DateTime Replay { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Created { get; set; }
        public string TaskCreatedDate { get; set; }
    }
}
