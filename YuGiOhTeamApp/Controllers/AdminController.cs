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
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers([FromQuery] PageQuery query)
        {
            return Ok(_adminService.GetUsers(query));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserDto> GetUserById([FromRoute] Guid id)
        {
            return Ok(_adminService.GetUserById(id));
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteUser([FromRoute]Guid id)
        {
            _adminService.DeleteUser(id);
            return NoContent();
        }
    }
}
