﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class FSetting : F_Base 
    {
        public FSetting()
        {
            InitializeComponent();
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void FSetting_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
          

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            if (txtpass.Text == "12345")
            {
           
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
