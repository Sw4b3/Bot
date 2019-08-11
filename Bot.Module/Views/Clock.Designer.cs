namespace VirtualAssistant
{
    partial class Clock
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
            this.labelmonth = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labeltime
            // 
            this.labeltime.BackColor = System.Drawing.Color.Transparent;
            this.labeltime.Font = new System.Drawing.Font("Digital-7", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltime.ForeColor = System.Drawing.Color.White;
            this.labeltime.Location = new System.Drawing.Point(11, 135);
            this.labeltime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labeltime.Name = "labeltime";
            this.labeltime.Size = new System.Drawing.Size(290, 53);
            this.labeltime.TabIndex = 29;
            this.labeltime.Text = "00:00:00";
            this.labeltime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelmonth
            // 
            this.labelmonth.BackColor = System.Drawing.Color.Transparent;
            this.labelmonth.Font = new System.Drawing.Font("Digital-7", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelmonth.ForeColor = System.Drawing.Color.White;
            this.labelmonth.Location = new System.Drawing.Point(18, 89);
            this.labelmonth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelmonth.Name = "labelmonth";
            this.labelmonth.Size = new System.Drawing.Size(283, 46);
            this.labelmonth.TabIndex = 28;
            this.labelmonth.Text = "Month ";
            this.labelmonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Clock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.ClientSize = new System.Drawing.Size(312, 261);
            this.Controls.Add(this.labeltime);
            this.Controls.Add(this.labelmonth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Clock";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Clockcs";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.Load += new System.EventHandler(this.Clockcs_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labeltime;
        private System.Windows.Forms.Label labelmonth;
        private System.Windows.Forms.Timer timer1;
    }
}