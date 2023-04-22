using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Figure_long : Figure  
    {
        public Figure_long():base()
        {
            size = 4;
            code = 5;
            tab = new byte[4,4];
            for(int i=0;i<tab.GetLength(1);i++)
                tab[2,i] = code;
        }

        /*
        public override void rotate() {
            if (rotation + 1 < 4) rotation++;
            else rotation=0;
            switch (rotation)
            {
            case 0 or 2:
                    clear();
                    for (int i = 0; i < tab.GetLength(1); i++)
                        tab[2, i] = code;
                    break;
            case 1 or 3:
                    clear();
                    for (int i = 0; i < tab.GetLength(1); i++)
                        tab[i, 2] = code;
                    break;
            }
        }
        */

        

        void clear()
        {
            for(int i=0;i<tab.GetLength(0);i++)
                if(rotation==0||rotation==2)
                     tab[i,2] = 0;
                else tab[2,i] = 0;
        }
    }
}
