using System;
using System.Collections.Generic;
using System.Text;
using IntelliAbb.Xamarin.Controls;
using SQLite;
using Xamarin.Forms;
using SQLiteNetExtensions.Attributes;
using taskScheduler.CustomInterfaceRepresentation;

namespace taskScheduler.Models
{
   /* [Table("StepTask")]
    public class StepTask
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Step { get; set; }
        public bool IsCompleted { get; set; }
        public bool StepIsDone { get; set; }
        
        *//*public XEditor StepEditor { get; set; }
        public Checkbox StepDoneChBx { get; set; }*//*

        [ForeignKey(typeof(TaskFilds))]
        public int TasksFildsID { get; set; }

        [ManyToOne]
        public TaskFilds TaskFilds { get; set; }
    }*/
}
