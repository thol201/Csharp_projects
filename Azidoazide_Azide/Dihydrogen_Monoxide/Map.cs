using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dihydrogen_Monoxide
{
    internal class Map
    {

        public IBlock[,] array;
        public IBlock[] allfigures;
        public int Width { get => array.GetLength(1); }
        public int Height { get => array.GetLength(0); }


        public Map()
        {
            array = null;
        }
        public Map(int h,int w, IBlock[] ib)
        {
            array = new IBlock[h,w];
            allfigures = ib;
        }


        public void Generate_Row()
        {
            Random r=new();
            int c;
            //c = r.Next(0, Width);
            array[0, r.Next(0, Width)] = new AddBall();

            for (int i=0;i<Width;i++) 
            {
                c=r.Next(0,allfigures.Count());
                if (allfigures[c] is AddBall) continue;
                array[0, i] = allfigures[c];
            }
        }
    }
}
