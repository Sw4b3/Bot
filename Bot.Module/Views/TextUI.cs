using System;
using System.Windows.Forms;

namespace Bot.Module
{
    public partial class TextUI : Form
    {
        double xpos, ypos;

        public TextUI()
        {
            InitializeComponent();
            SetPostion();
        }

        private void SetPostion()
        {
            xpos = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2);
            ypos = (Screen.PrimaryScreen.WorkingArea.Height/2) - (this.Height / 2);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Convert.ToInt32(xpos);
            this.Top = Convert.ToInt32(ypos);
        }

        public void SetText(String reply) {
            textBox1.Text=(reply);
        }
    }
}
