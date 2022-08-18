using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MicServiceWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace MicServiceWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region DataInit
        private List<User> _UserList = new List<User>()
        {
            new User()
            {
                Id=1,
                Account="Administrator",
                Email="57265177@qq.com",
                Name="Eleven",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
             new User()
            {
                Id=1,
                Account="Apple",
                Email="57265177@qq.com",
                Name="Apple",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
              new User()
            {
                Id=1,
                Account="Cole",
                Email="57265177@qq.com",
                Name="Cole",
                Password="1234567890",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
        };
        #endregion

        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _IConfiguration;

        public UserController(ILogger<UserController> logger,
            IConfiguration configuration)
        {
            _IConfiguration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<User> GetAll()
        {
            this._logger.LogInformation($"This is UserController-Get {this._IConfiguration["port"]}");
            return this._UserList.Select(u => new User()
            {
                Id = u.Id,
                Account = u.Account,
                Name = u.Name,
                Role = $"{ this._IConfiguration["ip"]}{ this._IConfiguration["port"]}",
                Email = u.Email,
                LoginTime = u.LoginTime,
                Password = u.Password
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<User> GetAllWithNoVerify()
        {
            this._logger.LogInformation($"This is UserController-Get {this._IConfiguration["port"]}");
            return this._UserList.Select(u => new User()
            {
                Id = u.Id,
                Account = u.Account,
                Name = u.Name,
                Role = $"{this._IConfiguration["ip"]}{this._IConfiguration["port"]}",
                Email = u.Email,
                LoginTime = u.LoginTime,
                Password = u.Password
            });
        }
    }
}
