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
            Name = "1",
            Password = "1",
            IsAdmin = true
        };

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