using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{
    public interface IJWTIdentityService
    {
        int GetUserId();

        string GetUserName();
    }

    public class JWTIdentityService : IJWTIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public JWTIdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int GetUserId()
        {
            var nameId = _context.HttpContext.User.FindFirst("id");

            return nameId != null ? Convert.ToInt32(nameId.Value) : 0;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.FindFirst("name")?.Value;
        }
    }
}