﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InfraredAnalyze
{
    public class StructClass
    {
        /// <summary>
        /// 结构体StructSMInfrared_Config 项目名称 数据库名称
        /// </summary>
        public struct StructSMInfrared_Config
        {
            public int ProjId;
            public string ProjName;
            public string DataBaseName;
            public bool Enable;
        }
        public struct StructIAnalyzeConfig
        {
            public int CameraID;
            public string CameraName;
            public string IP;
            public int Port;
            public int NodeID;
            public string Reamrks;
            public bool Enable;
        }

        public struct StructSM7003Tag
        {
            public int CameraID;
            public string IP;
            public int Port;
            public int NodeID;
            public string Reamrks;
            public bool Enable;
        }

        public struct StructAlarmconfig//报警设置结构体
        {
            public int AreaType;//区域编号
            public int Spark;//报警触发方式  大于还是小于
            public int AlarmTemper;//报警温度
            public bool Enable;//是否启用报警
        }


        public struct realTimeTemper
        {
            public int type;
            public int number;
            public int temper;
        }

        public struct AreaAlarmCount//区域告警次数计数
        {
            public int AreaId;//区域ID
            public int AreaCount;//区域告警次数计数
        }

        public struct StructAlarm
        {
            public int CameraId;
            public StructAlarmconfig[] structAlarmconfigs;//目前是8个测温目标（可扩展）
        }

        public struct realTimeStructTemper
        {
            public int CameraId;
            public realTimeTemper[] realTimeTemper;//目前是8个测温目标（可扩展）

        }

        public struct alarmStructCount
        {
            public int CameraId;
            public AreaAlarmCount[] areaAlarmCounts;//目前是8个测温目标（可扩展）

        }

        public struct StructTemperData
        {
            public int CameraID;
            public string IPAddress;
            public DateTime dateTime;
            public string Type;
            public decimal Temper;
            public string Status;
        }

        public struct StructCameraId_Datetime
        {
            public int CameraID;//相机ID
            public StructNumber_Datetime[] structNumber_DateTime;
        }

        public struct StructNumber_Datetime
        {
            public int number;
            public DateTime dateTime;
        }

        /// <summary>
        /// 火警数组
        /// </summary>
        public struct StructFireData
        {
            public int CameraID;
            public string IPAddress;
            public DateTime dateTime;
            public string Type;
            public string Message;//报警信息
        }

        /// <summary>
        /// 故障数组
        /// </summary>
        public struct StructErrData
        {
            public int CameraID;
            public string IPAddress;
            public DateTime dateTime;
            public string Type;
            public string Message;//报警信息
        }

        public struct StructRecordsData
        {
            public int CameraID;
            public string IPAddress;
            public DateTime dateTime;
            public string Type;
            public string Message;
        }

        public struct StructPwd
        {
            public int level;
            public string pwd;
        }
    }
}
