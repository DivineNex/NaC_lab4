using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;


namespace Server
{
    class DataBaseManager
    {
        private String dbFileName;
        private SQLiteConnection dbCon;
        private SQLiteCommand dbCom;


        public void Connect()
        {
            dbCon = new SQLiteConnection("Data Source=DataBase.db; Version=3");
            dbCon.Open();
            dbCom = new SQLiteCommand(dbCon);

        }
        public void Select()
        {
            // метод должен принимать строку которая содержит параметры, которые надо вернуть(если "all
            // " тогда возвращаем все параметры), начальную дату с которой возвращаем параметры. 
            // возвращать за 22 все записи о температуре сверла
          //  ("SELECT * FROM Params ");
        }
        public void AddParamValue(DateTime time, string paramName, double value)
        { 
            dbCom.CommandText = $"INSERT INTO Params (Time, Name, Value) VALUES (:Time, :Name, :Value)";
            SQLiteTransaction transaction = dbCon.BeginTransaction();
            try
            {
                dbCom.Parameters.AddWithValue("Time", time);
                dbCom.Parameters.AddWithValue("Name", paramName);
                dbCom.Parameters.AddWithValue("Value", value);
                dbCom.ExecuteNonQuery();
                {
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Authorization(string type, string login, string password)
        {
            dbCom.CommandText = $"SELECT rowid FROM Users WHERE login like '%{login}%' and password like '%{password}%' and type like '%{type}%'" ;

            object count = dbCom.ExecuteScalar();
            //Int32 Total_Records = System.Convert.ToInt32(count); //возвращает номер строки первой найденной записи

            if (count != null)
            {
                //
            }
        }
    }
}
