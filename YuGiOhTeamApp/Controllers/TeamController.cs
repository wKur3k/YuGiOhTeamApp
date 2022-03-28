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
    [Route("api/team")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpPost]
        public ActionResult CreateTeam([FromBody] CreateTeamDto dto)
        {
            _teamService.CreateTeam(dto);
            return Ok();
        }
        [HttpPut]
        public ActionResult ChangeDescription([FromBody] string newDesc)
        {
            if (_teamService.ChangeDescription(newDesc))
            {
                return Ok();
            }
            return BadRequest("You are not team leader.");
        } 
        [HttpPut]
        [Route("{username}")]
        public ActionResult ChangeTeamLeader([FromRoute] string username)
        {
            _teamService.passLeader(username);
            return Ok();
        }
        [HttpDelete]
        [Route("{teamId}")]
        public ActionResult DeleteAdmin([FromRoute] int teamId)
        {
            _teamService.deleteAdmin(teamId);
            return NoContent();
        }
    }
}
