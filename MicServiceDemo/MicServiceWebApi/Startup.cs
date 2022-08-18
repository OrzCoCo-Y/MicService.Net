using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MicServiceWebApi.Utility;

namespace MicServiceWebApi
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
            services.AddControllers()
                .AddMvcOptions(options =>
                {
                    options.EnableEndpointRouting = false;
                });

            // 授权认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:7000";    //id4服务地址
                    options.ApiName = "UserApi";    //id4 api资源里的ApiName
                    options.RequireHttpsMetadata = false;   //不使用https
                    options.SupportedTokens = SupportedTokens.Both;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication(); // api资源需先身份验证，  否则会IdentityServer4 AuthenticationScheme: Bearer was challenged 错误
            app.UseAuthorization();

            app.UseMvc();

            Configuration.RegisterConsul();
        }
    }
}
