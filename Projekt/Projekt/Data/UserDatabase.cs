using Projekt.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Data
{
    public class UserDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public UserDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Users).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Users)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Users>> GetItemsAsync()
        {
            return Database.Table<Users>().ToListAsync();
        }

        public Task<List<Users>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<Users>("SELECT * FROM [Users] WHERE [Done] = 0");
        }

        public Task<Users> GetItemAsync(int id)
        {
            return Database.Table<Users>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Users item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Users item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
