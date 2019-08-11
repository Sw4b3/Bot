using System;
using System.Windows.Forms;

namespace VirtualAssistant
{
    public partial class Clock : Form
    {
        double xpos, ypos;
        public Clock()
        {
            InitializeComponent();
            setPostion();
        }

        private void setPostion()
        {
            xpos = (Screen.PrimaryScreen.WorkingArea.Width /2) - (this.Width / 2);
            ypos = (Screen.PrimaryScreen.WorkingArea.Height * 0.8) - (this.Height / 2);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Convert.ToInt32(xpos);
            this.Top = Convert.ToInt32(ypos);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
            labeltime.Text = currentTime;
           
        }

        private void Clockcs_Load(object sender, EventArgs e)
        {
            System.DateTime nowtime = System.DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                nowtime = nowtime.AddMonths(1);
                labelmonth.Text = nowtime.ToString("dd MMMM");
            }
        }
    }
}
