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
        private static InputMenager? inputs;                // input manager handles inputs
        public static System.Timers.Timer? UpdateTimer;     // main timer handles lowering figure
        public int points;                              
        public int status;                                  // statuses
                                                            // 0 - game not started
                                                            // 1 - Game running 
                                                            // 2 - Game handling update
                                                            // 3 - Game Over
        bool disp;                                          // allow display if not displaying right now
        public Map map;
        public Map_Printer printer;
        List<Figure> figures;
        List<Figure> allfigures;
        Figure? active;
        public bool trigger;                                // Figure is on the ground but still can move for a while
                                                            // IT'S NOT A BUG ITS A FEATURE

        public Game_Menager() {
            disp = true;
            status = 0;
            points = 0;
            inputs = new InputMenager(this);
            active = null;
            map = new Map();
            List<Type> temporalfig;
            printer = new Map_Printer(map);
            figures = new List<Figure>();
            allfigures = new List<Figure>();
            temporalfig = new List<Type>();
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
            Console.ReadKey(true);

        }
        public void displayOutro()
        {
            Console.WriteLine("Game Over");
            Console.WriteLine("Your final score : {0}",points);
            Console.WriteLine("Press Any Key (Even Power Button) To Exit");
            Console.ReadKey(true);

        }

        public void start()
        {
            map.menager = this;
            trigger = false;
            status = 1;
            displayIntro();
            UpdateTimer = new System.Timers.Timer(1000);
            UpdateTimer.Elapsed += update;
            UpdateTimer.AutoReset = true;
            UpdateTimer.Enabled = true;
            Random ran = new Random();
            active = allfigures[ran.Next(0, allfigures.Count() - 1)];
            active.x = 0;
            active.y = ran.Next(0, map.mapy - 3);
            inputs.start();
        }

        public void stop()
        {
            status = 3;
            UpdateTimer.Stop();
            UpdateTimer.Dispose();
            displayOutro();
            inputs.stop();
        }
        public void Event(int x)
        {
            int bol=0;
            if (status == 3)
                return;
            switch (x) 
            {
                case 1:   // end game event
                    status = 3;
                    stop();
                    string path= Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                    path = path + "\\tertis.jpg";
                    string argument = "/open, \"" + path + "\"";
                    try
                    {
                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case 2:   // rotate right event
                    if (active == null || status == 2)
                        break;
                    status = 2;
                    active.rotate(1);
                    map.clear();
                    if (map.get_instant_Collison(active) == 1)
                        active.rotate(0);
                    bol = map.insert_figure(active);
                    display();
                    status = 1;
                    break;
                case 3:    // rotate left event
                    if (active == null || status == 2)
                        break;
                    status = 2;
                    active.rotate(0);
                    map.clear();
                    if (map.get_instant_Collison(active) == 1)
                        active.rotate(1);
                    bol = map.insert_figure(active);
                    display();
                    status = 1;
                    break;
                case 4:    // move right event
                    if (active == null || status == 2)
                        break;
                    status = 2;
                    bol = map.get_Collision(active,1);
                    if (bol==1)
                    {
                        active.y++;
                        map.clear();
                        bol = map.insert_figure(active);
                        display();
                    }
                    status = 1;
                    break;
                case 5:    // move left event
                    if (active == null || status == 2)
                        break;
                    status = 2;
                    bol = map.get_Collision(active, 2);
                    if (bol == 1)
                    {
                        active.y--;
                        map.clear();
                        bol =map.insert_figure(active);
                        display();
                    }
                    status = 1;
                    break;
                case 6:    // move down event
                    if (active == null || status == 2)
                        break;
                    status = 2;
                    bol = map.get_Collision(active, 0);
                    if (bol == 1)
                    {
                        active.x++;
                        map.clear();
                        bol = map.insert_figure(active,true);
                        display();
                    }
                    status = 1;
                    break;
                case 7:
                    if (active == null || status==2)
                        break;
                    status = 2;
                    while(map.get_Collision(active, 0)==1)
                        active.x++;
                    map.clear();
                    bol = map.insert_figure(active);
                    display();
                    status = 1;
                    break;
            }
        }

        

        void display()
        {
            if (disp != true)    // return if display is updating
                return;
            disp = false;
            Console.Clear();
            printer.print() ;
            disp = true;
        }

        public void update(Object source, ElapsedEventArgs e)
        {
            Random ran = new Random();
            if (status==2||status==3)
                return;
            status = 2;
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
                active = null;
                active = allfigures[ran.Next(0, allfigures.Count() - 1)];
                active.x = 0;
                active.y = ran.Next(0, map.mapy-3);
                if (map.get_instant_Collison(active) == 1)
                {
                    Event(1);
                    return;
                }
                display();
            }
            else if (bol == 2) { Console.WriteLine("AMEN"); stop(); }
            else if (bol == 3) { trigger = true; }
            else
            {
                active.x++;
                map.clear();
                bol = map.insert_figure(active, true);
                display();
            }
            status = 1;
        }

    }


}