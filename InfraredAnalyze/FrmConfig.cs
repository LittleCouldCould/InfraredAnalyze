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
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            grpConfig.Text = "网络设置";
            btnNetConfig.Tag = 1;
            btnNetConfig.BackColor = Color.Yellow;
            btnNetConfig.PerformClick();
        }

        private void pnlLeftMenu_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)pnlLeftMenu.GetChildAtPoint(new Point(e.X, e.Y));

        }

        private void btnBackColor_Init(Button button)
        {
            grpConfig.Text = button.Text;
            foreach (Button btn in pnlLeftMenu.Controls)
            {
                btn.BackColor = Color.Transparent;
                btn.Tag = 0;//没单击
            }
            button.BackColor = Color.Yellow;
            button.Tag = 1;//单击过
        }

        #region//鼠标进入事件
        private void btnNetConfig_MouseEnter(object sender, EventArgs e)
        {
            btnNetConfig.BackColor = Color.Yellow;
        }

        private void btnNetConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnNetConfig.Tag) == 0)
                btnNetConfig.BackColor = Color.Transparent;
        }

        private void btnMeasureConfig_MouseEnter(object sender, EventArgs e)
        {
            btnMeasureConfig.BackColor = Color.Yellow;
        }

        private void btnMeasureConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnMeasureConfig.Tag) == 0)
                btnMeasureConfig.BackColor = Color.Transparent;
        }

        private void btnDataConfig_MouseEnter(object sender, EventArgs e)
        {
            btnDataConfig.BackColor = Color.Yellow;
        }

        private void btnDataConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnDataConfig.Tag) == 0)
                btnDataConfig.BackColor = Color.Transparent;
        }

        private void btnImageConfig_MouseEnter(object sender, EventArgs e)
        {
            btnImageConfig.BackColor = Color.Yellow;
        }

        private void btnImageConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnImageConfig.Tag) == 0)
                btnImageConfig.BackColor = Color.Transparent;
        }

        private void btnTemperParam_MouseEnter(object sender, EventArgs e)
        {
            btnTemperParam.BackColor = Color.Yellow;
        }

        private void btnTemperParam_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnTemperParam.Tag) == 0)
                btnTemperParam.BackColor = Color.Transparent;
        }

        private void btnAlarmConfig_MouseEnter(object sender, EventArgs e)
        {
            btnAlarmConfig.BackColor = Color.Yellow;
        }

        private void btnAlarmConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnAlarmConfig.Tag) == 0)
                btnAlarmConfig.BackColor = Color.Transparent;
        }

        private void btnSystemConfig_MouseEnter(object sender, EventArgs e)
        {
            btnSystemConfig.BackColor = Color.Yellow;
        }

        private void btnSystemConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnSystemConfig.Tag) == 0)
                btnSystemConfig.BackColor = Color.Transparent;
        }

        private void btnVideoConfig_MouseEnter(object sender, EventArgs e)
        {
            btnVideoConfig.BackColor = Color.Yellow;
        }

        private void btnVideoConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnVideoConfig.Tag) == 0)
                btnVideoConfig.BackColor = Color.Transparent;
        }

        private void btnPTZConfig_MouseEnter(object sender, EventArgs e)
        {
            btnPTZConfig.BackColor = Color.Yellow;
        }

        private void btnPTZConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32( btnPTZConfig.Tag) == 0) 
                btnPTZConfig.BackColor = Color.Transparent;
        }

        #endregion
      

        #region//单击事件
        private void btnNetConfig_Click(object sender, EventArgs e)//网络设置
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmCameraNetConfig frmCameraNetConfig = new FrmCameraNetConfig();
            frmCameraNetConfig.TopLevel = false;
            grpConfig.Controls.Add(frmCameraNetConfig);
            frmCameraNetConfig.BringToFront();
            frmCameraNetConfig.Location = new Point(0, grpConfig.Height / 4);
            frmCameraNetConfig.Show();
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
            }
        }

        private void btnMeasureConfig_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);
            FrmMeasureTemperConfig frmMeasureTemperConfig = new FrmMeasureTemperConfig();
            frmMeasureTemperConfig.TopLevel = false;
            grpConfig.Controls.Add(frmMeasureTemperConfig);
            frmMeasureTemperConfig.BringToFront();
            frmMeasureTemperConfig.Location = new Point(20, 20);
            frmMeasureTemperConfig.Show();
        }

        private void btnDataConfig_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);
            FrmSaveImageConfig frmSaveImageConfig = new FrmSaveImageConfig();
            frmSaveImageConfig.TopLevel = false;
            grpConfig.Controls.Add(frmSaveImageConfig);
            frmSaveImageConfig.BringToFront();
            frmSaveImageConfig.Location = new Point(20, 20);
            frmSaveImageConfig.Show();
        }

        private void btnImageConfig_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);
            FrmImageConfig frmImageConfig = new FrmImageConfig();
            frmImageConfig.TopLevel = false;
            grpConfig.Controls.Add(frmImageConfig);
            frmImageConfig.BringToFront();
            frmImageConfig.Location = new Point(20, 20);
            frmImageConfig.Show();
        }

        private void btnTemperParam_Click(object sender, EventArgs e)
        {
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);

        }

        private void btnAlarmConfig_Click(object sender, EventArgs e)
        {
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);

        }

        private void btnSystemConfig_Click(object sender, EventArgs e)
        {
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);

        }

        private void btnVideoConfig_Click(object sender, EventArgs e)
        {
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);

        }

        private void btnPTZConfig_Click(object sender, EventArgs e)
        {
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("设备连接失败！请重新连接设备！");
                return;
            }
            btnBackColor_Init(sender as Button);

        }
        #endregion
    }
}
