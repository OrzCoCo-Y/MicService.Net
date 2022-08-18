using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MicServiceWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Account="MrZhang",
                Email="test001@yopmail.com",
                Name="张三",
                Password="1234567890",
                LoginTime=DateTime.Now,
                ApiDomain="Admin"
            },
             new User()
            {
                Id=2,
                Account="MissSi",
                Email="test002@yopmail.com",
                Name="李四",
                Password="1234567890",
                LoginTime=DateTime.Now,
                ApiDomain="Admin"
            },
              new User()
            {
                Id=3,
                Account="MrWu",
                Email="test003@yopmail.com",
                Name="王五",
                Password="1234567890",
                LoginTime=DateTime.Now,
                ApiDomain="Admin"
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
            _logger.LogInformation($"This is UserController-Get {_IConfiguration["port"]}");
            return _UserList.Select(u => new User()
            {
                Id = u.Id,
                Account = u.Account,
                Name = u.Name,
                ApiDomain = $"{_IConfiguration["ip"]}:{_IConfiguration["port"]}",
                Email = u.Email,
                LoginTime = u.LoginTime,
                Password = u.Password
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<User> GetAllWithNoVerify()
        {
            _logger.LogInformation($"This is UserController-Get {_IConfiguration["port"]}");
            return _UserList.Select(u => new User()
            {
                Id = u.Id,
                Account = u.Account,
                Name = u.Name,
                ApiDomain = $"{_IConfiguration["ip"]}:{_IConfiguration["port"]}",
                Email = u.Email,
                LoginTime = u.LoginTime,
                Password = u.Password
            });
        }
    }
}
