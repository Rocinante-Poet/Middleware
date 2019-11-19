using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Middleware_CoreWeb;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJWTTokenService, JWTTokenService>();
            //services.AddScoped<IJWTAuthentication, JWTAuthentication>();
            //services.AddScoped<IJWTIdentityService, JWTIdentityService>();

            var jwtSetting = new JwtSetting();
            Configuration.Bind("JwtSetting", jwtSetting);

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.Events = new JwtBearerEvents()
                   {
                       ////在第一次接收到协议消息时
                       //OnMessageReceived = context =>
                       //{
                       //    context.Token = context.Request.Query["access_token"];
                       //    return Task.CompletedTask;
                       //},
                       ////未授权时
                       //OnChallenge = context =>
                       //{
                       //    context.Response.Redirect("https://cn.bing.com/");
                       //    //return new JsonResult((Success: false, Message: "用户名或密码不正确！"));
                       //    return Task.CompletedTask;
                       //},
                       ////如果授权失败并导致禁止响应时
                       //OnForbidden = context =>
                       //{
                       //    context.Response.WriteAsync("如果授权失败并导致禁止响应");
                       //    return Task.CompletedTask;
                       //},
                       //认证失败
                       OnAuthenticationFailed = context =>
                       {
                           context.Response.WriteAsync("在请求处理期间抛出异常");
                           return Task.CompletedTask;
                       },
                       ////在Token验证通过后调用
                       //OnTokenValidated = context =>
                       //{
                       //    context.Response.WriteAsync("在验证通过后调用");
                       //    return Task.CompletedTask;
                       //}
                   };

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))
            });
        
            app.UseRouting();
       
            // 认证授权
            app.UseMiddleware<JWTAuth>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                          name: "default",
                          pattern: "{controller=Home}/{action=LoginMain}/{id?}");
            });
        }
    }
}