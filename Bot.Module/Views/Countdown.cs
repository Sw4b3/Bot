using System;
using System.Windows.Forms;
using System.Media;

namespace Bot.Module
{
    public partial class Countdown : Form
    {
        double xpos, ypos;
        double timeLeft;
        int time;

        public Countdown()
        {
            SetPostion();
            InitializeComponent();
          
        }

        public void StartCountdown(String utterance)
        {
            if (utterance.Contains("minutes"))
            {
                time = Convert.ToInt32(utterance.Replace("minutes", ""));
                timeLeft = time * 60;
            }
            if (utterance.Contains("seconds"))
            {
                time = Convert.ToInt32(utterance.Replace("seconds", ""));
                timeLeft = time;
            } 
            timer.Start();
        }

        public void StopCoutdown()
        {
            try
            {
                timer.Stop();
                this.Dispose();
            }
            catch (Exception) {
                //_speechController.Speak("There is no timer to stop");
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
                if (timeLeft > 0)
            {
                timeLeft -=1;
                var timespan = TimeSpan.FromSeconds(timeLeft);
                labeltime.Text = timespan.ToString(@"hh\:mm\:ss");
            }
            else
            {
                StopCoutdown();
                //speechController.Speak("Time's up!");
                new SoundPlayer(".\\pure_bell.wav").Play();
            }
        }

        private void Countdown_Leave(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SetPostion()
        {
            xpos = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2);
            ypos = (Screen.PrimaryScreen.WorkingArea.Height * 0.8) - (this.Height / 2);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Convert.ToInt32(xpos);
            this.Top = Convert.ToInt32(ypos);
        }
    }
}
