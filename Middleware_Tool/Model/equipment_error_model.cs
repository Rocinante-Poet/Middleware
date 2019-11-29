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
    /// 日 期：2019/11/28 16:54:16 
    /// 描 述：equipment_error_dal 实体 
    /// </summary> 
    [Table("equipment_error")]
    public partial class equipment_error_model
    {
        
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int equipmentId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string errorType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string errorTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime date { get; set; }
        
        /// <summary>
        /// 是否处理 -1未处理 200已处理
        /// </summary>
        public int state { get; set; }
        
    }
}