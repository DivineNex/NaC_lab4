using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    internal class Program
    {
        private static RegistrationModule registrationModule;
        private static GenerationModule generationModule;

        static void Main(string[] args)
        {
            /*
             * КОМАНДЫ УПРАВЛЕНИЯ:
             * help - вывод доступных команд
             * connect - подключения модуля регистрации к серверу
             * disconnect - отключение модуля регистрации от сервера
             * start - запуск модулей
             * stop - остановка модулей

             */

            Init();

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "connect")
                    Connect();
                else if (line == "disconnect")
                    Disconnect();
                else if (line == "start")
                    StartModules();
                else if (line == "stop")
                    StopModules();
                else if (line == "help")
                    PrintHelpMessage();
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{line} не является командой");
                    Console.BackgroundColor = ConsoleColor.Black;
                }  
            }
        }

        private static void Init()
        {
            generationModule = new GenerationModule();
            registrationModule = new RegistrationModule(generationModule);

            generationModule.LoadAndParseParams();

            foreach (var par in generationModule.allParams)
            {
                par.rm = registrationModule;
            }

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Инициализация прошла успешно");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Введите help для получения списка доступных команд");
        }
        private static void PrintHelpMessage()
        {
            Console.WriteLine("\r\nДоступные команды: \r\nstart - запуск модулей" +
                                                 "\r\nstop - остановка модулей" +
                                                 "\r\nconnect - подключения модуля регистрации к серверу" +
                                                 "\r\ndisconnect - отключение модуля регистрации от сервера\r\n");
            
        }
        private static void Connect()
        {
            registrationModule.ConnectToServer();
        }

        private static void Disconnect()
        {
            registrationModule.DisconnectFromServer();
        }

        private static void StartModules()
        {
            if (generationModule.Active == false && registrationModule.Active == false)
            {
                if (registrationModule.Connected)
                {
                    generationModule.Start();
                    registrationModule.Start();
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Модули запущены");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Сперва подключитесь к серверу");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Модули уже запущены");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private static void StopModules()
        {
            if (generationModule.Active == true && registrationModule.Active == true)
            {
                generationModule.Stop();
                registrationModule.Stop();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Модули остановлены");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Модули уже остановлены");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
