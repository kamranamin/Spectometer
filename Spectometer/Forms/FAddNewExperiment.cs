using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class FAddNewExperiment : Spectometer.Forms.F_Base
    {
        public FAddNewExperiment()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FAddNewExperiment_Load(object sender, EventArgs e)
        {
            txtExperimentName.Focus();
        }
    }
}
