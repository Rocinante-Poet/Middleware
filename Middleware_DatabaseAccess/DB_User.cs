using Dapper;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_DatabaseAccess
{
    public class DB_User
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DBLoginAsync(JWTUserModel _user)
        {
            var connection = CRUD.GetOpenConnection();
            var data = await connection.GetListAsync<JWTUserModel>(new { _user.Name, _user.Pwd });
            if (data.ToList().Count != 0)
                return true;
            return false;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DBRegisterAsync(JWTUserModel _user)
        {
            var connection = CRUD.GetOpenConnection();
            var data = await connection.GetListAsync<JWTUserModel>(new { _user.Name });
            if (data.ToList().Count != 0)
                return false;
            else
            {
                await connection.InsertAsync<int, JWTUserModel>(new JWTUserModel { Name = _user.Name, Pwd = _user.Pwd, Remark = _user.Remark });
                return true;
            }
        }
    }
}