using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OcelotGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration(conf =>
              {
                  conf.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
              })
               .ConfigureLogging((context, loggingBuilder) =>
               {
                   //loggingBuilder.AddFilter("System", LogLevel.Warning);//���˵������ռ�
                   //loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                   //loggingBuilder.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);
                   //loggingBuilder.AddFilter("Ocelot.Logging.OcelotDiagnosticListener", LogLevel.Warning);
                   //loggingBuilder.AddFilter("Ocelot.Authorisation.Middleware.AuthorisationMiddleware", LogLevel.Warning);
                   loggingBuilder.AddLog4Net();//ʹ��log4net
               })//��չ��־
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              });
    }
}
