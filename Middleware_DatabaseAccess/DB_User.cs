using Dapper;
using Middleware_CoreWeb;
using System.Collections.Generic;
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
        public async Task<JWTUserModel> DBLoginAsync(JWTUserModel _user)
        {
            var connection = CRUD.GetOpenConnection();
            var _d = await connection.GetListAsync<JWTUserModel>(new { _user.Name, _user.Pwd });
            if (_d.ToList().Count > 0)
            {
                foreach (JWTUserModel u in _d)
                {
                    _user.id = u.id;
                    _user.Power_ID = u.Power_ID;
                    _user.Remark = u.Remark;
                }
                return _user;
            }
            else return null;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DBRegisterAsync(JWTUserModel _user)
        {
            _user.UserState = 0;
            _user.Power_ID = 1;

            var connection = CRUD.GetOpenConnection();
            var data = await connection.GetListAsync<JWTUserModel>(new { _user.Name });
            if (data.ToList().Count != 0)
                return false;
            else
            {
                await connection.InsertAsync<int, JWTUserModel>(_user);//new JWTUserModel { Name = _user.Name, Pwd = _user.Pwd, UserState = _user.UserState, Power_ID = _user.Power_ID, Remark = _user.Remark }
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
                if (!string.IsNullOrWhiteSpace(Name) || Group != 0)
                {
                    strQuery.Append("where ");
                }
                if (Group != 0)
                {
                    strQuery.Append(" Power_ID=@Power_ID  ");
                }
                if (!string.IsNullOrWhiteSpace(Name) && Group == 0)
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
                    ItemInfo.Pwd = string.Empty;
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
                return connection.ExecuteAsync(group) > 0;
            });
        }

        public bool Delete(List<Userinfo> grouparray)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.DeleteList<Userinfo>("WHERE UserID=@id", grouparray) > 0;
            });
        }

        public bool Update(Userinfo group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Update(group) > 0;
            });
        }

        public IEnumerable<Userinfo> Get(Userinfo group)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.GetList<Userinfo>(new { Name = group.Name });
            });
        }

        public Userinfo GetUser(int id)
        {
            return CRUD.ExcuteSql(connection =>
            {
                return connection.Get<Userinfo>(id);
            });
        }
    }
}