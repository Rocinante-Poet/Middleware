using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{
    public class Log4net
    {
        private static Dictionary<string, log4net.ILog> m_lstLog = new Dictionary<string, log4net.ILog>();

        static Log4net()
        {
            string strLog4NetConfigFile = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
            ILoggerRepository repository = LogManager.CreateRepository("NETCorelog4net");
            XmlConfigurator.Configure(repository, new System.IO.FileInfo(strLog4NetConfigFile));
            m_lstLog["info_log"] = LogManager.GetLogger(repository.Name, "info_log");
            m_lstLog["error_log"] = LogManager.GetLogger(repository.Name, "error_log");
            m_lstLog["mysql_log"] = LogManager.GetLogger(repository.Name, "mysql_log");
        }

        /// <summary>
        /// 功能描述:写入常规日志
        /// </summary>
        /// <param name="strInfoLog">strInfoLog</param>
        public static void WriteInfo(string strInfoLog)
        {
            if (m_lstLog["info_log"].IsInfoEnabled)
            {
                m_lstLog["info_log"].Info(strInfoLog);
            }
        }

        /// <summary>
        /// 功能描述:写入错误日志
        /// </summary>
        /// <param name="strErrLog">strErrLog</param>
        /// <param name="ex">ex</param>
        public static void WriteError(string strErrLog, Exception ex = null)
        {
            if (m_lstLog["error_log"].IsErrorEnabled)
            {
                m_lstLog["error_log"].Error(strErrLog, ex);
            }
        }

        /// <summary>
        /// 功能描述:写入MySql日志
        /// </summary>
        /// <param name="">strInfoLog</param>
        public static void WriteMysql(string level, string Message)
        {
            if (m_lstLog["mysql_log"].IsErrorEnabled)
            {
                GlobalContext.Properties["loglevel"] = $"{level}";
                m_lstLog["mysql_log"].Info($"{Message}");
            }
        }
    }
}