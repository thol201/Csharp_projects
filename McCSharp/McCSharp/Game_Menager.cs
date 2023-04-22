using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows;

namespace Program
{

    class Game_Menager
    {
        public static System.Timers.Timer UpdateTimer;
        public static System.Timers.Timer DisplayTimer;
        public static System.Timers.Timer InputTimer;
        public long time;
        public Map map;
        public Map_Printer printer;
        List<Figure> figures;
        List<Figure> allfigures;
        Figure? active;

        public Game_Menager() {
            time = 0;
            active = null;
            map = new Map();
            List<Type> temporalfig;
            printer = new Map_Printer(map);
            figures = new List<Figure>();
            allfigures = new List<Figure>();
            temporalfig = new List<Type>();
            Type[] types = Assembly.GetAssembly(typeof(Figure)).GetTypes();
            IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(typeof(Figure)));
            //foreach (var tast in subclasses) { temporalfig.Add(tast); }
            foreach (var tast in subclasses) { allfigures.Add((Figure)Activator.CreateInstance(tast)); }


        }
        
        public void start()
        {
            UpdateTimer = new System.Timers.Timer(1000);
            DisplayTimer = new System.Timers.Timer(100);
            InputTimer = new System.Timers.Timer(100);
            UpdateTimer.Elapsed += update;
            DisplayTimer.Elapsed += display;
            InputTimer.Elapsed += input;
            UpdateTimer.AutoReset = true;
            DisplayTimer.AutoReset = true;
            InputTimer.AutoReset = true;
            UpdateTimer.Enabled = true;
            DisplayTimer.Enabled = true;
            InputTimer.Enabled = true;
            //map.ptr[15, 15] = 244;
            Random ran = new Random();
            active = allfigures[ran.Next(0, allfigures.Count() - 1)];//new Figure_L();
            active.x = 2;
            active.y = ran.Next(2, 17);
        }

        public void stop()
        {
            UpdateTimer.Stop();
            UpdateTimer.Dispose();
            DisplayTimer.Stop();
            DisplayTimer.Dispose();
        }
        public void Event(int x)
        {
            switch(x) 
            {
                case 1:
                    stop();
                    string path= Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                    path = path + "\\tertis.jpg";
                    string argument = "/open, \"" + path + "\"";
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                    break;
                case 2:
                    if (active == null)
                        break;
                    active.rotate(0);
                    map.clear();
                    //foreach (Figure f in figures)
                    int bol = map.insert_figure(active, active.x, active.y);
                    break;
            }
        }

        void input(Object source, ElapsedEventArgs e)
        {
            if ((Console.ReadKey(true).Key == ConsoleKey.Escape)) Event(1); 
            else if ((Console.ReadKey(true).Key == ConsoleKey.R)) Event(2);
        }

        void display(Object source, ElapsedEventArgs e)
        {
            Console.Clear();
            printer.print();

        }

        public void update(Object source,ElapsedEventArgs e) 
        {
            Random ran = new Random();
            map.clear();
            active.x++;
            int bol=0;
            //foreach (Figure f in figures)
                bol=map.insert_figure(active,active.x,active.y);
            if (bol == 1)
            {
                active = allfigures[ran.Next(0, allfigures.Count() - 1)];//new Figure_L();
                active.x = 2;
                active.y = ran.Next(2, 17);
                //active.falling = false;
                //active.code = (byte)(255 - active.code);
                //map.clear();
                //bol = map.insert_figure(active, active.x, active.y);
                //active.change_code();
                //active = null;
            }
            else if(bol == 2) { Console.WriteLine("AMEN"); }

        }

    }


}