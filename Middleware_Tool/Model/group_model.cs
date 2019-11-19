using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Dapper;

namespace Middleware_CoreWeb
{
    /*代码自动生成工具自动生成,不要在这里写代码，否则会被自动覆盖哦 */

    /// <summary>
    /// 版 本 V1.0.1 代码快速生成工具
    /// 创 建：代码生成工具
    /// 日 期：2019/11/12 15:39:39
    /// 描 述：group_dal 实体
    /// </summary>
    [Table("group")]
    public partial class group_model
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 权限组描述
        /// </summary>
        public string explain { get; set; }
    }
}