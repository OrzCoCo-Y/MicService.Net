using Consul;
using Microsoft.Extensions.Configuration;
using System;

namespace MicServiceWebApi.Utility
{
    public static class ConsulHelper
    {
        public static async void RegisterConsul(this IConfiguration configuration)
        {
            var ip = configuration["ip"];
            var port = int.Parse(configuration["port"]);
            var weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

            using ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri(configuration["ConsulAddress"]);
                c.Datacenter = configuration["ConsulCenter"];
            });
            await client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "MicService " + ip + ":" + port, 
                Name = "MicServiceDemo",
                Address = ip,
                Port = port,
                Tags = new string[] { weight.ToString() },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),
                    HTTP = $"http://{ip}:{port}/Api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                }
            });

            //命令行参数获取
            Console.WriteLine($"{ip}:{port}--weight:{weight}");
        }
    }
}
