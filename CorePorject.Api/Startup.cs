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

            //ע�������Ķ��� �����û�ȡ������Ϣ�е� ���ӵ�ַ
            services.AddDbContext<HealthyDBContext>(options =>
            {
                var connection = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(
                    connection
                    );
            });

            //ע��redis ����
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["Redis:Host"];
                options.InstanceName = Configuration["Redis:InstanceName"]; ;
            });

            //ע��Cors ����
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            //ע��Swagger������������һ���Ͷ��Swagger �ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",  //������Ȩ����������
                   new OpenApiSecurityScheme()
                   {
                       Description = "������token��Authorization:{token}",  //��������
                       Name = "Authorization",  //������--�����ͷ�Ĳ�����ͬ
                       In = ParameterLocation.Header,  //��������Header��
                       Type = SecuritySchemeType.ApiKey,  //������apikey
                   });
                //������Ȩ����
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"  //��Ȩ��������
                            }
                        },
                        new string[] { }
                    }
                });
            });

            //ע��Jwt
            services.AddAuthorizationSetup(Configuration);

            //��Ӷ�AutoMapper��֧�֣���������г����м̳��� Profile ����
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //����ע��
            builder.RegisterModule(new CustomAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�����쳣
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //�Զ����Լ����ļ�·��
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = new PathString("/Files"),//����ķ���·��
                //env.ContentRootPath ��ȡϵͳ��·��
                FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/Files")//ָ��ʵ������·��
            });

            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; //���ø�Ŀ¼��Swagger UI
            });

            //·��
            app.UseRouting();

            //����
            app.UseCors("AllowCors");

            //������֤
            app.UseAuthentication();

            //��Ȩ
            app.UseAuthorization();

            //�ս��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
