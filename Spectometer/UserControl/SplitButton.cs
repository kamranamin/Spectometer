﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectometer.UserControl
{
    class SplitButton:FlatButton 
    {
        readonly Button _arrowButton;

        ContextMenuStrip _dropdownMenu;
        Form _infoDialog;
        public delegate void onDialogClose();
        public onDialogClose onDialogCloseEvent;

        bool _hideArrow;

        public ContextMenuStrip Menu
        {
            get { return _dropdownMenu; }
            set
            {
                _dropdownMenu = value;
                _infoDialog = null;
                _arrowButton.Image = Properties.Resources.arrow;
            }
        }

        public Form Dialog
        {
            get { return _infoDialog; }
            set
            {
                _dropdownMenu = null;
                _infoDialog = value;
                _arrowButton.Image = Properties.Resources.expand ;
            }
        }

        public bool HideArrow
        {
            get { return _hideArrow; }
            set
            {
                _hideArrow = value;

                if (_hideArrow)
                {
                    _arrowButton.Visible = false;
                    _arrowButton.Click -= arrowButton_Click;
                    base.Click += arrowButton_Click;
                }
                else
                {
                    _arrowButton.Visible = true;
                    _arrowButton.Click += arrowButton_Click;
                    base.Click -= arrowButton_Click;
                }
            }
        }

        public SplitButton()
        {
            base.TextAlign = ContentAlignment.MiddleLeft;

            _arrowButton = new FlatButton
            {
                Width = 19,
                ImageAlign = ContentAlignment.MiddleCenter
            };

            //_arrowButton.Click += arrowButton_Click;
            Controls.Add(_arrowButton);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _arrowButton.Left = Width - _arrowButton.Width;
            _arrowButton.Height = Height;
            _arrowButton.BackColor = BackColor;
        }

        private void arrowButton_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Click");

            if (_dropdownMenu != null)
            {
                Point screenPoint = PointToScreen(new Point(Left, Bottom));

                if (screenPoint.Y + _dropdownMenu.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    _dropdownMenu.Show(this, new Point(0, -(_dropdownMenu.Size.Height + 1)));
                }
                else
                {
                    _dropdownMenu.Show(this, new Point(0, Height + 1));
                }
            }
            else if (_infoDialog != null)
            {
                _infoDialog.ShowDialog();

                if (onDialogCloseEvent != null)
                {
                    if (_infoDialog.DialogResult != DialogResult.Cancel)
                        onDialogCloseEvent();
                }
            }
        }

        public void Expand()
        {
            arrowButton_Click(null, null);
        }

    }
}
