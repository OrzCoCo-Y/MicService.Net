using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace AuthenticationCenter
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
            services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.EnableEndpointRouting = false;
            }); 
            #region 客户端模式

            // 授权中心
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(ClientInitConfig.IdentityResources)

                .AddInMemoryClients(ClientInitConfig.Clients) 
                .AddInMemoryApiScopes(ClientInitConfig.ApiScopes)
                .AddInMemoryApiResources(ClientInitConfig.ApiResources)
                
                .AddDeveloperSigningCredential(); // 默认的开发者证书--临时证书--生产环境为了保证token不失效，证书是不变的

            #region 测试访问受保护的资源

            // 测试访问受保护的资源
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    // IdentityServer地址
            //    options.Authority = "http://localhost:7000";

            //    // 对应Idp中ApiResource的Name
            //    options.Audience = "UserApi";

            //    // 不使用https
            //    options.RequireHttpsMetadata = false;
            //}); 
            #endregion

            #endregion

            #region 密码模式

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //   .AddInMemoryApiResources(PasswordInitConfig.GetApiResources())//API访问授权资源
            //   .AddInMemoryClients(PasswordInitConfig.GetClients())  //客户端
            //   .AddTestUsers(PasswordInitConfig.GetUsers());//添加用户

            #endregion

            #region 隐藏模式

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //   .AddInMemoryApiResources(ImplicitInitConfig.GetApiResources()) //API访问授权资源
            //   .AddInMemoryClients(ImplicitInitConfig.GetClients())//客户端
            //   .AddTestUsers(ImplicitInitConfig.GetUsers()); //添加用户

            #endregion

            #region Code模式

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //   .AddInMemoryApiResources(CodeInitConfig.GetApiResources()) //API访问授权资源
            //   .AddInMemoryClients(CodeInitConfig.GetClients())//客户端
            //   .AddTestUsers(CodeInitConfig.GetUsers()); //添加用户

            #endregion

            #region Hybrid模式

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //    .AddInMemoryIdentityResources(HybridInitConfig.GetIdentityResources())//身份信息授权资源
            //   .AddInMemoryApiResources(HybridInitConfig.GetApiResources()) //API访问授权资源
            //   .AddInMemoryClients(HybridInitConfig.GetClients())//客户端
            //   .AddTestUsers(HybridInitConfig.GetUsers()); //添加用户

            #endregion

            #region 密码模式+EFCore

            //var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ConfigurationDbContext>(opt => opt.UseSqlServer(connectionString
            //    , b => b.MigrationsAssembly("Zhaoxi.AspNetCore31.AuthenticationCenterIds4")));

            ////ConfigurationDbContext
            ////PersistedGrantDbContext

            //////services.AddDbContext<ConfigurationDbContext>(opt => opt.UseSqlServer(connectionString
            //////    , b => b.MigrationsAssembly("Zhaoxi.AspNetCore31.AuthenticationCenterIds4")));

            //////services.AddDbContext<PersistedGrantDbContext>(opt => opt.UseSqlServer(connectionString
            //////    , b => b.MigrationsAssembly("Zhaoxi.AspNetCore31.AuthenticationCenterIds4")));
            /////*
            ////  add-migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb 
            ////  add-migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
            //// */
            //var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            //services.InitSeedData(connectionString);//初始原来的那些内存数据

            //services
            //    .AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //        {
            //            builder.UseSqlServer(connectionString);
            //        };
            //    })
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //        {
            //            builder.UseSqlServer(connectionString);
            //        };
            //    })
            //   //.AddTestUsers(PasswordInitConfig.GetUsers());
            //   .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
            //   .AddProfileService<CustomProfileService>()
            //;
            //services.AddTransient<IUserServiceTest, UserServiceTest>();

            #endregion

            #region 自定义Grant_Type

            //var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            //services.InitSeedData(connectionString);//初始原来的那些内存数据
            //services
            //    .AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //        {
            //            builder.UseSqlServer(connectionString);
            //        };
            //    })
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //        {
            //            builder.UseSqlServer(connectionString);
            //        };
            //    })
            //    .AddExtensionGrantValidator<CustomElevenGrantValidator>();
            //services.AddTransient<IUserServiceTest, UserServiceTest>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // IdentityServer中间件
            app.UseIdentityServer();

            app.UseRouting();

            // 身份验证
            // app.UseAuthentication();

            // 授权
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();

            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}");
            //});

            app.UseMvc();

            IdentityModelEventSource.ShowPII = true; // here
        }
    }
}
