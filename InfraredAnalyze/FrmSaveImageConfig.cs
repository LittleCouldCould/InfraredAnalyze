﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmSaveImageConfig : Form
    {
        public FrmSaveImageConfig()
        {
            InitializeComponent();
        }

        private void FrmSaveImageConfig_Load(object sender, EventArgs e)
        {
            tbxSaveImage.Text = ConfigurationManager.AppSettings["ImageSavePath"];
            StaticClass.Temper_Monitor = DMSDK.DM_OpenMonitor(pbxTemper.Handle, StaticClass.Temper_Ip, 5000);
        }

        string path = string.Empty;
        private void btnChangePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            tbxSaveImage.Text = path;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//保存配置文件
            config.AppSettings.Settings["ImageSavePath"].Value = path.ToString();
            ConfigurationManager.RefreshSection("ImageSavePath");
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void btnSnapShot_Click(object sender, EventArgs e)
        {
            path = ConfigurationManager.AppSettings["ImageSavePath"];
            if (DMSDK.DM_Capture(StaticClass.Temper_Monitor, path) < 0)
            {
                MessageBox.Show("保存图片失败！请重试");
            }
            else
            {
                MessageBox.Show("拍摄完成！");
            }
        }
    }
}