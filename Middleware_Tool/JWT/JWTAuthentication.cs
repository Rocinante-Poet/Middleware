using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_Tool
{
    public interface IJWTAuthentication
    {
        Task<JWTUserModel> LoginAsync(string name, string password);
    }

    public class JWTAuthentication : IJWTAuthentication
    {
        private readonly static JWTUserModel User = new JWTUserModel
        {
            Id = 1,
            Name = "3",
            Password = "3",
            //IsAdmin = true,
            RoleNumber = 2
        };

        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<JWTUserModel> LoginAsync(string name, string password)
        {
            await Task.CompletedTask;

            if (User.Name == name && User.Password == password)
            {
                return User;
            }
            return null;
        }
    }
}