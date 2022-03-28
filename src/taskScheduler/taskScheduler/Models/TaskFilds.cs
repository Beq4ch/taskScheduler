using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace taskScheduler.Models
{
    [Table("Tasks")]
    public class TaskFilds
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompletionDate { get; set; }
        public bool IsCompleted { get; set; }

        /*[OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<StepTask> Step { get; set; }*/
        public string Description { get; set; }
        public string Importance { get; set; }
        public string TaskCreatedDate { get; set; }
        public DateTime DistributionByDay { get; set; }
        public DateTime Replay { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Created { get; set; }
    }
}
