using CZGL.Auth.Interface;
using CZGL.Auth.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Middleware_CoreWeb.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
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
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "CoreWebApi",
            //        Version = "v1",
            //        Description = "ASP.NET Core WebApi"
            //    });

            //    // 读取xml信息 使用反射获取xml文件。并构造出文件的路径
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
            //    c.IncludeXmlComments(xmlPath, false);
            //    ////启用swagger验证功能 ApiKeyScheme
            //    //c.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //    //{
            //    //    Description = "权限认证(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
            //    //    Name = "Authorization",//jwt默认的参数名称
            //    //    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
            //    //    Type = "apiKey"
            //    //});//Authorization的设置
            //});

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJWTAuthentication, JWTAuthentication>();
            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddScoped<IJWTIdentityService, JWTIdentityService>();

            var jwtSetting = new Middleware_Tool.JwtSetting();
            Configuration.Bind("JwtSetting", jwtSetting);

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = jwtSetting.Issuer,
                       ValidAudience = jwtSetting.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),
                       ClockSkew = TimeSpan.Zero
                   };
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

            // 启用中间件服务生成Swagger
            app.UseSwagger();
            // 启用中间件服务生成SwaggerUI，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;//设置根节点访问
            });

            // 认证授权
            app.UseAuthorization();
            app.UseAuthentication();

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseMiddleware<RoleMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
                      {
                          endpoints.MapControllerRoute(
                              name: "default",
                              pattern: "{controller=Home}/{action=Login}/{id?}");
                      });
        }
    }
}