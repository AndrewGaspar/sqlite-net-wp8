using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;
using System.Threading.Tasks;

namespace SQLiteConsumer
{
    public class Data
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public Decimal Value { get; set; }
    }

    public class DataHolder
    {
        private SQLiteAsyncConnection db;

        private Task creationTask;

        private async Task CreateTask() 
        {
            await db.DropTableAsync<Data>();
            await db.CreateTableAsync<Data>();
        }

        public DataHolder()
        {
            db = new SQLiteAsyncConnection("data_table");

            creationTask = CreateTask();
        }

        public async Task AddItem(string description, decimal value)
        {
            await creationTask;
            await db.InsertAsync(new Data()
            {
                Description = description,
                Value = value
            });
        }

        public async Task<IEnumerable<Data>> GetItems()
        {
            await creationTask;
            return await db.Table<Data>().ToListAsync();
        }
    }
}
