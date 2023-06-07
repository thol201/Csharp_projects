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
        List<IBlock> blocks;
        IBlock[] allfigures;
        public GameManeger()
        {
            //Type[]? types = Assembly.GetAssembly(typeof(IBlock)).GetTypes();
            //IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(typeof(IBlock)));
            //foreach (var tast in subclasses) { allfigures.Add((IBlock)Activator.CreateInstance(tast)); }

            //var subclasses = from Assembly in AppDomain.CurrentDomain.GetAssemblies()
            //                 from type in Assembly.GetTypes()
            //                 where type.IsSubclassOf(typeof(IBlock))
            //                 select type;
            //Console.Write(" ");

            //var type = typeof(IBlock);
            //var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p=>type.IsAssignableFrom(p));
            //IBlock instances =(IBlock)Activator.CreateInstance((Type)types);
            Type[] assambles = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                where !t.IsInterface&& !t.IsAbstract
                                where typeof(IBlock).IsAssignableFrom(t)
                                select t).ToArray();

            allfigures=assambles.Select(t=> (IBlock)Activator.CreateInstance(t)).ToArray();
            
            Console.Write(" ");

        }





        public void Start()
        {
            Random ran=new();
            blocks = new();
            for(int i=0;i<7;i++)
            {
                int r;
                r = ran.Next(0, allfigures.Count());
                blocks.Add(allfigures[r]);
            }

        }
    }
}
