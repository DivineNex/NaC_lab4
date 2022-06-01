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

        public void Authorization(Client client, string type, string login, string password)
        {
            dbCom.CommandText = $"SELECT rowid FROM Users WHERE login like '%{login}%' and password " +
                $"like '%{password}%' and type like '%{type}%'" ;

            object count = dbCom.ExecuteScalar();
            //Int32 Total_Records = System.Convert.ToInt32(count); //номер строки первой найденной записи

            if (count != null)
            {
                Program.SendMessageToClient("auth//success", client);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Клиент {client.ip_port} авторизовался по {login} {password}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Program.SendMessageToClient("auth//fail", client);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Клиент {client.ip_port} не авторизовался по {login} {password}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public void Registration(Client client, string type, string login, string password)
        {
            dbCom.CommandText = $"SELECT rowid FROM Users WHERE login like '%{login}%'";

            object count = dbCom.ExecuteScalar();
            //Int32 Total_Records = System.Convert.ToInt32(count); //номер строки первой найденной записи

            if (count == null)
            {
                dbCom.CommandText = $"INSERT INTO Users (login, password, type) VALUES (:login, :password, :type)";
                SQLiteTransaction transaction = dbCon.BeginTransaction();
                try
                {
                    dbCom.Parameters.AddWithValue("login", login);
                    dbCom.Parameters.AddWithValue("password", password);
                    dbCom.Parameters.AddWithValue("type", type);
                    dbCom.ExecuteNonQuery();
                    {
                        transaction.Commit();
                    }

                    Program.SendMessageToClient("registration//success", client);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Клиент {client.ip_port} зарегистрировался по {login} {password}");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    Program.SendMessageToClient("registration//fail", client);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Клиент {client.ip_port} не зарегистрировался по {login} {password}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    throw;
                }
            }
            else
            {
                Program.SendMessageToClient("registration//fail", client);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Клиент {client.ip_port} не зарегистрировался по {login} {password}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
