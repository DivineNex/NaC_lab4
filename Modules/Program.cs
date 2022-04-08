using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            /*
             * КЛАВИШИ И КОМАНДЫ УПРАВЛЕНИЯ:
             * 'L' - остановить/возобновить логирование
             * 'S' - остановить/возобновить работу модулей
             */
            
            RegistrationModule registrationModule = new RegistrationModule();
            GenerationModule generationModule = new GenerationModule(registrationModule.buffer);
            
            generationModule.LoadAndParseParams();
            registrationModule.SetSendInterval(generationModule.minInterval);
            generationModule.Start();
            registrationModule.Start();
            

            while (true)
            {  

                if (Console.ReadKey().Key == ConsoleKey.S)
                   
                {  
                    if (generationModule.Active && registrationModule.Active)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Работа модулей остановлена");
                        Console.BackgroundColor = ConsoleColor.Black;
                        generationModule.Stop();
                        registrationModule.Stop();
                    }
                    else
                    {   
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.WriteLine("Работа модулей возобновлена");
                        Console.BackgroundColor = ConsoleColor.Black;
                        generationModule.Start();
                        registrationModule.Start();   
                    }
                }
                if (Console.ReadKey().Key == ConsoleKey.L)
                {
                    generationModule.Logging = !generationModule.Logging;
                }
            }

        }
    }
}
