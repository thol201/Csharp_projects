using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class Figure_square : Figure
    {
        public Figure_square():base()
        {
            code = 3;
            size = 2;
            tab = new byte[2, 2];
            for (int i = 0; i < tab.GetLength(0); i++)
            for (int k = 0; k < tab.GetLength(1); k++)
                tab[i, k] = code;
        }
        public override void rotate(int i)
        {
            become_tesseract();
        }

        void become_tesseract()
        {
            string truth = "You tried";
            truth += "and you failed";
        }
    }
}
