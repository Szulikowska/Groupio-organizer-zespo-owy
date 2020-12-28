using Projekt.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Data
{
    public class UserGroupDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePathPersonalized+"UserGroupSQLite.db3", Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public UserGroupDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Group).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Group)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Group>> GetItemsAsync()
        {
            return Database.Table<Group>().ToListAsync();
        }

        public Task<List<Group>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<Group>("SELECT * FROM [UserGroup] WHERE [Done] = 0");
        }

        public Task<Group> GetItemAsync(int id)
        {
            return Database.Table<Group>().Where(i => i.Col == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Group item)
        {
            if (item.Col != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Group item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
