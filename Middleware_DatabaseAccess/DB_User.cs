using Dapper;
using Middleware_DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Middleware_DatabaseAccess
{
    public class DB_User
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool DBLogin(Userinfo _user)
        {
            return CRUD.ExcuteSql<bool>(connection =>
            {
                var data = connection.GetList<Userinfo>(new { _user.Name, _user.Pwd }).ToList();

                if (data.Count != 0)
                    return true;
                return false;
            });
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public bool DBRegister(Userinfo _user)
        {
            return CRUD.ExcuteSql<bool>(connection =>
            {
                var data = connection.GetList<Userinfo>(new { _user.Name }).ToList();

                if (data.Count != 0)
                    return false;
                else
                {
                    var d = connection.Insert<int, Userinfo>(new Userinfo { Name = _user.Name, Pwd = _user.Pwd, Remark = _user.Remark });
                    return true;
                }
            });
        }
    }
}