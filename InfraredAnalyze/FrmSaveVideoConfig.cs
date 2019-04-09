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
    public partial class FrmSaveVideoConfig : Form
    {
        public FrmSaveVideoConfig()
        {
            InitializeComponent();
        }

        string path;
        private void btnChangePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            tbxSaveVideo.Text = path;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//保存配置文件
            config.AppSettings.Settings["VideoSavePath"].Value = path.ToString();
            ConfigurationManager.RefreshSection("VideoSavePath");
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void FrmSaveVideoConfig_Load(object sender, EventArgs e)
        {
            path = ConfigurationManager.AppSettings["ImageSavePath"];
            tbxSaveVideo.Text = path;
            StaticClass.Temper_Monitor = DMSDK.DM_OpenMonitor(pbxTemper.Handle, StaticClass.Temper_Ip, 5000);
        }

        private void btnSaveVideo_Click(object sender, EventArgs e)
        {
            if (btnSaveVideo.Text == "保存视频")
            {
                DMSDK.DM_Record(StaticClass.Temper_Monitor,path);
                btnSaveVideo.Text = "停止保存";
            }
            else if(btnSaveVideo.Text == "停止保存")
            {
                DMSDK.DM_StopRecord(StaticClass.Temper_Monitor);
                btnSaveVideo.Text = "保存视频";
            }
        }
    }
}
