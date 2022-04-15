using MileageManagerForms.Database;
using MileageManagerForms.Utilities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MileageManagerForms.DataAccess
{
    public class MileageItemRepository
    {
        internal readonly SQLiteAsyncConnection sqlConnection;
        internal readonly SQLiteConnection sqlConnection2;

        public MileageItemRepository()
        {
            DBFileHelper db = new DBFileHelper();
            sqlConnection = new SQLiteAsyncConnection(db.GetLocalFilePath("MileageManagerDB.db3"));
            sqlConnection2 = new SQLiteConnection(db.GetLocalFilePath("MileageManagerDB.db3"));
            if (!AutoTablePresent())
                AllocateTables();
        }

        public async void AllocateTables()
        {
            sqlConnection2.CreateTable<AutoTableDefination>();
            await sqlConnection.CreateTableAsync<MileageTableDefination>();
        }

        public void AlterMileageTable()
        {
            DBFileHelper db = new DBFileHelper();
            SQLiteConnection sqlConnection3 = new SQLiteConnection(db.GetLocalFilePath("MileageManagerDB.db3"));
            SQLiteCommand cmd = new SQLiteCommand(sqlConnection3)
            {
                CommandText = "ALTER TABLE MileageData ADD COLUMN Note varchar(50);"
            };
            cmd.ExecuteNonQuery();
        }

        public Task<List<MileageTableDefination>> GetMileageData(int autoId) => sqlConnection.QueryAsync<MileageTableDefination>("select * from MileageData where Carid = ? ", autoId);




        public async Task<List<MileageDisplayDefination>> GetMileageDisplayData(int autoId)
        {
            List<Task> tt = new List<Task>();
            List<MileageDisplayDefination> results = new List<MileageDisplayDefination>();
            tt.Add(
               new Task(async () =>
               {
                   results = await sqlConnection.QueryAsync<MileageDisplayDefination>("select * from MileageData where Carid = ? ", autoId);
                   Thread.Sleep(4000);

               }));
            return results;
        }




        public async Task<List<MileageTableDefination>> GetAllMileageData()
        {
            return await sqlConnection.Table<MileageTableDefination>().ToListAsync();
        }

        //public async Task<List<MileageDisplayDefination>> GetMileageDisplayData(int autoId) => await sqlConnection.QueryAsync<MileageDisplayDefination>("select * from MileageData where Carid = ?", autoId);

        public async Task<List<MileageTableDefination>> GetMileageDisplayDataByDate(int autoId, string month, int year)
        {
            string newMonth = string.Empty;
            if (month.Length == 1)
            {
                newMonth = "0" + month;
            }
            else
            {
                newMonth = month;
            }

            var crap = sqlConnection2.Query<MileageTableDefination>("select * from MileageData where Carid = ? AND (substr(StrDate, 1, 2) = ? AND substr(StrDate, 7,4) = ?)", autoId, newMonth, year.ToString());
            return crap;
        }

        public async Task<List<MileageDisplayDefination>> DeleteAllMileageEntries(int autoId) => await sqlConnection.QueryAsync<MileageDisplayDefination>("delete from MileageData where Carid = ?", autoId);

        public async Task<List<MileageDisplayDefination>> GetAllMileageDisplayData() => await sqlConnection.QueryAsync<MileageDisplayDefination>("select * from MileageData");

        //Delete specific ToDoItem
        public async Task DeleteMileageEntry(MileageTableDefination todoItem)
        {
            await sqlConnection.DeleteAsync(todoItem);
        }

        //Delete specific ToDoItem
        public async Task DeleteCar(AutoTableDefination todoItem)
        {
            await sqlConnection.DeleteAsync(todoItem);
        }

        //Add new ToDoItem to DB
        public async Task<int> AddMileageData(MileageTableDefination todoItem)
        {
            return await sqlConnection.InsertAsync(todoItem);
        }

        public int AddMileageDataSync(MileageTableDefination todoItem)
        {
            return sqlConnection2.Insert(todoItem);
        }

        internal async Task<int> DropMileageTable()
        {
            return await sqlConnection.DropTableAsync<MileageTableDefination>();
        }

        internal async Task<int> DropAutosTable()
        {
            return await sqlConnection.DropTableAsync<AutoTableDefination>();
        }

        public Task<int> AddAutoData(AutoTableDefination autoData)
        {
            return sqlConnection.InsertAsync(autoData);
        }

        public async Task<List<AutoTableDefination>> GetAutoData()
        {
            return await sqlConnection.Table<AutoTableDefination>().ToListAsync();
        }

        public async Task<List<AutoTableDefination>> GetDefaultAutoData()
        {
            //var results = await sqlConnection.UpdateAsync(auto);
            List<Task> tt = new List<Task>();
            List<AutoTableDefination> results = new List<AutoTableDefination>();
            tt.Add(
               new Task(async () =>
               {
                   results = await sqlConnection.QueryAsync<AutoTableDefination>("Select * from AutoData where IsDefault = ?", true);
                   Thread.Sleep(4000);

               }));
            return results;
        }
        //Get all ToDoItems
        public async Task<List<AutoTableDefination>> GetAuto()
        {
            return await sqlConnection.Table<AutoTableDefination>().ToListAsync();
        }


        //Get all ToDoItems
        public List<AutoTableDefination> GetAuto2()
        {
            return sqlConnection2.Query<AutoTableDefination>("select * from AutoData");
        }

        //Get all ToDoItems
        public bool AutoTablePresent()
        {
            try
            {
                string autoTable = "AutoData";
                //sqlConnection2.Execute("SELECT Count(*) FROM sqlite_master WHERE tbl_name = ? AND type = ?", autoTable, table);
                sqlConnection2.Query<AutoTableDefination>("SELECT * FROM " + autoTable);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Get all ToDoItems
        public List<AutoTableDefination> GetAuto3(int id) => sqlConnection2.Query<AutoTableDefination>("select CarYear, CarDesc from AutoData where id = ?", id);

        //Update Mileage
        public async Task<int> UpdateMileageAsync(MileageTableDefination mileage)
        {
            return await sqlConnection.UpdateAsync(mileage);
        }

        //Update Mileage
        //public async Task<int> UpdateAutoAsync(C auto)
        //{
        //    return await sqlConnection.UpdateAsync(auto);
        //}
        public async Task<int> UpdateAutoAsync(bool isDefault, int id)
        {
            //var results = await sqlConnection.UpdateAsync(auto);
            List<Task> tt = new List<Task>();
            int results = -1;

            results = await sqlConnection.ExecuteAsync("Update AutoData Set IsDefault = ? where id = ?", isDefault, id);
            Thread.Sleep(1000);
            return results;
        }
    }
}