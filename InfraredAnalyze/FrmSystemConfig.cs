using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmSystemConfig : Form
    {
        public FrmSystemConfig()
        {
            InitializeComponent();
            DMSDK.DM_Init();
            DMSDK.DM_PlayerInit(pbxVideo.Handle);
            trbLocation.Value = 5;
            lbltrbValue.Text = trbLocation.Value.ToString();
            this.Disposed += FrmSystemConfig_Disposed;
        }

        int tempConnect;
        int tempMonitor;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SqlCreate sqlCreate = new SqlCreate();
    
        private void FrmVideoConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            DMSDK.DM_Disconnect(tempConnect);
            DMSDK.DM_CloseMonitor(tempMonitor);
            //DMSDK.DM_PlayerCleanup();
            this.DialogResult = DialogResult.OK;
        }

        private void FrmSystemConfig_Disposed(object sender, EventArgs e)
        {
            DMSDK.DM_Disconnect(tempConnect);
            DMSDK.DM_CloseMonitor(tempMonitor);
            //DMSDK.DM_PlayerCleanup();
        }

        StringBuilder cCameraName = new StringBuilder();
        //StringBuilder cUserDefineInfo = new StringBuilder();
        StringBuilder dateTime = new StringBuilder();
        string cUserDefineInfo;

       /*
        BitrateType:	编码类型。0: 可变编码；1: 固定编码
        Resolution:		分辨率。0: 320x240; 1: 384x288; 2: 640x480; 3: 720x480; 4: 720x576; 
                    Bitrate:		码流。0: 128; 1: 256; 2: 512; 3: 1024
                    FrameRate:		帧率。可选参数: 1~25，30
        */
       
        int Major_BitrateType;
        int Major_Resolution;//主码流用于本地视频的保存 
        int Major_Bitrate;//
        int Major_FrameRate;

        int Minor_BitrateType;
        int Minor_Resolution;//次码流用于传输显示
        int Minor_Bitrate;//
        int Minor_FrameRate;

        int iDisplayName;
        int iDisplayUserDefineInfo;
        int iDisplayTime;

        int iDisplayTimeX = 0;
        int iDisplayTimeY = 0;
        int iDisplayNameX = 0;
        int iDisplayNameY = 0;
        int iDisplayUserDefineInfoX = 0;
        int iDisplayUserDefineInfoY = 0;
        DMSDK.tagResolutionInfo tagResolutionInfo = new DMSDK.tagResolutionInfo();
        

        private void FrmVideoConfig_Load(object sender, EventArgs e)
        {

            tempConnect = DMSDK.DM_Connect(this.Handle, StaticClass.Temper_Ip, 80);
            if (tempConnect > 0)
            {
                tempMonitor = DMSDK.DM_OpenMonitor(pbxVideo.Handle, StaticClass.Temper_Ip, 5000, 0);
            }
            if (tempConnect < 0 || tempMonitor < 0)
            {
                MessageBox.Show("连接失败，请重试！");
            }
            else
            {
                #region//显示位置归零
                //DMSDK.DM_SetOSDInfo_DateTime(tempConnect, 1, 100, 0);
                //DMSDK.DM_SetOSDInfo_CameraName(tempConnect, tbxCameraName.Text, 1,  0, 0);
                //DMSDK.DM_GetOSDInfo_UserDefine(StaticClass.intPtrs_Operate[iPCameraId - 1], cUserDefineInfo, out iDisplayUserDefineInfo, out iDisplayUserDefineInfoX, out iDisplayUserDefineInfoY);//自定义信息
                #endregion
                DMSDK.DM_GetDM6xResolution(tempConnect, ref tagResolutionInfo);//获取分辨率
                tempConnect = DMSDK.DM_Connect(pbxVideo.Handle, StaticClass.Temper_Ip, 80);
                tempMonitor = DMSDK.DM_OpenMonitor(pbxVideo.Handle, StaticClass.Temper_Ip, 5000, 0);
                DMSDK.DM_GetEncodingInfo_Major(tempConnect, ref Major_BitrateType, ref Major_Resolution, ref Major_Bitrate, ref Major_FrameRate);
                DMSDK.DM_GetEncodingInfo_Minor(tempConnect, ref Minor_BitrateType, ref Minor_Resolution, ref Minor_Bitrate, ref Minor_FrameRate);
                DMSDK.DM_GetOSDInfo_CameraName(tempConnect, cCameraName, out iDisplayName, out iDisplayNameX, out iDisplayNameY);//cCameraName 乱码 未解决
                DMSDK.DM_GetOSDInfo_DateTime(tempConnect, out iDisplayTime, out iDisplayTimeX, out iDisplayTimeY);
                DMSDK.DM_GetDateTime(tempConnect, dateTime);//RTM的难搞
                dtpCameraDateTime.Value = Convert.ToDateTime(dateTime.ToString());//获取相机的系统时间并显示
                if (iDisplayName == 0)
                {
                    rdbNotCameraName.Checked = true;
                }else
                {
                    rdbIsCameraName.Checked = true;
                }
                if(iDisplayTime==0)
                {
                    rdbNotTime.Checked = true;
                }else
                {
                    rdbIsTime.Checked = true;
                }
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, StaticClass.Temper_CameraName, iDisplayName, iDisplayNameX, iDisplayNameY);
                tbxCameraName.Text = StaticClass.Temper_CameraName;
                rdbMajor.Checked = true;
            }
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

        private void tbxCameraName_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            Match match = regex.Match(tbxCameraName.Text);
            if(!match.Success)
            {
                MessageBox.Show("请输入合适的相机名称！");
                tbxCameraName.Focus();
                tbxCameraName.Text = StaticClass.Temper_CameraName;
            }
            else
            {
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, tbxCameraName.Text, iDisplayName, iDisplayNameX, iDisplayNameY);
                sqlCreate.UpDate_CameraName(StaticClass.Temper_CameraId, tbxCameraName.Text, StaticClass.DataBaseName);
                StaticClass.Temper_CameraName = tbxCameraName.Text;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int returnValue = DMSDK.DM_SetEncodingInfo_Minor(tempConnect, Minor_BitrateType, Minor_Resolution, Minor_Bitrate, 25);
            returnValue = DMSDK.DM_SetEncodingInfo_Major(tempConnect, Major_BitrateType, Major_Resolution, Major_Bitrate, 25);
            if (returnValue > 0)
            {
                MessageBox.Show("设置成功！");
            }
            else
            {
                MessageBox.Show("设置失败！请重试");
            }
            MessageBox.Show("设置完成！");
        }
        
        private void rdbIsCameraName_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbIsCameraName.Checked==true)
            {
                iDisplayName = 1;
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, StaticClass.Temper_CameraName, iDisplayName, iDisplayNameX, iDisplayNameY);
            }
        }

        private void rdbNotCameraName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNotCameraName.Checked == true)//
            {
                iDisplayName = 0;
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, StaticClass.Temper_CameraName, iDisplayName, iDisplayNameX, iDisplayNameY);
            }
        }

        private void rdbIsTime_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbIsTime.Checked==true)
            {
                iDisplayTime = 1;
                DMSDK.DM_SetOSDInfo_DateTime(tempConnect, 1, iDisplayTimeX, iDisplayTimeY);
            }
        }

        private void rdbNotTime_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbNotTime.Checked==true)//
            {
                iDisplayTime = 0;
                DMSDK.DM_SetOSDInfo_DateTime(tempConnect, 0, iDisplayTimeX, iDisplayTimeY);
            }
        }

        private void btnUpdateTime_Click(object sender, EventArgs e)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            DMSDK.DM_SetDateTime(tempConnect, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            dateTime = dtpCameraDateTime.Value;
            dtpCameraDateTime.Value = dateTime;
            //timer1.Start();//会有一秒左右的延迟
            dtpCameraDateTime.Value = System.DateTime.Now;
            MessageBox.Show("设置完成！");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtpCameraDateTime.Value = System.DateTime.Now;
        }

        public void GetVideoPara(int BitrateType, int Resolution, int Bitrate,int FrameRate)
        {
            switch (BitrateType)
            {
                case 0:
                    cbxBitrateType.SelectedIndex = 0;
                    break;
                case 1:
                    cbxBitrateType.SelectedIndex = 1;
                    break;
            }
            switch (Resolution)
            {
                case 0:
                    cbxResolution.SelectedIndex = 0;
                    break;
                case 1:
                    cbxResolution.SelectedIndex = 1;
                    break;
                case 2:
                    cbxResolution.SelectedIndex = 2;
                    break;
                case 3:
                    cbxResolution.SelectedIndex = 3;
                    break;
                case 4:
                    cbxResolution.SelectedIndex = 4;
                    break;
            }
            switch (Bitrate)
            {
                case 0:
                    cbxBitrate.SelectedIndex = 0;
                    break;
                case 1:
                    cbxBitrate.SelectedIndex = 1;
                    break;
                case 2:
                    cbxBitrate.SelectedIndex = 2;
                    break;
                case 3:
                    cbxBitrate.SelectedIndex = 3;
                    break;
            }
            tbxFrameRate.Text = FrameRate.ToString();
        }

        private void tbxFrameRate_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[1-9]\d*$");
            Match match = regex.Match(tbxFrameRate.Text);
            if (!match.Success || Convert.ToInt32(tbxFrameRate.Text) > 25)
            {
                MessageBox.Show("范围：1-25");
                tbxFrameRate.Focus();
            }
        }

        private void btnLocationRight_Click(object sender, EventArgs e)
        {
            if (rdbCNameLocation.Checked == true && (iDisplayNameX + trbLocation.Value + 100) < tagResolutionInfo.width)
            {
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, StaticClass.Temper_CameraName, 1, iDisplayNameX + trbLocation.Value, iDisplayNameY);
                DMSDK.DM_GetOSDInfo_CameraName(tempConnect, cCameraName, out iDisplayName, out iDisplayNameX, out iDisplayNameY);
            }
            else if (rdbCTimeLocation.Checked == true && (iDisplayTimeX + trbLocation.Value + 100) < tagResolutionInfo.width)
            {
                DMSDK.DM_SetOSDInfo_DateTime(tempConnect, iDisplayTime, iDisplayTimeX + trbLocation.Value, iDisplayTimeY);
                DMSDK.DM_GetOSDInfo_DateTime(tempConnect, out iDisplayTime, out iDisplayTimeX, out iDisplayTimeY);

            }
        }

        private void btnLocationLeft_Click(object sender, EventArgs e)
        {
            if (rdbCNameLocation.Checked == true && (iDisplayNameX - trbLocation.Value) > 0)
            {
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, StaticClass.Temper_CameraName, 1, iDisplayNameX - trbLocation.Value, iDisplayNameY);
                DMSDK.DM_GetOSDInfo_CameraName(tempConnect, cCameraName, out iDisplayName, out iDisplayNameX, out iDisplayNameY);
            }
            else if (rdbCTimeLocation.Checked == true && (iDisplayTimeX - trbLocation.Value) > 0)
            {
                DMSDK.DM_SetOSDInfo_DateTime(tempConnect, iDisplayTime, iDisplayTimeX - trbLocation.Value, iDisplayTimeY);
                DMSDK.DM_GetOSDInfo_DateTime(tempConnect, out iDisplayTime, out iDisplayTimeX, out iDisplayTimeY);
            }

        }

        private void btnLocationUp_Click(object sender, EventArgs e)
        {
            if (rdbCNameLocation.Checked == true && (iDisplayNameY - trbLocation.Value) > 0)
            {
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, StaticClass.Temper_CameraName, 1, iDisplayNameX, iDisplayNameY - trbLocation.Value);
                DMSDK.DM_GetOSDInfo_CameraName(tempConnect, cCameraName, out iDisplayName, out iDisplayNameX, out iDisplayNameY);
            }
            else if (rdbCTimeLocation.Checked == true && (iDisplayTimeY - trbLocation.Value) > 0)
            {
                DMSDK.DM_SetOSDInfo_DateTime(tempConnect, iDisplayTime, iDisplayTimeX, iDisplayTimeY - trbLocation.Value);
                DMSDK.DM_GetOSDInfo_DateTime(tempConnect, out iDisplayTime, out iDisplayTimeX, out iDisplayTimeY);
            }
        }

        private void btnLocationDown_Click(object sender, EventArgs e)
        {
            if (rdbCNameLocation.Checked == true && (iDisplayNameY + 10 + trbLocation.Value) < tagResolutionInfo.height)
            {
                DMSDK.DM_SetOSDInfo_CameraName(tempConnect, StaticClass.Temper_CameraName, 1, iDisplayNameX, iDisplayNameY + trbLocation.Value);
                DMSDK.DM_GetOSDInfo_CameraName(tempConnect, cCameraName, out iDisplayName, out iDisplayNameX, out iDisplayNameY);
            }
            else if (rdbCTimeLocation.Checked == true && (iDisplayTimeY + 10 + trbLocation.Value) < tagResolutionInfo.height)
            {
                DMSDK.DM_SetOSDInfo_DateTime(tempConnect, iDisplayTime, iDisplayTimeX, iDisplayTimeY + trbLocation.Value);
                DMSDK.DM_GetOSDInfo_DateTime(tempConnect, out iDisplayTime, out iDisplayTimeX, out iDisplayTimeY);
            }
        }

        private void trbLocation_Scroll(object sender, EventArgs e)
        {
            lbltrbValue.Text = trbLocation.Value.ToString();
        }

        private void cbxBitrateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbMinor.Checked == true)
            {
                switch (cbxBitrateType.SelectedIndex)
                {
                    case 0:
                        Minor_BitrateType = 0;
                        break;
                    case 1:
                        Minor_BitrateType = 1;
                        break;
                }
            }
        }

        private void cbxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbMinor.Checked == true)
            {
                switch (cbxResolution.SelectedIndex)
                {
                    case 0:
                        Minor_Resolution = 0;
                        break;
                    case 1:
                        Minor_Resolution = 1;
                        break;
                    case 2:
                        Minor_Resolution = 2;
                        break;
                    case 3:
                        Minor_Resolution = 3;
                        break;
                    case 4:
                        Minor_Resolution = 4;
                        break;
                }
            }
        }

        private void cbxBitrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbMinor.Checked == true)
            {
                switch (cbxBitrate.SelectedIndex)
                {
                    case 0:
                        Minor_Bitrate = 0;
                        break;
                    case 1:
                        Minor_Bitrate = 1;
                        break;
                    case 2:
                        Minor_Bitrate = 2;
                        break;
                    case 3:
                        Minor_Bitrate = 3;
                        break;
                }
            }
        }

        private void rdbMajor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMajor.Checked == true)
            {
                pnlEncodingInfo.Enabled = true;
                GetVideoPara(Major_BitrateType, Major_Resolution, Major_Bitrate, Major_FrameRate);
            }
        }

        private void rdbMinor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMinor.Checked == true)
            {
                pnlEncodingInfo.Enabled = true;
                GetVideoPara(Minor_BitrateType, Minor_Resolution, Minor_Bitrate, Minor_FrameRate);
            }
        }

        
    }
}
