using Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Figure_Z : Figure
    {
        public Figure_Z():base()
        {
            code = 1;
            size = 3;
            tab = new byte[3, 3];
            tab[0, 1] = code;
            tab[1, 1] = code;
            tab[1, 2] = code;
            tab[2, 2] = code;
        }
        /*public override void rotate()
        {
            if (rotation + 1 < 4) rotation++;
            else rotation = 0;
            switch (rotation)
            {
                case 0:
                    clear();
                    tab[0, 1] = code;
                    tab[1, 1] = code;
                    tab[1, 2] = code;
                    tab[2, 2] = code;
                    break;
                case 1:
                    clear();
                    tab[1, 1] = code;
                    tab[1, 2] = code;
                    tab[2, 0] = code;
                    tab[2, 1] = code;
                    break;
                case 2:
                    clear();
                    tab[0, 0] = code;
                    tab[1, 0] = code;
                    tab[1, 1] = code;
                    tab[2, 1] = code;
                    break;
                case 3:
                    clear();
                    tab[0, 1] = code;
                    tab[0, 2] = code;
                    tab[1, 0] = code;
                    tab[1, 1] = code;
                    break;
            }

        }
        */
        void clear()
        {
            for (int i = 0; i < tab.GetLength(0); i++)
                for (int k = 0; k < tab.GetLength(1); k++)
                    tab[i, k] = 0;

        }
    }
}
