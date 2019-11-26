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
    /// 日 期：2019/11/18 15:52:09
    /// 描 述：error_dal 实体
    /// </summary>
    [Table("error")]
    public partial class error_model
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string time { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string url { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string errormeg { get; set; }
    }
}