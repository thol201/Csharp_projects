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

    public class Game_Menager
    {
        private static InputMenager? inputs;                // input manager handles inputs
        public  System.Timers.Timer? UpdateTimer;     // main timer handles lowering figure
        public int points;                              
        public int status;                                  // Statuses
        public int Status { get => status; set { status = value; } }                // 0 - game not started
                                                                                    // 1 - Game running 
                                                                                    // 2 - Game handling update
                                                                                    // 3 - Game Over
        public bool disp;                                                                  // allow display if not displaying right now
        public Map map;
        public Map_Printer printer;
        List<Figure> figures;
        List<Figure> allfigures;
        Figure? active;
        public Figure? prev;
        public bool locked=false;                               
        public bool trigger;                                // Figure is on the ground but still can move for a while
                                                            // IT'S NOT A BUG ITS A FEATURE

        private bool? _console_present;
        public bool console_present
        {
            get
            {
                if (_console_present == null)
                {
                    _console_present = true;
                    try { int window_height = Console.WindowHeight; }
                    catch { _console_present = false; }
                }
                return _console_present.Value;
            }
        }


        public void set_function(string s,updater up)
        {
            switch (s)
            {
                case "Start": 
                    {
                        intro = up;
                        break;
                    }
                case "Display":
                    {
                        displayer = up;
                        break;
                    }
                case "Stop":
                    {
                        outro = up;
                        break;
                    }
                case "Score":
                    {
                        _points = up;
                        break;
                    }
            }
        }
        public delegate void updater();
        updater displayer;
        updater intro;
        updater outro;
        updater _points;

        public Game_Menager() {
            disp = true;
            Status = 0;
            points = 0;
            inputs = new InputMenager(this);
            active = null;
            map = new Map();
            List<Type> temporalfig;
            printer = new Map_Printer(map);
            figures = new List<Figure>();
            allfigures = new List<Figure>();
            temporalfig = new List<Type>();
            intro = displayIntro;
            displayer = Displeyfunc;
            outro = displayOutro;
            Type[]? types = Assembly.GetAssembly(typeof(Figure)).GetTypes();
            IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(typeof(Figure)));
            foreach (var tast in subclasses) { allfigures.Add((Figure)Activator.CreateInstance(tast)); }


        }
        public void displayIntro()
        {
            Console.WriteLine("Welcome to Tertis");
            Console.WriteLine("To Move Right Press {0}",(char)inputs.Action_Key_Move_Right);
            Console.WriteLine("To Move Left Press {0}", (char)inputs.Action_Key_Move_Left);
            Console.WriteLine("To Move Down Press {0}", (char)inputs.Action_Key_Move_Down);
            Console.WriteLine("To Move All The Way Down Press Spacebar");
            Console.WriteLine("To Rotate Right Press {0}", (char)inputs.Action_Key_Rotate_Right);
            Console.WriteLine("To Rotate Left Press {0}", (char)inputs.Action_Key_Rotate_Left);
            Console.WriteLine("Press Any Key Different Then Power Button To Start");
            try
            {
                Console.ReadKey(true);
            }
            catch { }
        }
        public void displayOutro()
        {
            Console.WriteLine("Game Over");
            Console.WriteLine("Your final score : {0}",points);
            Console.WriteLine("Press Any Key (Even Power Button) To Exit");
            Console.ReadKey(true);

        }


        public void Reset()
        {
            map = new();
            map.menager = this;
            Random ran = new Random();
            active = allfigures[ran.Next(0, allfigures.Count() - 1)];
            prev = allfigures[ran.Next(0, allfigures.Count() - 1)];
            active.x = 0;
            active.y = ran.Next(0, map.mapy - 3);
        }
        public void start()
        {
            map.menager = this;
            trigger = false;
            Status = 1;
            //if(console_present)
            intro();
            UpdateTimer = new System.Timers.Timer(1000);
            UpdateTimer.Elapsed += update;
            //UpdateTimer.AutoReset = true;
            UpdateTimer.Enabled = true;
            Random ran = new Random();
            active = allfigures[ran.Next(0, allfigures.Count() - 1)];
            prev = allfigures[ran.Next(0, allfigures.Count() - 1)];
            int r;
            while (prev.GetType() == active.GetType())
            {
                r = ran.Next(0, allfigures.Count());
                prev = allfigures[r];
            }
            active.x = 0;
            active.y = ran.Next(0, map.mapy - 3);
            if (console_present)
            inputs?.start();
            int bol = 0;
            bol = map.insert_figure(active, true);
            display();

        }

        public void stop()
        {
            Status = 3;
            UpdateTimer.Stop();
            UpdateTimer.Dispose();
            outro();
            if (console_present)
                inputs.stop();
        }
        public void Event(int x)
        {
            int bol=0;
            if (Status == 3)
                return;
            if (x != 1 && locked)
                return;
            switch (x) 
            {
                case 1:   // end game event
                    Status = 3;
                    stop();
                    string path= Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
                    path = path + "\\McCSharp\\tertis.jpg";
                    string argument = "/open, \"" + path + "\"";
                    try
                    {
                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case 2:   // rotate right event
                    if (active == null || Status == 2)
                        break;
                    Status = 2;
                    active.rotate(1);
                    map.clear();
                    if (map.get_instant_Collison(active) == 1)
                        active.rotate(0);
                    bol = map.insert_figure(active);
                    display();
                    Status = 1;
                    break;
                case 3:    // rotate left event
                    if (active == null || Status == 2)
                        break;
                    Status = 2;
                    active.rotate(0);
                    map.clear();
                    if (map.get_instant_Collison(active) == 1)
                        active.rotate(1);
                    bol = map.insert_figure(active);
                    display();
                    Status = 1;
                    break;
                case 4:    // move right event
                    if (active == null || Status == 2)
                        break;
                    Status = 2;
                    bol = map.get_Collision(active,1);
                    if (bol==1)
                    {
                        active.y++;
                        map.clear();
                        bol = map.insert_figure(active);
                        display();
                    }
                    Status = 1;
                    break;
                case 5:    // move left event
                    if (active == null || Status == 2)
                        break;
                    Status = 2;
                    bol = map.get_Collision(active, 2);
                    if (bol == 1)
                    {
                        active.y--;
                        map.clear();
                        bol =map.insert_figure(active);
                        display();
                    }
                    Status = 1;
                    break;
                case 6:    // move down event
                    if (active == null || Status == 2)
                        break;
                    Status = 2;
                    bol = map.get_Collision(active, 0);
                    if (bol == 1)
                    {
                        active.x++;
                        map.clear();
                        bol = map.insert_figure(active,true);
                        display();
                    }
                    Status = 1;
                    break;
                case 7:
                    if (active == null || Status==2)
                        break;
                    Status = 2;
                    while(map.get_Collision(active, 0)==1)
                        active.x++;
                    map.clear();
                    bol = map.insert_figure(active);
                    display();
                    Status = 1;
                    break;
            }
        }

        
        public void Displeyfunc()
        {
            Console.Clear();
            printer.print();
        }
        void display()
        {
            if (disp != true)    // return if display is updating
                return;
            disp = false;
            displayer();
            //UpdateTimer.Start();
            disp = true;
        }

        public async void update(Object source, ElapsedEventArgs e)
        {
            if (Status==2||Status==3)
                return;
            UpdateTimer.Stop();
            Random ran = new Random();
            Status = 2;
            map.clear();
            int bol = 0;
            bol = map.insert_figure(active, true);
            if (bol == 1 && trigger == false)
            {
                int multipler = 1;
                while (map.test_line() == true)
                {
                    points += 10 * multipler;
                    multipler++;
                }
                if(_points!=null)
                _points();
                active = null;
                active = prev;
                int r;
                while (prev.GetType() == active.GetType())
                {
                    r = ran.Next(0, allfigures.Count());
                    prev = allfigures[r];
                }
                active.x = 0;
                active.y = ran.Next(0, map.mapy-3);
                if (map.get_instant_Collison(active) == 1)
                {
                    Event(1);
                    return;
                }
                bol = map.insert_figure(active);
                display();
            }
            else if (bol == 2) { stop(); }
            else if (bol == 3) { trigger = true; }
            else
            {
                active.x++;
                map.clear();
                bol = map.insert_figure(active, true);
                display();
            }
            Status = 1;
            UpdateTimer.Start();
        }

    }


}