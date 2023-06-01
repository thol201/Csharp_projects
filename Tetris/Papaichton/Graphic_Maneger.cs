using Program;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papaichton
{
    public class Graphic_Maneger
    {
        int size = 30;
        Panel MainDisplay;
        Panel SecondaryDisplay;
        public Map map;
        int xsize;
        int ysize;
        SolidBrush bruh=new(Color.Red);
        Rectangle rectangle= new Rectangle();
        Dictionary<int, Color> colordic = new();


        public Graphic_Maneger() { }
        public Graphic_Maneger(Panel p1, Panel p2)
        {
            MainDisplay = p1;
            SecondaryDisplay = p2;
            colordic.Add(0,Color.Red);
            colordic.Add(1,Color.Gray);
            colordic.Add(2,Color.Green);
            colordic.Add(3,Color.Blue);
            colordic.Add(4,Color.Violet);
            colordic.Add(5,Color.White);
            colordic.Add(6,Color.Brown);
            colordic.Add(7,Color.Cyan);
            colordic.Add(8,Color.DarkMagenta);
            colordic.Add(9,Color.Orange);
        }
       

        public void updateMain(Graphics g)
        {

            //int i = 0, k = 0;
            //Rectangle rec = new(i * size, k * size, size, size);
            //g.DrawRectangle(new Pen(Color.Red), rec);
            //MessageBox.Show("updating");
            //Pen? pen = new Pen(Color.Black) ;
            xsize= (MainDisplay.Width - 5)/map.ptr.GetLength(1)-5;
            ysize= (MainDisplay.Height - 5)/map.ptr.GetLength(0)-5;

            //Graphics ag=g;
            MainDisplay.SuspendLayout();


            //MainDisplay.Update();
            for (int i = 0; i < map.ptr.GetLength(0); i++)
            {
                for (int k = 0; k < map.ptr.GetLength(1); k++)
                {
                    if (map.ptr[i, k] != 0/*&& map.ptr[i, k]<100*/)
                    {
                        int color;
                        color = map.ptr[i, k];
                        if (color > 200)
                            color = 255 - color;
                        bruh.Color = colordic[color];
                        //Rectangle rec = new Rectangle(5+k*(5+xsize),5+i* (5+ysize), xsize, ysize);
                        rectangle.X = 5 + k * (5 + xsize);
                        rectangle.Y = 5 + i * (5 + ysize);
                        rectangle.Width = xsize;
                        rectangle.Height = ysize;
                        g.FillRectangle(bruh, rectangle);
                    }
                   
                }
            }
            MainDisplay.ResumeLayout();
            //MainDisplay.Invalidate();
        }

        public void updateSecondary(Graphics g,Figure fig)
        {
            for (int i = 0; i < fig.tab.GetLength(0); i++)
                for (int k = 0; k < fig.tab.GetLength(1); k++)
                {
                    if (fig.tab[i, k] == 0)
                        continue;
                    int color;
                    color = fig.code;
                    if (color > 200)
                        color = 255 - color;
                    //Rectangle rec = new Rectangle(5 + (k+1) * (5 + size), 5 + (i+1) * (5 + size), size, size);
                    bruh.Color = colordic[color];
                    rectangle.X = 5 + (k+1) * (5 + size);
                    rectangle.Y = 5 + (i+1) * (5 + size);
                    rectangle.Width = size;
                    rectangle.Height = size;
                    g.FillRectangle(bruh,rectangle);
                }
        }

        public void start()
        {
            MainDisplay.BackColor= Color.Black;
            SecondaryDisplay.BackColor= Color.Black;
            
            MainDisplay.Height = map.rows * ( size + 5 )+5;
            MainDisplay.Width = map.cols * ( size + 5 )+5;
        }
    }
}
