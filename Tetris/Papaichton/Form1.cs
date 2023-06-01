using Program;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Papaichton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            StartGame();
        }

        ////////////////////////////////////////////////
        ///////  Graphic Design Is My Passion  /////////
        ////////////////////////////////////////////////
        // Even After Threading Display Function Panel Is Too Slow To Paint Multiple Object At Once
        // It Causes significant Performance Drop After A While
        // Nevermind Im Just Stupid
        // Invalidate Method Calls Paint Event
        // So Using Invalidate Inside Paint Event calls The Same Event Again
        // So After minute There Is A Cascade Of Events To Handle
        // I'll Leave It As It Shows My IQ Of Room Temperature In Alaska




        void StartGame()
        {
            graphic_maneger = new(panel1, panel2);
            gamemen = new();
            graphic_maneger.map = gamemen.map;
            graphic_maneger.start();
            inputs = new Input_Maneger(gamemen);
            gamemen.set_function("Start", disp_intro);
            gamemen.set_function("Stop", disp_outro);
            gamemen.set_function("Display", callupdate);
            gamemen.set_function("Score", updateScore);
            button1.Show();
            gamemen.start();
            T = new Thread(new ThreadStart(start_thread));
            stopTimer();
        }

        public void updateScore()
        {
            // Safe Thread Update
            var threadParameters = new System.Threading.ThreadStart(delegate { setScore(gamemen.points.ToString()); });
            var thread2 = new System.Threading.Thread(threadParameters);
            thread2.Start();

            //ScoreLabal.Text = "Score: " + gamemen.points.ToString();
            //setScore(gamemen.points.ToString());
            //ScoreLabal.Refresh();
        }
        void setScore(string s)
        {
            if (ScoreLabal.InvokeRequired)
            {
                Action safeWrite = delegate { setScore(s); };
                ScoreLabal.Invoke(safeWrite);
            }
            else
                ScoreLabal.Text = "Score: " + s;
        }
        public void disp_intro()
        {
            Stat.Text = "Welcome In TERTIS \nDon't Compare It To The Game TETRIS\nThis Is Just A Cheap Copy Of This Game\nTo Play Press \"Start\" Button";
            //MessageBox.Show("intro");
        }

        void Ending(string s)
        {
            if (Stat.InvokeRequired)
            {
                Action safeWrite = delegate { Ending(s); };
                Stat.Invoke(safeWrite);
            }
            else
                Stat.Text = s;
        }
        public void disp_outro()
        {
            Ending("GAME OVER\nIf You Want To Play Again\nSelect Restart In Tool Menu");
        }

        public void callupdate()
        {

            //panel1.Paint += new PaintEventHandler(panel1_Paint);
            if (T != null&&!T.IsAlive)
            {
                T = new Thread(new ThreadStart(start_thread));
                T.Start();
            }
        }

        void start_thread()
        {
            panel1.Invalidate();
            //panel1.Paint += new PaintEventHandler(panel1_Paint);
            //panel2.Paint += new PaintEventHandler(panel2_Paint);
            panel2.Invalidate();
        }

        Thread T;
        Game_Menager gamemen = new();
        Graphic_Maneger? graphic_maneger;
        public Input_Maneger inputs;

        protected override CreateParams CreateParams  // I Have No Idea How It Works But Panel Stopped Flickering 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                cp.ExStyle |= 0x00080000; //WS_EX_LAYERED. Transparency key
                return cp;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            graphic_maneger.updateMain(e.Graphics);
            
            //panel1.Invalidate();
            //this.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (gamemen.prev == null)
                return;
            
            graphic_maneger.updateSecondary(e.Graphics,gamemen.prev);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            inputs.callEvent(e.KeyValue);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ScoreLabal_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Label lab = (Label)sender;
            lab.Text = "Score: " + gamemen.points.ToString();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamemen.Reset();
            graphic_maneger = new(panel1, panel2);
            graphic_maneger.map = gamemen.map;
            graphic_maneger.start();
            gamemen.points = 0;
            updateScore();
            StartGame();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopTimer();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startTimer();
        }

        void startTimer()
        {
            gamemen.UpdateTimer.Start();
            gamemen.locked = false;
        }

        void stopTimer()
        {
            gamemen.UpdateTimer.Stop();
            gamemen.locked = true;
        }

        private void changeControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopTimer();
            Form2 f = new Form2();
            f.inp = inputs;
            f.Show();
            f.setValues();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startTimer();
            button1.Hide();
            this.Focus();
            Stat.Text = "You can Stop/Start/Reset Using Tools Menu\nOr You Can Change Controls";
        }
    }
}