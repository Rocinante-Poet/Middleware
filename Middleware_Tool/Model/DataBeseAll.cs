using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_CoreWeb
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

        /// <summary>
        /// 操作记录
        /// </summary>
        public string Operating { get; set; }

        public string Date { get; set; }

        /// <summary>
        /// 详情说明
        /// </summary>
        public string Details { get; set; }

        public string UserName { get; set; }

        public string ip { get; set; }

        public string Browser { get; set; }

        public string OS { get; set; }

        public int state { get; set; }

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

    [Table("Userinfo")]
    public class JWTUserModel : Userinfo
    { }

    [Table("Userinfo")]
    public class Userinfo
    {
        [Key]
        [Column("UserID")]
        public int id { get; set; }

        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int Power_ID { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public int UserState { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [IgnoreUpdate]
        public string CreateTime { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string showName { get; set; }


        [Editable(false)]
        public group_model group { get; set; }



    }

    [Table("menu")]
    public class menu_model
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
        public int state { get; set; }

        /// <summary>
        /// 子菜单ID
        /// </summary>
        public int menuid { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string explain { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int no { get; set; }
    }
}