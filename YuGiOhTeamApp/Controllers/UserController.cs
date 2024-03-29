﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Models;
using YuGiOhTeamApp.Services;

namespace YuGiOhTeamApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _userService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            return Ok(_userService.GenerateJwtToken(dto));
        }
    }
}
