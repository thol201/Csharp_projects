using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Map_Printer
    {
        Map mapptr = null;
        public Map_Printer() { }
        public Map_Printer(Map k) { mapptr= k;}

        public void print()
        {
            for (int i = 0; i < mapptr.ptr.GetLength(0); i++)
            {
                for (int k = 0; k < mapptr.ptr.GetLength(1); k++)
                {
                    if(mapptr.ptr[i, k]!=0)
                        if (mapptr.ptr[i,k]>100)
                            Console.Write(mapptr.ptr[i, k]);
                        else
                            Console.Write(" "+mapptr.ptr[i, k] + " ");

                    else
                        Console.Write(" _ ");

                }
                Console.WriteLine();
            }
        }
    }
}
