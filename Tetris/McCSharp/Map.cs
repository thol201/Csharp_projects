using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class Map
    {
        public int mapx;
        public int mapy;
        public Game_Menager menager;
        public byte[,] ptr = null;
        public Map() {
            mapx= 20;
            mapy= 10;
            ptr = new byte[mapx,mapy];
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

        public int get_instant_Collison(Figure? fig)
        {

            for (int i = 0; i < fig.size; i++)
                for (int k = 0; k < fig.size; k++)
                    if (fig.tab[i, k] != 0)
                        if (fig.x+i>=rows || fig.y + k >= cols || fig.y + k < 0  || ptr[fig.x+i, fig.y+k] != 0)
                            return 1;
            return 0;
        }
        public int get_Collision(Figure? fig,int direction,bool supervisor=false)
        {


            switch(direction)
            {
                case 0:  // down collision
                    for (int k = 0; k < fig.size; k++)
                        for (int i = fig.size-1; i >= 0; i--)  
                        {
                            if (fig.tab[i, k] != 0)
                            {
                                if (fig.x + 1 + i >= ptr.GetLength(0) || ptr[fig.x + i + 1, fig.y + k] != 0)
                                    return 0;
                                break;
                            }
                        }
                    break;
                case 1:  // colision right
                    for (int i = 0; i < fig.size; i++)
                        for (int k = fig.size - 1; k >= 0; k--)
                            if (fig.tab[i, k] != 0)
                            {
                                if (fig.y + 1 + k >= ptr.GetLength(1)  || ptr[fig.x + i, fig.y + 1 + k] != 0)
                                    return 0;
                                break;
                            }
                    break;
                case 2:   // colison left
                    for (int i = 0; i < fig.tab.GetLength(0); i++)
                        for (int k = 0; k < fig.tab.GetLength(1); k++)
                            if (fig.tab[i, k] != 0)
                            {
                                if (fig.y - 1 + k < 0 || ptr[fig.x + i, fig.y - 1 + k] != 0)
                                    return 0;
                                break;
                            }
                    break;
               
            }
            return 1;
        }

        public int insert_figure(Figure fig,bool supervisor=false)  
        {
            int code = 0;
            for (int i = 0; i < fig.tab.GetLength(0); i++)
                for (int k = 0; k < fig.tab.GetLength(1); k++)
                {
                    if (fig.x + i < ptr.GetLength(0)&& fig.y+k<ptr.GetLength(1))
                    {
                        if (fig.tab[i, k] != 0)
                            if (ptr[fig.x + i, fig.y + k] == 0)
                                ptr[fig.x + i, fig.y + k] = fig.tab[i, k];
                            
                    }
                }

            if(supervisor)
            code = get_Collision(fig, 0);

            if (code == 0)
            {
                if (supervisor&&menager.trigger == false)
                    return 3;
                if (supervisor)
                {
                    for (int i = 0; i < fig.tab.GetLength(0); i++)
                        for (int k = 0; k < fig.tab.GetLength(1); k++)
                            if (fig.tab[i, k] != 0)
                            {
                                ptr[fig.x + i, fig.y + k] = (byte)(255 - fig.code);
                            }
                    menager.trigger = false;
                }
                return 1;
            }
            if(code==1&&supervisor) 
            {
                menager.trigger = false;
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
        public bool test_line()
        {
            int counter=0;
            bool found=false;
            for(int i=ptr.GetLength(0)-1; i>=0;i--) 
            {
                for(int k=0;k<ptr.GetLength(1);k++)
                    if (ptr[i, k] != 0)
                        counter++;
                if (counter == ptr.GetLength(1))
                {
                    for (int k = 0; k < ptr.GetLength(1); k++)
                    {
                        ptr[i, k] = 255;
                        found= true;
                    }
                    counter = 0;
                }
                else
                    counter = 0;

            }
            if (found == false)
                return false;
            for (int i = ptr.GetLength(0) - 1; i >= 0; i--)
                if (ptr[i, 0] == 255)
                    for (int z = i; z >= 0; z--)
                    for (int k = 0; k < ptr.GetLength(1); k++)
                    if (z - 1 >= 0)
                        ptr[z, k] = ptr[z - 1, k];
                    else
                        ptr[z, k] = 0;
            return true;
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
