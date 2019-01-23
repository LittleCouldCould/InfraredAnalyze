﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmAddSensor : Form
    {
        public FrmAddSensor()
        {
            InitializeComponent();
        }

        IpAddressTextBox ipTextBox = new IpAddressTextBox();

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        Point point;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point temp_Point = MousePosition;
                temp_Point.Offset(-point.X, -point.Y);
                this.Location = temp_Point;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddSensor_Load(object sender, EventArgs e)
        {
            pnlIPAdd.Controls.Add(ipTextBox);
            ipTextBox.Location = new Point(0, 4);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ipTextBox.IPAdd.ToString());
        }
    }
}