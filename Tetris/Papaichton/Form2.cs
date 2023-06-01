using Program;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Papaichton
{


    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Input_Maneger inp;

        public void setValues()
        {
            RR.Text = Convert.ToChar(inp.Action_Key_Rotate_Right).ToString();
            RL.Text = Convert.ToChar(inp.Action_Key_Rotate_Left).ToString();
            MR.Text = Convert.ToChar(inp.Action_Key_Move_Right).ToString();
            ML.Text = Convert.ToChar(inp.Action_Key_Move_Left).ToString();
            MD.Text = Convert.ToChar(inp.Action_Key_Move_Down).ToString();
            MAD.Text = "Spacebar";
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(RR.Text!="")
                inp.Action_Key_Rotate_Right = (int)Convert.ToChar(RR.Text);
            if (RL.Text != "")
                inp.Action_Key_Rotate_Left = (int)Convert.ToChar(RL.Text);
            if (MR.Text != "")
                inp.Action_Key_Move_Right = (int)Convert.ToChar(MR.Text);
            if (ML.Text != "")
                inp.Action_Key_Move_Left = (int)Convert.ToChar(ML.Text);
            if (MD.Text != "")
                inp.Action_Key_Move_Down = (int)Convert.ToChar(MD.Text);

            this.Close();

        }
    }
}
