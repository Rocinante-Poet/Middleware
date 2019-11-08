using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Middleware_Tool
{
    public interface IJWTTokenService
    {
        string GetToken(JWTUserModel user);
    }

    public class JWTTokenService : IJWTTokenService
    {
        private readonly ServerJwtSetting _jwtSetting;

        public JWTTokenService()
        {
            _jwtSetting = new ServerJwtSetting
            {
                SecurityKey = "d0ecd23c-dvdb-5005-a2ka-0fea210c858a", // 密钥
                Issuer = "jwtIssuertest", // 颁发者
                Audience = "jwtAudiencetest", // 接收者
                ExpireSeconds = 999999 // 过期时间
            };
        }

        public string GetToken(JWTUserModel user)
        {
            // 创建用户身份标识
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Uid", user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim("Uname", user.Name, ClaimValueTypes.String),
                new Claim("Role", user.RoleNumber.ToString(), ClaimValueTypes.Integer32)
            };

            // 创建令牌
            var token = new JwtSecurityToken(
                    issuer: _jwtSetting.Issuer,
                    audience: _jwtSetting.Audience,
                    signingCredentials: _jwtSetting.Credentials,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds)
                );
            /*
            iss (issuer)：签发人
            exp (expiration time)：过期时间
            sub (subject)：主题
            aud (audience)：受众
            nbf (Not Before)：生效时间
            iat (Issued At)：签发时间
            jti (JWT ID)：编号
            */

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}