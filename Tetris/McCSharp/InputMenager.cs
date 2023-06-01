using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Runtime;

namespace Program
{
    public class InputMenager
    {
        Game_Menager menager;
        Thread t;
        public int Action_Key_Rotate_Right='R';
        public int Action_Key_Rotate_Left = 'Q';
        public int Action_Key_Move_Right = 'D';
        public int Action_Key_Move_Left = 'A';
        public int Action_Key_Move_Down = 'S';
        public int Action_Key_Move_Full_Down = (int)ConsoleKey.Spacebar;

        public InputMenager()
        {
            t = null;
            menager = null;

        }
        public InputMenager ( Game_Menager menagment)
        {
            t = null;
            menager=menagment;
        }

        public void stop()
        {
            t.Interrupt();
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void loop()
        {
            ConsoleKeyInfo cki;
            while (menager.Status!=3)
            {
                cki = Console.ReadKey(true);
                if ((cki.Key == ConsoleKey.Escape)) { menager.Event(1);}
                else if ((cki.Key == (ConsoleKey)Action_Key_Rotate_Right)) menager.Event(2);
                else if ((cki.Key == (ConsoleKey)Action_Key_Rotate_Left)) menager.Event(3);
                else if ((cki.Key == (ConsoleKey)Action_Key_Move_Right)) menager.Event(4);
                else if ((cki.Key == (ConsoleKey)Action_Key_Move_Left)) menager.Event(5);
                else if ((cki.Key == (ConsoleKey)Action_Key_Move_Down)) menager.Event(6);
                else if ((cki.Key == (ConsoleKey)Action_Key_Move_Full_Down)) menager.Event(7);
                //else if ((cki.Key == ConsoleKey.Spacebar)) menager.Event(7);
               
               
                    //while (Console.In.Peek() != -1)
                        //Console.In.Read();
                
            }
        }

        public void start()
        {
            t = new Thread(new ThreadStart(loop));
            t.Start();
        }

    }
}
