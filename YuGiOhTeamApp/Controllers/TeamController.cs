using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;
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
        [Route("leader/{username}")]
        public ActionResult ChangeTeamLeader([FromRoute] string username)
        {
            _teamService.PassLeader(username);
            return Ok();
        }
        [HttpPost]
        [Route("join")]
        public ActionResult RequestToJoin([FromQuery] string teamName)
        {
            return Ok("Sent request to join team: " + _teamService.RequestToJoin(teamName));
        }
        [HttpGet]
        [Route("requests")]
        public ActionResult<PagedResult<UserRequestDto>> ShowRequests([FromQuery] PageQuery query)
        {
            return Ok(_teamService.ShowRequests(query));
        }
        [HttpPut]
        [Route("request/{username}")]
        public ActionResult HandleJoinRequest([FromQuery] bool answer, [FromRoute] string username)
        {
            return Ok(_teamService.HandleJoinRequest(answer, username));
        }
        [HttpDelete]
        [Route("{username}")]
        public ActionResult DeleteUserFromTeam([FromRoute] string username)
        {
            return Ok(_teamService.DeleteUserFromTeam(username));
        }
        [HttpGet]
        public ActionResult<PagedResult<UserDto>> ShowUsers([FromQuery] PageQuery query)
        {
            return Ok(_teamService.ShowUsers(query));
        }
    }
}
