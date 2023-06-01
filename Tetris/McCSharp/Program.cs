using System;
using System.Data;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Windows.Input;


namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Game_Menager menager= new Game_Menager();
            menager.start() ;
           
           


        }
        
    }
}