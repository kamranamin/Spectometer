using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spectometer
{
    public partial class New : Form 
    {
        public New()
        {
           

            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
       
          
          

        
         //   this.ClientSize = new System.Drawing.Size(909, 528);
            InitializeComponent();
        }



        int i = 0;
        private void New_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Width, Screen.PrimaryScreen.WorkingArea.Bottom - this.Height);
            this.timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i += 10;
          //  this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Width, Screen.PrimaryScreen.WorkingArea.Bottom - this.Height+i);

            this.timer1.Interval = 300;
            if (base.Opacity >= 0.1)
            {
                base.Opacity -= 0.1;
            }
            else
            {
                this.timer1.Enabled = false;
                base.Dispose();
            }
        }
    }
}
