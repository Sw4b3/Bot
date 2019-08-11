using Bot.Core.Interfaces;
using System;
using System.Collections;
using System.Drawing;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace Bot.Core.Desktop
{
    public partial class MainForm : Form
    {
        SpeechRecognitionEngine engine;
        ISpeechController _speechController;
        IModuleController _moduleController;
        IRecogntionController _recogntionController;
        Stack memory = new Stack();
        bool IS_SLEEPING;
        int xpos, ypos;

        public MainForm(ISpeechController speechController, IRecogntionController recogntionController, IModuleController moduleController)
        {
            _speechController = speechController;
            _recogntionController = recogntionController;
            _moduleController = moduleController;
            engine = _recogntionController.getInstance();
            engine.SpeechRecognized += engine_speechRecognized;
            _moduleController.ShowAll();
            InitializeComponent();
            Fullscreen();
        }


        private void engine_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //textbox1.AppendText(e.Result.Text);
            switch (e.Result.Text)
            {
                case "go queit":
                case "go offline":
                    _speechController.Speak("Going quiet");
                    IS_SLEEPING = true;
                    break;
                case "go loud":
                case "go online":
                    _speechController.Speak("Listening again");
                    IS_SLEEPING = false;
                    break;
            }

            if (!IS_SLEEPING)
            {
                switch (e.Result.Text)
                {
                    case "minimize":
                        this.WindowState = FormWindowState.Minimized;
                        _moduleController.SetAISpeechLog("Minimizing");
                        _speechController.Speak("Minimizing");
                        _moduleController.HideAll();
                        break;
                    case "maximize":
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                        //notifyIcon1.Visible = false;
                        _moduleController.SetAISpeechLog("Expanding");
                        _speechController.Speak("Expanding");
                        break;
                    case "show chatlog":
                        _moduleController.ShowChatlog();
                        break;
                    case "show time":
                    case "show clock":
                        _moduleController.ShowClock();
                        break;
                    case "hide chatlog":
                        _moduleController.HideChatlog();
                        break;
                    case "hide clock":
                        _moduleController.HideClock();
                        break;
                    case "show all":
                        _moduleController.ShowAll();
                        break;
                    case "hide all":
                        _moduleController.HideAll();
                        break;
                    case "put into sleep mode":
                        _speechController.Speak("Are you sure you want to put in Sleep mode");
                        break;
                    case "shutdown":
                        _speechController.Speak("Are you sure");
                        break;
                    case "change theme":
                        this.SetTheme();
                        break;
                    case "change theme back":
                        this.SetThemeBack();
                        break;
                    case "yes":
                        switch (memory.Peek())
                        {
                            case "shutdown":
                                _speechController.Speak("Shutting down");
                                this.Close();
                                break;
                            case "sleep":
                                _speechController.Speak("Putting Computer into Sleep mode");
                                //     Application.SetSuspendState(PowerState.Suspend, true, true
                                break;
                        }
                        break;
                    case "no":
                        break;

                };
            }
            memory.Push(e.Result.Text);
        }
        public void Fullscreen()
        {
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            xpos = (Width / 2) - (pictureBox1.Width / 2);
            ypos = (Height / 2) - (pictureBox1.Height / 2);
            this.pictureBox1.Location = new Point(xpos, ypos);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        public void SetTheme()
        {
            try
            {
                pictureBox1.Image = Image.FromFile("C: \\Users\\Andrew\\source\\repos\\VirtualAssiant\\VirtualAssiant\\Resources\\ezgif.com-crop.gif");
                pictureBox1.Size = new Size(800, 600);
                xpos = (Width / 2) - (pictureBox1.Width / 2);
                ypos = (Height / 2) - (pictureBox1.Height / 2);
                this.pictureBox1.Location = new Point(xpos, ypos);
                this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(25, 12, 64);
                this.BackColor = System.Drawing.Color.FromArgb(25, 12, 64);
            }
            catch (Exception)
            {
            }
        }

        public void SetThemeBack()
        {
            try
            {
                pictureBox1.Image = Image.FromFile("C: \\Users\\Andrew\\source\\repos\\VirtualAssiant\\VirtualAssiant\\Resources\\motion.gif");
                pictureBox1.Size = new Size(415, 327);
                xpos = (Width / 2) - (pictureBox1.Width / 2);
                ypos = (Height / 2) - (pictureBox1.Height / 2);
                this.pictureBox1.Location = new Point(xpos, ypos);
                this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
                this.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            }
            catch (Exception)
            {
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Maximized;
            notifyIcon1.Visible = false;
        }


    }
}

