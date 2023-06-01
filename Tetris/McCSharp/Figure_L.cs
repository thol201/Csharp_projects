using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class Figure_L : Figure
    {
        public Figure_L():base()
        {
            size = 3;
            code = 4;
            tab = new byte[3, 3];
            for (int i = 0; i < tab.GetLength(1); i++)
                tab[i, 1] = code;
                tab[2, 2] = code;
        }

        /*
        public override void rotate()
        {
            if (rotation + 1 < 4) rotation++;
            else rotation = 0;
            switch (rotation)
            {
                case 0:
                    clear();
                    for (int i = 0; i < tab.GetLength(0); i++)
                        tab[i, 1] = code;
                    tab[2, 2] = code;
                    break;
                case 1 :
                    clear();
                    for (int i = 0; i < tab.GetLength(0); i++)
                        tab[1, i] = code;
                    tab[2, 0] = code;
                    break;
                case 2:
                    clear();
                    for (int i = 0; i < tab.GetLength(0); i++)
                        tab[i, 1] = code;
                    tab[0, 0] = code;
                    break;
                case 3:
                    clear();
                    for (int i = 0; i < tab.GetLength(0); i++)
                        tab[1, i] = code;
                    tab[0, 2] = code;
                    break;
            }

        }
        */

        void clear()
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            for (int k = 0; k < tab.GetLength(1); k++)
                    tab[i,k] = 0;
                
        }
    }
}
