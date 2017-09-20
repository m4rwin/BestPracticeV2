using System;
using System.Windows.Forms;

namespace MyTimer
{
    public partial class Form1 : Form
    {
        Timer MyTimer;
        bool tick;
        bool buttonStatus;

        public Form1()
        {
            InitializeComponent();
            MyTimer = new Timer();
            MyTimer.Interval = 1000;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);

            button.Click += button_Click;

            button.Text = "START";
            label.Text = "Ready for Start";

            tick = true;
            buttonStatus = true;
        }

        void button_Click(object sender, EventArgs e)
        {
            if (buttonStatus)
            {
                MyTimer.Start();
                button.Text = "STOP";
            }
            else
            {
                MyTimer.Stop();
                button.Text = "START";
                label.Text = "...";
            }
            buttonStatus = !buttonStatus;
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            label.Text = tick ? "TIK" : "TAK";
            tick = !tick;
        }
    }
}
