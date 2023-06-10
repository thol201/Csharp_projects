using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dihydrogen_Monoxide
{
    class GameManeger
    {
        //List<IBlock> blocks;
        
        IBlock[] allfigures;
        Map map;
        //Printer printer;
        public delegate void printer();
        printer _Print;
        public GameManeger()
        {
            Type[] assambles = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                where !t.IsInterface&& !t.IsAbstract
                                where typeof(IBlock).IsAssignableFrom(t)
                                select t).ToArray();
            allfigures=assambles.Select(t=> (IBlock)Activator.CreateInstance(t)).ToArray();
            map = new(8, 7,allfigures);
            //printer = new Printer(map) ;
            _Print = null;
        }

        public void set(printer p)
        {
            _Print= p;
        }




        public void Start()
        {
            Random ran=new();
            map.Generate_Row();
            //printer.print();
            _Print();

        }
    }
}
