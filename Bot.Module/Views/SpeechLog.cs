﻿using System;
using System.Windows.Forms;

namespace Bot.Module
{
    public partial class SpeechLog : Form
    {
        double xpos, ypos;

        public SpeechLog()
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

        public void SetText(string reply) {
            textBox1.Text=(reply);
        }
    }
}