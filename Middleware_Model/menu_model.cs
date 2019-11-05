using Dapper;

namespace Middleware_Model
{
    /*代码自动生成工具自动生成,不要在这里写代码，否则会被自动覆盖哦 */

    /// <summary> 
    /// 版 本 V1.0.1 代码快速生成工具 
    /// 创 建：代码生成工具 
    /// 日 期：2019/11/4 19:50:13 
    /// 描 述：menu_dal 实体 
    /// </summary> 
    [Table("menu")]
    public partial class menu_model
    {
        
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int id { get; set; }
        
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// icon图标
        /// </summary>
        public string icon { get; set; }
        
        /// <summary>
        /// url地址
        /// </summary>
        public string url { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public string state { get; set; }
        
        /// <summary>
        /// 子菜单ID
        /// </summary>
        public int menuid { get; set; }
        
        /// <summary>
        /// 说明
        /// </summary>
        public string explain { get; set; }

        /// <summary>
        /// 是否为子菜单
        /// </summary>
        public int type { get; set; }


    }
}