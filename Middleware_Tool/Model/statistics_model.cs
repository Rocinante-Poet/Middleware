using Dapper;
using System;

namespace Middleware_Tool
{
    public partial class signaliolog_model
    {
        /// <summary>
        ///
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 内部标识
        /// </summary>
        public string iid { get; set; }

        /// <summary>
        /// EQP，PD，QC
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 产线号
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 目标组
        /// </summary>
        public string GroupID { get; set; }

        /// <summary>
        /// 状态 1创建/2接受/3开始/4结束/5取消
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建任务时间
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// 接受任务设备
        /// </summary>
        public string AcceptedUser { get; set; }

        /// <summary>
        /// 接受任务时间
        /// </summary>
        public string AcceptedTime { get; set; }

        /// <summary>
        /// 开始任务时间
        /// </summary>
        public string StartedTime { get; set; }

        /// <summary>
        /// 完成任务时间
        /// </summary>
        public string CancelledTime { get; set; }
    }

    public partial class workshoplog_model
    {
        [Key]
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        public int Iid { get; set; }

        /// <summary>
        /// 产线号
        /// </summary>
        public int WSnum { get; set; }

        /// <summary>
        /// 1-外纸箱、2-小纸盒、3-说明书、5-空托盘、6-成品、7-垃圾
        /// </summary>
        public string WScommand { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int WSanswer { get; set; }

        /// <summary>
        /// 任务下达时间
        /// </summary>
        public string WStime { get; set; }

        /// <summary>
        /// 仓库准备时间
        /// </summary>
        public string WHPrepare { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string AGVstart { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public string AGVend { get; set; }
    }
}