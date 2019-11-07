using JwtAuthenticationHelper.Extensions;
using JwtAuthenticationHelper.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Middleware_CoreWeb
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
            services.AddControllersWithViews();
            //配置Swagger
            //注册Swagger生成器，定义一个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CoreWebApi",
                    Version = "v1",
                    Description = "ASP.NET Core WebApi"
                });

                // 读取xml信息 使用反射获取xml文件。并构造出文件的路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                c.IncludeXmlComments(xmlPath, true);

                // 启用swagger验证功能
                //var security = new Dictionary<string, IEnumerable<string>> { { "CoreAPI", new string[] { } }, };
                //c.AddSecurityRequirement(security);
                //c.AddSecurityDefinition("CoreAPI", new ApiKeyScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格",
                //    Name = "Authorization",//jwt默认的参数名称
                //    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = "apiKey"
                //});
            });

            var tokenOptions = new TokenOptions(
               Configuration["Token:Audience"],
               Configuration["Token:Issuer"],
               Configuration["Token:SigningKey"]);

            services.AddJwtAuthenticationWithProtectedCookie(
                tokenOptions);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiresAdmin",
                    policy =>
                    policy.RequireClaim("HasAdminRights"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))
            });
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            //// 启用中间件服务生成Swagger
            //// 启用中间件服务生成SwaggerUI，指定Swagger JSON终结点
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");
                c.RoutePrefix = string.Empty;//设置根节点访问
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}");
            });
        }
    }
}