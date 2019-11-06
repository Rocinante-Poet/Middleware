using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtAuthenticationHelper.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;

namespace Middleware_CoreWeb.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AuthorityController(IJwtTokenGenerator jwtTokenGenerator)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody]Userinfo userCredentials)
        {
            // Replace this with your custom authentication logic which will
            // securely return the authenticated user's details including
            // any role specific info
            if (userCredentials.Name == "1" && userCredentials.Pwd == "1")
            {
                var userInfo = new Userinfo
                {
                    Name = "UserFName",
                    Remark = "UserLName",
                    HasAdminRights = true
                };

                var accessTokenResult = jwtTokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
                    userCredentials.Name,
                    AddMyClaims(userInfo));

                return Ok(accessTokenResult.AccessToken);
            }
            else
            {
                return Unauthorized();
            }
        }

        private static IEnumerable<Claim> AddMyClaims(Userinfo authenticatedUser)
        {
            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, authenticatedUser.Name),
                new Claim(ClaimTypes.Surname, authenticatedUser.Remark),
                new Claim("HasAdminRights", authenticatedUser.HasAdminRights ? "Y" : "N")
            };

            return myClaims;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(new { Code = 200, Message = "Success!" });
        }

        [Authorize]
        [HttpGet("/A")]
        public JsonResult A()
        {
            return new JsonResult(new { Code = 200, Message = "Success!" });
        }

        [HttpGet("/B")]
        public JsonResult B()
        {
            return new JsonResult(new { Code = 200, Message = "Success!" });
        }

        [HttpGet("/error")]
        public JsonResult Denied()
        {
            return new JsonResult(
                new
                {
                    Code = 0,
                    Message = "访问失败!",
                    Data = "此账号无权访问！"
                });
        }
    }
}