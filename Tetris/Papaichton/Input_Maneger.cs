using Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papaichton
{
    public class Input_Maneger
    {
        Game_Menager menager;
        public int Action_Key_Rotate_Right = 'R';
        public int Action_Key_Rotate_Left = 'Q';
        public int Action_Key_Move_Right = 'D';
        public int Action_Key_Move_Left = 'A';
        public int Action_Key_Move_Down = 'S';
        public int Action_Key_Move_Full_Down = (int)ConsoleKey.Spacebar;

        public Input_Maneger(Game_Menager gam)
        {
            this.menager = gam;
        }
        Input_Maneger() { }

        public void callEvent(int val)
        {
            //MessageBox.Show(val.ToString());

            if ((val == (int)Keys.Escape)) { menager.Event(1); }
            else if ((val == Action_Key_Rotate_Right)) menager.Event(2);
            else if ((val == Action_Key_Rotate_Left)) menager.Event(3);
            else if ((val == Action_Key_Move_Right)) menager.Event(4);
            else if ((val == Action_Key_Move_Left)) menager.Event(5);
            else if ((val == Action_Key_Move_Down)) menager.Event(6);
            else if ((val == Action_Key_Move_Full_Down)) menager.Event(7);

        }


    }
}
