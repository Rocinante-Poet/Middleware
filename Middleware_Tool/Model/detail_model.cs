using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Middleware_Tool
{
    /*代码自动生成工具自动生成,不要在这里写代码，否则会被自动覆盖哦 */

    /// <summary> 
    /// 版 本 V1.0.1 代码快速生成工具 
    /// 创 建：代码生成工具 
    /// 日 期：2019/11/15 13:52:17 
    /// 描 述：detail_dal 实体 
    /// </summary> 
    [Table("detail")]
    public partial class detail_model
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int id { get; set; }
        
        /// <summary>
        /// 角色id
        /// </summary>
        public int groupid { get; set; }
        
        /// <summary>
        /// 功能ID
        /// </summary>
        public int menuid { get; set; }
        
    }
}