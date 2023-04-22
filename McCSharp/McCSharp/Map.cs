using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Map
    {
        public byte[,] ptr = null;
        public Map() {
            ptr = new byte[20,20];
        }

        public int rows { get { return ptr.GetLength(0); } }
        public int cols { get { return ptr.GetLength(1); } }

        public void map_print()
        {
            for (int i = 0; i < ptr.GetLength(0); i++)
            {
                for (int k = 0; k < ptr.GetLength(1); k++)
                {
                    Console.Write(ptr[i, k] + " ");
                }
                Console.WriteLine();
            }
        }

        public int insert_figure(Figure fig, int x, int y)
        {
            int code = 0;
            for (int i = 0; i < fig.tab.GetLength(0); i++)
                for (int k = 0; k < fig.tab.GetLength(1); k++)
                {
                    if (x + i < ptr.GetLength(0)&& y+k<ptr.GetLength(1))
                    {
                        if (fig.tab[i, k] != 0)
                            if (ptr[x + i, y + k] == 0)
                                ptr[x + i, y + k] = fig.tab[i, k];
                            
                    }
                    //if (ptr[x + i-1, y + k] == fig.code&& x+i==ptr.GetLength(0)) code = 1;
                }

            for (int i = 0; i < fig.size; i++)
                for (int k = 1; k < fig.tab.GetLength(1); k++)
                    if (fig.tab[fig.size-k,i]==fig.code&&x+fig.size-k+1 >= ptr.GetLength(0))
                    {
                        code = 1;
                        break;
                    }


            if (code==1)
            {
                //fig.falling = false;
                //fig.code = (byte)(255 - fig.code);
                //fig.change_code();
                for (int iz = 0; iz < fig.size; iz++)
                    for (int ik = 0; ik < fig.size; ik++)
                        if (fig.tab[ik, iz] != 0)
                            ptr[ik + x, iz + y] = (byte)(255 - fig.code);//fig.code;
                return 1;
            }


            for (int i = 0; i < fig.tab.GetLength(0); i++)
                for (int k = 0; k < fig.tab.GetLength(1); k++)
                    if (k+x+1<ptr.GetLength(0) && ptr[k + x, i + y] == fig.code)
                        if (ptr[k + x + 1, i + y] >200) //!= fig.code && ptr[k+x+1,i+y]!=0)
                        {
                            //int aaaaaaaaa = ptr[k + x + 1, i + y];
                            //fig.falling = false;
                            //fig.code = (byte)(255 - fig.code);
                            //fig.change_code();
                            for (int iz = 0; iz < fig.size; iz++)
                                for (int ik = 0; ik < fig.size; ik++)
                                    if (fig.tab[ik,iz]!=0) 
                                        ptr[ik + x, iz + y] = (byte)(255 - fig.code);//fig.code;
                            return 1;
                        }
            return 0;
        }
        public void randomise()
        {
            Random ran=new Random();
            for(int i=0;i<ptr.GetLength(0);i++)
                for (int k = 0; k < ptr.GetLength(1);k++)
                    if(ran.Next(2)==0)
                        ptr[i,k] = (byte)ran.Next(1, 4);
                    
        }
        public void test_line()
        {
            int counter=0;
            for(int i=ptr.GetLength(0)-1; i>=0;i--) 
            {
                for(int k=0;k<ptr.GetLength(1);k++)
                    if (ptr[i, k] != 0)
                        counter++;
                if (counter == ptr.GetLength(1))
                {
                    for (int k = 0; k < ptr.GetLength(1); k++)
                        ptr[i, k] = 255;
                    counter = 0;
                }
                else
                    counter = 0;

            }
            for (int i = ptr.GetLength(0) - 1; i >= 0; i--)
                if (ptr[i, 0] == 255)
                    for (int z = i; z >= 0; z--)
                    for (int k = 0; k < ptr.GetLength(1); k++)
                    if (z - 1 >= 0)
                        ptr[z, k] = ptr[z - 1, k];
                    else
                        ptr[z, k] = 0;
        }
        public void clear()
        {
            for (int i = ptr.GetLength(0) - 1; i >= 0; i--)
                for (int k = 0; k < ptr.GetLength(1); k++)
                    if (ptr[i,k]<10)
                    ptr[i,k] = 0;
        }
    }
}
