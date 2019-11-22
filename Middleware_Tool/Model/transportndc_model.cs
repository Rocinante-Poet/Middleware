using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Dapper;

namespace Middleware_Tool
{
    /*代码自动生成工具自动生成,不要在这里写代码，否则会被自动覆盖哦 */

    /// <summary>
    /// 版 本 V1.0.1 代码快速生成工具
    /// 创 建：代码生成工具
    /// 日 期：2019-11-22 13:28:00
    /// 描 述：transportndc_dal 实体
    /// </summary>
    [Table("transportndc")]
    public partial class Transportndc_model
    {
        [Key]
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// NDC系统中 执行编号
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// NDC系统中 任务阶段代号
        /// </summary>
        public int phase { get; set; }

        /// <summary>
        /// 阶段时间 累加
        /// </summary>
        public string phaseTime { get; set; }

        /// <summary>
        /// AVG编号
        /// </summary>
        public int carNum { get; set; }

        /// <summary>
        /// NDC系统中 任务订单号
        /// </summary>
        public int ikey { get; set; }

        /// <summary>
        /// 起点
        /// </summary>
        public int startingPoint { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public int endPoint { get; set; }

        /// <summary>
        /// 交互状态
        /// </summary>
        public int interactiveState { get; set; }

        /// <summary>
        /// 交互时间 累加
        /// </summary>
        public string interactionTime { get; set; }
    }
}