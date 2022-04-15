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
            dbCon = new SQLiteConnection();
            
        }
        public void Cmd()
        {
            dbCom = new SQLiteCommand();
        }

        public void AddParam()
        {
            if ()
            {

            }
        }

   }
}
