using Dapper;
using Middleware_DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Data;
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


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Pagelimit"></param>
        /// <param name="Pageoffset"></param>
        /// <param name="meunName"></param>
        /// <returns></returns>
        public JsonData<Userinfo> GetList(int Pagelimit, int Pageoffset, string Name, int Group)
        {
            return CRUD.ExcuteSql((connection) =>
            {
                StringBuilder strQuery = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(Name)|| Group != 0)
                {
                    strQuery.Append("where ");
                }
                if (Group != 0)
                {
                    strQuery.Append(" Power_ID=@Power_ID  ");
                }
                if (!string.IsNullOrWhiteSpace(Name)&& Group == 0)
                {
                    strQuery.Append(" showName like @Name");
                }
                if (!string.IsNullOrWhiteSpace(Name) && Group != 0)
                {
                    strQuery.Append(" and  showName like @Name");
                }
                var List = connection.GetListPaged<Userinfo>((Pageoffset / Pagelimit) + 1, Pagelimit, strQuery.ToString(), "", new { Name = $"%{Name}%", Power_ID = Group });
                foreach (var ItemInfo in List)
                {
                    ItemInfo.group = new DB_Group().Get(ItemInfo.Power_ID);
                }
                var CountPage = connection.RecordCount<Userinfo>(strQuery.ToString(), new { Name = $"%{Name}%", Power_ID = Group });
                return new JsonData<Userinfo>() { rows = List, total = CountPage };
            });
        }




        public bool Add(Userinfo group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Insert(group) > 0;
            });
        }

        public bool Delete(List<Userinfo> grouparray)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.DeleteList<Userinfo>("WHERE UserID=@UserID", grouparray) > 0;
            });
        }

        public bool Update(Userinfo group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Update(group) > 0;
            });
        }




    }
}