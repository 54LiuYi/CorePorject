using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePorject.Api
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services,IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 配置策略授权
            services.AddAuthorization(o =>
            {
                // 添加策略，使用时在方法上标注[Authorize(Policy ="AdminPolicy")]，就会验证请求token中的ClaimTypes.Role是否包含了Admin
                o.AddPolicy("AdminPolicy", o =>
                {
                    //ClaimTypes.Role == Admin
                    o.RequireRole("Admin").Build();

                    //ClaimTypes.Role == Admin 或者 == User
                    //o.RequireRole("Admin","User").Build(); 

                    //ClaimTypes.Role == Admin 并且 == User ,关于添加多个角色策略，在Login控制器中
                    //o.RequireRole("Admin").RequireRole("User").Build(); 
                });

                //只有User的策略
                o.AddPolicy("onlyUserPolicy", o =>
                {
                    o.RequireRole("User").Build();
                });

                //User和Admin都可以访问的策略
                o.AddPolicy("UserOrAdminPolicy", o =>
                {
                    o.RequireRole("User", "Admin").Build();
                });

                //User并且是Admin才能请求的策略
                o.AddPolicy("UserAndAdminPolicy", o =>
                {
                    o.RequireRole("User").RequireRole("Admin").Build();
                });
            });

            string key = configuration["JwtSettings:SecurityKey"];
            string issuer = configuration["JwtSettings:Issuer"]; 
            string audience = configuration["JwtSettings:Audience"];
                

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)); //秘钥的长度有要求，必须>=16位

            services.AddAuthentication("Bearer").AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    //是否秘钥认证
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey, //秘钥

                    //是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = issuer, //这个字符串可以随便写，就是发行人

                    //是否验证订阅
                    ValidateAudience = true,
                    ValidAudience = audience,

                    //是否验证过期时间
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
            });
        }
    }
}
