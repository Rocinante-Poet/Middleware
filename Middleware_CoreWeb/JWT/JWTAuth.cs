using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Middleware_CoreWeb
{
    public class JWTAuth
    {
        private readonly RequestDelegate _next;

        private JWTUpload _upload = new JWTUpload();

        public JWTAuth(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            string requestUrl = httpContext.Request.Path.Value;

            //if (requestUrl == "/" || requestUrl == "/api/Login")
            //{ httpContext.Request.Cookies.TryGetValue("access_token", out cookies_token); }

            var endpoint = httpContext.GetEndpoint();
            var authorizeData = endpoint?.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? Array.Empty<IAuthorizeData>();

            if (authorizeData == null || authorizeData.Count == 0)
            {
                await _next(httpContext);
                return;
            }
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(httpContext);
                return;
            }

            var result = await AuthorizationService(httpContext);

            if (result == false)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized");
                await _next(httpContext);
                return;
            }

            await _next(httpContext);
            return;
        }

        private async Task<bool> AuthorizationService(HttpContext context)
        {
            string jwt = context.Request.Headers["Authorization"];

            if (jwt == null || jwt == "")
            {
                context.Request.Cookies.TryGetValue("access_token", out string cookies_token);
                context.Request.Headers.Add("Authorization", $"Bearer {AESDecrypt(cookies_token)}");
            }

            var result = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            { return false; }

            string requestUrl = context.Request.Path.Value;

            var _role = result.Principal.Claims.ToList().FirstOrDefault(x => x.Type == JwtClaimTypes.Scope).Value;

            var b = _upload.IsHasRole(int.Parse(_role), requestUrl);

            return true;
        }

        public static string AESDecrypt(string value, string _aeskey = "[23|*kjenHU~'e;]")
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
                byte[] toEncryptArray = Convert.FromBase64String(value);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}