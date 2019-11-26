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
    /// 日 期：2019/11/21 14:54:34
    /// 描 述：order_dal 实体
    /// </summary>
    [Table("order")]
    public partial class order_model
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 消息id
        /// </summary>
        public int mesid { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string messageName { get; set; }

        /// <summary>
        /// 箱号ID
        /// </summary>
        public int boxId { get; set; }

        /// <summary>
        /// 层位
        /// </summary>
        public string level { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public int weight { get; set; }

        /// <summary>
        /// 库区编码
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 来源库区
        /// </summary>
        public string sourceCode { get; set; }

        /// <summary>
        /// 取货站台
        /// </summary>
        public string s_station { get; set; }

        /// <summary>
        /// 放货站台
        /// </summary>
        public string d_station { get; set; }

        /// <summary>
        /// 货位
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// wmsID
        /// </summary>
        public int wmsid { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int priority { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [IgnoreUpdate]
        public DateTime createTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [IgnoreUpdate]
        public DateTime endTime { get; set; }
    }
}