using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class SplashScreen : Form

    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer tcol1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer tLable2 = new System.Windows.Forms.Timer();

        bool fadeIn = true;
        bool fadeOut = false;
        public SplashScreen()
        {

            ExtraFormSettings();
                
            SetAndStartTimer();
            InitializeComponent();
            tcol1.Tick += Tcol1_Tick;
            tcol1.Interval = 100;
            tcol1.Start();
            tLable2.Tick += TLable2_Tick;
            tLable2.Interval = 300;
            tLable2.Start();
        }

        private void TLable2_Tick(object sender, EventArgs e)
        {
            label2.Refresh();
            label6.Refresh();
        }

        private void movePic(PictureBox pb)
        {
            while(pb.Location.Y<200)
            {
                pb.Location = new Point(pb.Location.X, pb.Location.Y + 1);
            }
        }
        private void Tcol1_Tick(object sender, EventArgs e)
        {
            //movePic(pictureBox1);
            //movePic(pictureBox2);
            //movePic(pictureBox3);
            //   pictureBox1.Location = new Point (pictureBox1.Location.X , pictureBox1.Location.Y+2);

            label1.Refresh();
         
        }
      
        private void SetAndStartTimer()
        {
            t.Interval = 6000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        private void ExtraFormSettings()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 1;
           // this.BackgroundImage = Spectometer .Properties.Resources.Untitled_12jpg ;
          
        }


        void t_Tick(object sender, EventArgs e)
        {

            // Fade in by increasing the opacity of the splash to 1.0
            //if (fadeIn)
            //{
            //    if (this.Opacity < 1.0)
            //    {
            //        this.Opacity += 0.05;
            //    }
            //    // After fadeIn complete, begin fadeOut
            //    else
            //    {
            //      //  System.Threading.Thread.Sleep(5000);
            //        fadeIn = false;
            //        fadeOut = true;
            //    }
            //}
            //else if (fadeOut) // Fade out by increasing the opacity of the splash to 1.0
            //{
            //    if (this.Opacity > 0)
            //    {
            //        this.Opacity -= 0.05;
            //    }
            //    else
            //    {
            //        fadeOut = false;
            //    }
            //}

            //// After fadeIn and fadeOut complete, stop the timer and close this splash.
            //if (!(fadeIn || fadeOut))
            //{
            //    t.Stop();
            //    this.Close();
            //}
            t.Stop();
            tcol1.Stop();
            this.Close();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            //this.TransparencyKey = Color.Turquoise;
            //this.BackColor = Color.Turquoise;
        }
        private float GradientStart = 0;
        private float Delta = 5;
        private void ShadeRect(Graphics gr, float xmin, float xmax)
        {
            using (LinearGradientBrush br = new LinearGradientBrush(
                new PointF(xmin, 0), new PointF(xmax, 0),
                Color.Red, Color.Red))
            {
                br.WrapMode = WrapMode.Tile;
                ColorBlend color_blend = new ColorBlend();
                color_blend.Colors = new Color[] { Color.Yellow  , Color.Orange, Color.Red, Color.Magenta, Color.Blue, Color.Green, Color.Yellow  };
                color_blend.Positions = new float[] {0.0f,
                1.0f / 6.0f,
                2.0f / 6.0f,
                3.0f / 6.0f,
                4.0f / 6.0f,
                5.0f / 6.0f,
                1.0f
            };

                br.InterpolationColors = color_blend;
                gr.FillRectangle(br, label1.ClientRectangle);
            }
        }
      
        private void label1_Paint(object sender, PaintEventArgs e)
        {
            int wid = label1.ClientSize.Width;
            //   e.Graphics.Clear(Color.White);

            // Make the gradient brush.
            ShadeRect(e.Graphics, GradientStart, GradientStart + wid);
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new PointF(GradientStart, 0),
                new PointF(GradientStart + wid, 0),
                Color.Gray, Color.Red))
            {
                brush.WrapMode = WrapMode.Tile;
                ColorBlend color_blend = new ColorBlend();
                color_blend.Colors = new Color[]
                {
                    Color.Blue, Color.Red,
                    Color.Transparent , Color.Green , Color.Gold
                };
                color_blend.Positions =
                    new float[] { 0, 0.4f, 0.5f, 0.6f, 1 };
                brush.InterpolationColors = color_blend;

                // Use the brush to draw some text.
                using (Font font = new Font("Times New Roman", 16, FontStyle.Bold))
                {
                    using (StringFormat string_format = new StringFormat())
                    {
                        string_format.Alignment = StringAlignment.Center;
                        string_format.LineAlignment = StringAlignment.Center;
                        e.Graphics.DrawString("",
                            font, brush,
                            label1.ClientSize.Width / 2,
                            label1.ClientSize.Height / 2,
                            string_format);
                    }
                }
            }

            // Increase the start position.
            GradientStart += Delta;
            if (GradientStart >= wid) GradientStart = 0;
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            // Clear the background.
            int wid = label2 .ClientSize.Width;
            e.Graphics.Clear(Color.White);

            // Make the gradient brush.
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new PointF(GradientStart, 0),
                new PointF(GradientStart + wid, 0),
                Color.Red, Color.Red))
            {
                brush.WrapMode = WrapMode.Tile;
                ColorBlend color_blend = new ColorBlend();
                color_blend.Colors = new Color[]
                {
                    Color.Black, Color.Red ,
                    Color.LightBlue, Color.Black, Color.Green 
                };
                color_blend.Positions =
                    new float[] { 0, 0.4f, 0.5f, 0.6f, 1 };
                brush.InterpolationColors = color_blend;

                // Use the brush to draw some text.
                using (Font font = new Font("Times New Roman", 16, FontStyle.Bold))
                {
                    using (StringFormat string_format = new StringFormat())
                    {
                        string_format.Alignment = StringAlignment.Center;
                        string_format.LineAlignment = StringAlignment.Center;
                        e.Graphics.DrawString( "Spectroscopy Station" ,
                            font, brush,
                            label2 .ClientSize.Width / 2,
                            label2 .ClientSize.Height / 2,
                            string_format);
                    }
                }
            }

            // Increase the start position.
            GradientStart += Delta;
            if (GradientStart >= wid) GradientStart = 0;
        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            // Clear the background.
            int wid = label2.ClientSize.Width;
            e.Graphics.Clear(Color.White);

            // Make the gradient brush.
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new PointF(GradientStart, 0),
                new PointF(GradientStart + wid, 0),
                Color.Red, Color.Red))
            {
                brush.WrapMode = WrapMode.Tile;
                ColorBlend color_blend = new ColorBlend();
                color_blend.Colors = new Color[]
                {
                    Color.Black, Color.Red ,
                    Color.LightBlue, Color.Black, Color.Green
                };
                color_blend.Positions =
                    new float[] { 0, 0.4f, 0.5f, 0.6f, 1 };
                brush.InterpolationColors = color_blend;

                // Use the brush to draw some text.
                using (Font font = new Font("Times New Roman", 16, FontStyle.Bold))
                {
                    using (StringFormat string_format = new StringFormat())
                    {
                        string_format.Alignment = StringAlignment.Center  ;
                        string_format.LineAlignment = StringAlignment.Center  ;
                        e.Graphics.DrawString( "SoftWare",
                            font, brush,
                            label2.ClientSize.Width / 2,
                            label2.ClientSize.Height / 2,
                            string_format);
                    }
                }
            }

            // Increase the start position.
            GradientStart += Delta;
            if (GradientStart >= wid) GradientStart = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
