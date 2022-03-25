using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using taskScheduler.Models;

namespace taskScheduler.Data
{
    public class TasksDB
    {
        public SQLiteAsyncConnection db;


        public TasksDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<TaskFilds>().Wait();
        }

        public Task<List<TaskFilds>> GetTasksAsync()
        {
            return db.Table<TaskFilds>().ToListAsync();
        }

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

        public Task<int> DeleteNoteAsync(TaskFilds task)
        {
            return db.DeleteAsync(task);
        }
    }
}
