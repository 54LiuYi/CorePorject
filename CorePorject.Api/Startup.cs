using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using CorePorject.Model;

namespace CorePorject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //注册上下文对象 并设置获取配置信息中的 连接地址
            services.AddDbContext<HealthyDBContext>(options =>
            {
                var connection = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(
                    connection
                    );
            });

            //注册redis 服务
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["Redis:Host"];
                options.InstanceName = Configuration["Redis:InstanceName"]; ;
            });

            //注册Cors 跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",  //定义授权方案的名称
                   new OpenApiSecurityScheme()
                   {
                       Description = "请输入token：Authorization:{token}",  //描述文字
                       Name = "Authorization",  //参数名--与标题头的参名相同
                       In = ParameterLocation.Header,  //参数放在Header中
                       Type = SecuritySchemeType.ApiKey,  //类型是apikey
                   });
                //加载授权方案
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"  //授权方案名称
                            }
                        },
                        new string[] { }
                    }
                });
            });

            //注册Jwt
            services.AddAuthorizationSetup(Configuration);

            //添加对AutoMapper的支持，会查找所有程序集中继承了 Profile 的类
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //批量注册
            builder.RegisterModule(new CustomAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //处理异常
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //自定义自己的文件路径
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = new PathString("/Files"),//对外的访问路径
                //env.ContentRootPath 获取系统的路径
                FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/Files")//指定实际物理路径
            });

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; //配置根目录打开Swagger UI
            });

            //路由
            app.UseRouting();

            //跨域
            app.UseCors("AllowCors");

            //开启认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();

            //终结点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
