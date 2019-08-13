using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bot.Module
{
    public partial class Chatlog : Form
    {
        double xpos, ypos;

        public Chatlog()
        {
            SetPostion();
            InitializeComponent();

        }

        private void SetPostion()
        {
            xpos = (Screen.PrimaryScreen.WorkingArea.Width * 0.95) - (this.Width / 2);
            ypos = (Screen.PrimaryScreen.WorkingArea.Height * 0.15) - (this.Height / 2);
            this.Location = new Point(Convert.ToInt32(xpos), Convert.ToInt32(ypos));
        }

        public void SetAIChatlog(string text)
        {
            textBox1.AppendText("AI: " + text + Environment.NewLine);
        }

        public void SetUserChatlog(string text)
        {
            textBox1.AppendText("User: " + text + Environment.NewLine);
        }

    }
}
