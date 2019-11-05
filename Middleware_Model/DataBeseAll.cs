using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Model
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
        public string Function { get; set; }
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
    }



}