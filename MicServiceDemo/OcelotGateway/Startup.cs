using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.Cache;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using OcelotGateway.OcelotExtend.CacheExtend;

namespace OcelotGateway
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
            #region 网关加入授权认证

            //var authenticationProviderKey1 = "UserGatewayKey";
            //Action<IdentityServerAuthenticationOptions> configureOptions = options =>
            //{
            //    options.Authority = "http://localhost:7000";
            //    options.ApiName = "UserApi"; // 授权中心 ids4中的ApiResource值
            //    options.RequireHttpsMetadata = false;
            //    options.SupportedTokens = SupportedTokens.Both;
            //};

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddIdentityServerAuthentication(authenticationProviderKey1, configureOptions);

            #endregion

            services.AddOcelot()
              .AddConsul()
              .AddCacheManager(x =>
              {
                  x.WithDictionaryHandle();//默认字典存储
              })
              .AddPolly();

            // 自定义缓存
            services.AddSingleton<IOcelotCache<CachedResponse>, CustomCacheExtend>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger<Startup>().LogWarning("This is Configure...........");

            app.UseOcelot();

        }
    }
}
