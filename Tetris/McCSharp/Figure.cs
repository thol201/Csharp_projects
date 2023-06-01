using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class Figure
    {
        public byte code;
        public int size;
        public bool falling;
        public int x;
        public int y;
        public Figure() { x = 0;y = 0; tab = null;rotation = 0; falling = true; }
        public byte[,]? tab;
        public int rotation;
        public virtual void rotate(int z)
        {
            byte[,] tmp = new byte[size, size];
            for (int i = 0; i < size; i++)
                for (int k = 0; k < size; k++)
                    if(z== 0)
                       tmp[size - i - 1, k] = tab[k, i];
                    else
                        tmp[k, i] = tab[size - i - 1, k];

            tab = tmp;
        }
        public virtual void change_code() 
        {
            for (int i = 0; i < tab.GetLength(0); i++)
                for (int k = 0; k < tab.GetLength(1); k++)
                    if (tab[i,k]!=0)
                        tab[i,k] = code;
                    }
    }
}
