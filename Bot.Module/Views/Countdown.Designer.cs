namespace Bot.Module
{
    partial class Countdown
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labeltime = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labeltime
            // 
            this.labeltime.BackColor = System.Drawing.Color.Transparent;
            this.labeltime.Font = new System.Drawing.Font("Digital-7", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltime.ForeColor = System.Drawing.Color.White;
            this.labeltime.Location = new System.Drawing.Point(-5, -1);
            this.labeltime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labeltime.Name = "labeltime";
            this.labeltime.Size = new System.Drawing.Size(290, 53);
            this.labeltime.TabIndex = 30;
            this.labeltime.Text = "00:00:00";
            this.labeltime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Countdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(284, 56);
            this.Controls.Add(this.labeltime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Countdown";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Countdown";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Leave += new System.EventHandler(this.Countdown_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labeltime;
        private System.Windows.Forms.Timer timer;
    }
}