using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationCenter.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        public string Index()
        {
            return "【授权中心】- 获取到了受保护的资源";
        }

        [AllowAnonymous]
        [HttpPost]
        public string Allow()
        {
            return "【授权中心】- 无需授权可访问的资源";
        }
    }
}
