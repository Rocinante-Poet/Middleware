using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        ////private readonly IJWTAuthentication _jWTAuthentication;
        //private readonly IJWTTokenService _tokenServic;

        //public GetTokenController(IJWTTokenService token)
        //{
        //    _tokenServic = token;
        //}

        //[HttpGet]
        //public async Task<string> GetTokenAsync(string name, string password)
        //{
        //    // 认证用户
        //    //var user = await _jWTAuthentication.LoginAsync(name, password);
        //    //if (user == null)
        //    //    return "Login Failed";

        //    //var token = _tokenServic.GetToken(user);

        //    //var response = new
        //    //{
        //    //    token_type = JwtBearerDefaults.AuthenticationScheme,
        //    //    access_token = token,
        //    //    profile = new
        //    //    {
        //    //        sid = user.UserID,
        //    //        name = user.Name,
        //    //    }
        //    //};

        //    return JsonConvert.SerializeObject(response);
        //}
    }
}