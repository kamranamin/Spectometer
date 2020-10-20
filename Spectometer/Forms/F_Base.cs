using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class F_Base : Form
    {
        private bool _is_m_down;
        private Point _p_down;
        private bool _is_max;
        private Point _last_location;
        private Size _last_size;

        public F_Base()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

        }
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }


        private void F_Base_Load(object sender, EventArgs e)
        {
            _pb_form_icon.Image = Spectometer.Properties.Resources.logo_32 ;
            _lbl_form_text.Text = Text;
        }

        private void _btn_close_MouseMove(object sender, MouseEventArgs e)
        {
            _btn_close.Image = Spectometer.Properties.Resources.Close_02;

        }

        private void _btn_close_MouseLeave(object sender, EventArgs e)
        {
            _btn_close.Image = Spectometer.Properties.Resources.Close_01;
        }

        private void _btn_max_min_MouseMove(object sender, MouseEventArgs e)
        {
            _btn_max_min.Image = Spectometer.Properties.Resources.Tiles2;
        }

        private void _btn_max_min_MouseLeave(object sender, EventArgs e)
        {
            _btn_max_min.Image = Spectometer.Properties.Resources.Tiles;
        }

        private void _btn_min_MouseMove(object sender, MouseEventArgs e)
        {
            _btn_min.Image = Spectometer.Properties.Resources.Minus2;

        }

        private void _btn_min_MouseLeave(object sender, EventArgs e)
        {
            _btn_min.Image = Spectometer.Properties.Resources.Minus;

        }
        private void pnlUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_is_max)
            {
                _is_m_down = true;
                this._p_down = new Point(e.X, e.Y);
            }
        }

        private void pnlUp_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._is_m_down)
            {
                this.Left = this.Left + e.X - this._p_down.X;
                this.Top = this.Top + e.Y - this._p_down.Y;
            }
        }

        private void pnlUp_MouseUp(object sender, MouseEventArgs e)
        {
            this._is_m_down = false;
        }

        private void _btn_close_Click(object sender, EventArgs e)
        {
           
            Close();

        }

        private void _btn_max_min_Click(object sender, EventArgs e)
        {
            if (_is_max)
            {
                Location = _last_location;
                Size = _last_size;
            }
            else
            {
                _last_location = Location;
                _last_size = Size;
                Size = new Size(Screen.PrimaryScreen.WorkingArea.Width,
                    Screen.PrimaryScreen.WorkingArea.Height);
                Location = new Point(0, 0);
            }
            _is_max = !_is_max;
        }

        private void _btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

    }
}
