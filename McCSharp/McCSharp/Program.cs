using System;
using System.Data;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Windows.Input;


namespace Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game_Menager menager= new Game_Menager();

            

            menager.start();
            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    //if ((Console.ReadKey(true).Key == ConsoleKey.Escape)) { menager.Event(1); break; }
                    //else if ((Console.ReadKey(true).Key == ConsoleKey.R)) menager.Event(2);
                }
                //else if ((Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.B)) menager.Event(2);
            }




        }
        
    }
}