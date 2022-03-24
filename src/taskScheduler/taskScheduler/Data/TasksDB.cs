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

            db.CreateTableAsync<TaskData>().Wait();
        }

        public Task<List<TaskData>> GetTasksAsync()
        {
            return db.Table<TaskData>().ToListAsync();
        }

        public Task<TaskData> GetTaskAsync(int id)
        {
            return db.Table<TaskData>()
                                    .Where(i => i.ID.Equals(id))
                                    .FirstOrDefaultAsync();
        } 

        public Task<int> SaveTaskAsync (TaskData task)
        {
            if (task.ID != 0)
                return db.UpdateAsync(task);
            else
                return db.InsertAsync(task);
        }

        public Task<int> DeleteNoteAsync(TaskData task)
        {
            return db.DeleteAsync(task);
        }
    }
}
