using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_Tool;
using Newtonsoft.Json;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetTokenController : ControllerBase
    {
        private readonly IJWTAuthentication _jWTAuthentication;
        private readonly IJWTTokenService _tokenServic;

        public GetTokenController(IJWTAuthentication auth, IJWTTokenService token)
        {
            _jWTAuthentication = auth;
            _tokenServic = token;
        }

        //[HttpGet]
        //[Authorize]
        //public async Task<string> Get()
        //{
        //    await Task.CompletedTask;
        //    return "Authorize";

        //    //return $"{_identityService.GetUserId()}:{_identityService.GetUserName()}";
        //}

        //[HttpGet]
        //public async Task<string> GetString()
        //{
        //    await Task.CompletedTask;
        //    return "Welcome";
        //}

        [HttpGet]
        public async Task<string> GetTokenAsync(string name, string password)
        {
            // 认证用户
            var user = await _jWTAuthentication.LoginAsync(name, password);
            if (user == null)
                return "Login Failed";

            var token = _tokenServic.GetToken(user);

            var response = new
            {
                Status = true,
                Token = token,
                Type = "Bearer"
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}