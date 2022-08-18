using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthenticationCenter
{
    public class ClientInitConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        /// <summary>
        /// 哪些客户端 Client（应用） 可以使用这个 Authorization Server
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("UserApi","用户api资源")
                {
                    Scopes={ "gatewayScope" }
                }
            };


        /// <summary>
        /// Authorization Server 保护了哪些 API Scope（作用域）
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("gatewayScope"),
                new ApiScope("scope2")
            };


        /// <summary>
        /// 哪些客户端 Client（应用） 可以使用这个 Authorization Server
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    // 客户端惟一标识
                    ClientId = "AuthenticationCenter",
                    ClientName = "客户端",

                    //授权方式，这里采用的是客户端认证模式，只要ClientId，以及ClientSecrets正确即可访问对应的AllowedScopes里面的api资源
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // 客户端密码，进行了加密
                    ClientSecrets =new[]{ new Secret("orz123456coco".Sha256()) },

                    // 授权方式，客户端认证，只要ClientId+ClientSecrets
                    AllowedScopes = new[] { "gatewayScope" },// 允许访问的资源

                }
            };
    }
}
