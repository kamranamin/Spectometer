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
       
          
          

        
         
            InitializeComponent();
          
        }



        int i = 0;
       
        private int startPosX;
        private int startPosY;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startPosY -= 5;
            //If window is fully visible stop the timer
            if (startPosY < Screen.PrimaryScreen.WorkingArea.Height - Height)
            {
                timer1.Stop();
                System.Threading.Thread.Sleep(2000);
                base.Dispose();
            }
            else
                SetDesktopLocation(startPosX, startPosY);
        
        //i += 10;


        //this.timer1.Interval = 100;
        //if (base.Opacity >= 0.1)
        //{
        //    base.Opacity -= 0.1;
        //}
        //else
        //{
        //    this.timer1.Enabled = false;
        //    base.Dispose();
        //}
    }
        protected override void OnLoad(EventArgs e)
        {
            startPosX = Screen.PrimaryScreen.WorkingArea.Width - Width;
            startPosY = Screen.PrimaryScreen.WorkingArea.Height;
            SetDesktopLocation(startPosX, startPosY);
            base.OnLoad(e);
            // Begin animation
            timer1.Start();
        }
        private void New_Load(object sender, EventArgs e)
        {
          //  this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Width, Screen.PrimaryScreen.WorkingArea.Bottom - this.Height);
        }
    }
}
