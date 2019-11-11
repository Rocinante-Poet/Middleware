using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DataBeseAll
    {
    }

    [Table("logstr")]
    public class Log4Info
    {
        [Key]
        [Column("logId")]
        public int Id { get; set; }

        public string logLevel { get; set; }
        public string logMessage { get; set; }
    }

    public class Operatinginfo
    {
        [Key]
        public int Id { get; set; }

        public int UserID { get; set; }
        public string Operating { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }
    }

    public class Powerdetails
    {
        [Key]
        public int ID { get; set; }

        public int PowerGroupID { get; set; }
        public string FunctionName { get; set; }
        public string FunctionUrl { get; set; }
    }

    public class Powerinfo
    {
        [Key]
        public int ID { get; set; }

        public string Power { get; set; }
        public string PowerName { get; set; }
        public string PowerDetails { get; set; }
    }

    public class Userinfo
    {
        [Key]
        public int UserID { get; set; }

        public string Name { get; set; }
        public string Pwd { get; set; }
        public string Remark { get; set; }
        public int Power_ID { get; set; }
        public string UserState { get; set; }
        public bool HasAdminRights { get; set; }
    }

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

        /// <summary>
        /// 排序
        /// </summary>
        public int no { get; set; }
    }
}