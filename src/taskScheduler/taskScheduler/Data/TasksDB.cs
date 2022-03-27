using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using taskScheduler.Models;

namespace taskScheduler.Data
{
    public class TasksDB
    {
        public readonly SQLiteAsyncConnection db;

        public TasksDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<TaskFilds>().Wait();
            /*db.CreateTableAsync<StepTask>().Wait();*/
        }

        public Task<List<TaskFilds>> GetTasksAsync()
        {
            return db.Table<TaskFilds>().ToListAsync();
        }
        /*public Task<List<StepTask>> GetStepTaskAsync()
        {
            return db.Table<StepTask>().ToListAsync();
        }*/
        public Task<TaskFilds> GetTaskAsync(int id)
        {
            return db.Table<TaskFilds>()
                                    .Where(i => i.ID.Equals(id))
                                    .FirstOrDefaultAsync();
        } 

        public Task<int> SaveTaskAsync (TaskFilds task)
        {
            if (task.ID != 0)
                return db.UpdateAsync(task);
            else
                return db.InsertAsync(task);
        }

        /*public Task<int> SaveStepTaskAsync(StepTask task)
        {
            if (task.ID != 0)
                return db.UpdateAsync(task);
            else
                return db.InsertAsync(task);
        }*/

        public Task<int> DeleteNoteAsync(TaskFilds task)
        {
            return db.DeleteAsync(task);
        }

        /*public Task<int> DeleteStepTaskAsync(StepTask task)
        {
            return db.DeleteAsync(task);
        }*/
    }
}
