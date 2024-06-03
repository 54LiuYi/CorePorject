using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CorePorject.Model;
using CorePorject.DTO.System;
using CorePorject.IService.System;
using CorePorject.Api;
using System.Security.Cryptography;

namespace CorePorject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersService _usersService;

        public AuthenticationController(IConfiguration configuration,IUsersService usersService) {
            this._configuration = configuration;
            this._usersService = usersService;
        }

        [HttpPost]
        public ResultData<UserInfoDto> Login(LoginUser user)
        {
            ResultData<UserInfoDto> result = new ResultData<UserInfoDto>();

            if (string.IsNullOrWhiteSpace(user.LoginName) || string.IsNullOrWhiteSpace(user.LoginPwd))
            {

                result.state = -1;
                result.code = "";
                result.msg = "账户密码不能为空！";
                return result;
            }
            MD5 md5 = MD5.Create();
            string pwdmd5 = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(user.LoginPwd))).Replace("-", null);

            var u = _usersService.GetList(u=>u.LoginName==user.LoginName && u.LoginPwd== pwdmd5).FirstOrDefault();

            if (u!=null)
            {
                //验证通过
                //返回Token  （票）

                result.state = 1;
                result.code = "";
                result.msg = "登录成功！";
                result.data = new UserInfoDto()
                {
                    UserName = u.UserName,
                    Img = u.Img,
                    Token = GetJwtToken(u.RoleId.ToString())
                };
                return result;
            }
            else
            {
                result.state = -2;
                result.code = "";
                result.msg = "账户或者密码错误！";
                return result;
            }
        }


        /// <summary>
        /// 颁发令牌接口
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetJwtToken))]
        public string GetJwtToken(string userRole)
        {
            //获取在配置文件中获取Jwt属性设置
            string key = _configuration["JwtSettings:SecurityKey"];
            //Appsettings.app(new string[] { "JwtSettings", "SecurityKey" });
            string issuer = _configuration["JwtSettings:Issuer"];
            //Appsettings.app(new string[] { "JwtSettings", "Issuer" });
            string audience = _configuration["JwtSettings:Audience"];
            //Appsettings.app(new string[] { "JwtSettings", "Audience" });
            string expireminutes = _configuration["JwtSettings:ExpireMinutes"];

            //创建授权的token类
            SecurityToken securityToken = new JwtSecurityToken(
                issuer: issuer, //签发人
                audience: audience, //受众
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256), //秘钥

                //创建过期时间
                expires: DateTime.Now.AddMinutes(double.Parse(expireminutes)), //过期时间 

                //自定义JWT字段
                claims: new Claim[] {
                    //把模拟请求的角色权限添加到Role中 
                    new Claim(ClaimTypes.Role,userRole),
                    new Claim("RoleID",userRole),
                    //下面的都是自定义字段，可以任意添加到Claim作为信息共享
                    //new Claim("Name","我叫lilili"),
                    //new Claim("Age","18"),
                }
            );
            //返回请求token
            //return JsonConvert.SerializeObject(new { token = new JwtSecurityTokenHandler().WriteToken(securityToken) });

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

    }

    public class LoginUser {
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
    }
}
