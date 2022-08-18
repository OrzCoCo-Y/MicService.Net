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
            #region �ͻ���ģʽ

            // ��Ȩ����
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(ClientInitConfig.IdentityResources)

                .AddInMemoryClients(ClientInitConfig.Clients) 
                .AddInMemoryApiScopes(ClientInitConfig.ApiScopes)
                .AddInMemoryApiResources(ClientInitConfig.ApiResources)
                
                .AddDeveloperSigningCredential(); // Ĭ�ϵĿ�����֤��--��ʱ֤��--��������Ϊ�˱�֤token��ʧЧ��֤���ǲ����

            #region ���Է����ܱ�������Դ

            // ���Է����ܱ�������Դ
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    // IdentityServer��ַ
            //    options.Authority = "http://localhost:7000";

            //    // ��ӦIdp��ApiResource��Name
            //    options.Audience = "UserApi";

            //    // ��ʹ��https
            //    options.RequireHttpsMetadata = false;
            //}); 
            #endregion

            #endregion

            #region ����ģʽ

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤�� 
            //   .AddInMemoryApiResources(PasswordInitConfig.GetApiResources())//API������Ȩ��Դ
            //   .AddInMemoryClients(PasswordInitConfig.GetClients())  //�ͻ���
            //   .AddTestUsers(PasswordInitConfig.GetUsers());//����û�

            #endregion

            #region ����ģʽ

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤�� 
            //   .AddInMemoryApiResources(ImplicitInitConfig.GetApiResources()) //API������Ȩ��Դ
            //   .AddInMemoryClients(ImplicitInitConfig.GetClients())//�ͻ���
            //   .AddTestUsers(ImplicitInitConfig.GetUsers()); //����û�

            #endregion

            #region Codeģʽ

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤�� 
            //   .AddInMemoryApiResources(CodeInitConfig.GetApiResources()) //API������Ȩ��Դ
            //   .AddInMemoryClients(CodeInitConfig.GetClients())//�ͻ���
            //   .AddTestUsers(CodeInitConfig.GetUsers()); //����û�

            #endregion

            #region Hybridģʽ

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤�� 
            //    .AddInMemoryIdentityResources(HybridInitConfig.GetIdentityResources())//�����Ϣ��Ȩ��Դ
            //   .AddInMemoryApiResources(HybridInitConfig.GetApiResources()) //API������Ȩ��Դ
            //   .AddInMemoryClients(HybridInitConfig.GetClients())//�ͻ���
            //   .AddTestUsers(HybridInitConfig.GetUsers()); //����û�

            #endregion

            #region ����ģʽ+EFCore

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
            //services.InitSeedData(connectionString);//��ʼԭ������Щ�ڴ�����

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

            #region �Զ���Grant_Type

            //var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            //services.InitSeedData(connectionString);//��ʼԭ������Щ�ڴ�����
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

            // IdentityServer�м��
            app.UseIdentityServer();

            app.UseRouting();

            // �����֤
            // app.UseAuthentication();

            // ��Ȩ
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
