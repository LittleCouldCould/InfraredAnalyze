﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmSaveImageConfig : Form
    {
        public FrmSaveImageConfig(UCPbx uCPbx)
        {
            InitializeComponent();
            pnlMonitor.Controls.Add(uCPbx);
            uCPbx.Dock = DockStyle.Fill;
        }

        private void FrmSaveImageConfig_Load(object sender, EventArgs e)
        {
            if (StaticClass.tempMonitor < 0)
            {
                MessageBox.Show("连接失败，请重新连接探测器！");
                foreach (Control control in this.Controls)
                {
                    control.Enabled = false;
                }
                return;
            }
            string path;
            path = ConfigurationManager.AppSettings["ImageSavePath"];
            tbxSaveImage.Text = path;
        }

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            string path;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                tbxSaveImage.Text = path;
            }
        }

        private void btnSnapShot_Click(object sender, EventArgs e)
        {
            string path;
            path = ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + StaticClass.Temper_CameraId.ToString() + "\\";
            if (DMSDK.DM_Capture(StaticClass.tempMonitor, path) < 0)
            {
                MessageBox.Show("保存图片失败！请检查路径再重试");
            }
            else
            {
                MessageBox.Show("抓图完成！");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//保存配置文件
                config.AppSettings.Settings["ImageSavePath"].Value = tbxSaveImage.Text;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                string path = ConfigurationManager.AppSettings["ImageSavePath"];
                tbxSaveImage.Text = path;
                if (!Directory.Exists(path+"SMCameraPic"))
                {
                    Directory.CreateDirectory(path + "SMCameraPic");
                    if(!Directory.Exists(path + "SMCameraPic" + "\\" + StaticClass.ProjName ))
                    {
                        Directory.CreateDirectory(path + "SMCameraPic" + "\\" + StaticClass.ProjName);
                    }
                }
                for (int i = 1; i <= 16; i++)
                {
                    if (!Directory.Exists(path + "SMCameraPic" + "\\" +StaticClass.ProjName+"\\"+ "Camera" + i.ToString()))
                    {
                        Directory.CreateDirectory(path + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + i.ToString());
                    }
                }
                MessageBox.Show("操作成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmSaveImageConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

    }
}