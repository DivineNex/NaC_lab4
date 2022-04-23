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
        public void Cmd()
        {
            //
        }

        public void AddParamValue(DateTime time, string paramName, double value)
        {
            dbCom.CommandText = $"INSERT INTO Params (Time, Name, Value) VALUES (:Time, :Name, :Value)";
            try
            {
                dbCom.Parameters.AddWithValue("Time", time);
                dbCom.Parameters.AddWithValue("Name", paramName);
                dbCom.Parameters.AddWithValue("Value", value);
                dbCom.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //transaction.Rollback();
                throw;
            }
           
        }



    }
}
